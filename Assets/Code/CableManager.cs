using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CableManager : MonoBehaviour
{
    public Transform boatAttachPoint;
    public Transform cablePlacementPoint;
    public Vector3 lastPosition;

    public GameObject cablePrefab;
    public GameObject buoyPrefab;

    public GameObject cableContainer;
    public PowerCable activeCable;
    public List<PowerCable> allCables = new List<PowerCable>();

    public CableConnector cableConnector;
    
    public float desiredTime;
    public float timer;

    public float maxLenght;
    public float lenghtOfActiveCable;
    public int numberOfBuoy;
    
    [Header("UI")]
    public TMP_Text leghtUI;
    public TMP_Text buoyUI;
    public Image cableIndicator;
    public Sprite[] indicators;
   
    

    public event EventHandler OnCableConnection;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        lastPosition = boatAttachPoint.position;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) StartNewCable();
        if (Input.GetKeyDown(KeyCode.F)) StopCable();
        UpdateUI();
        
    }
    private void LateUpdate()
    {
        if (activeCable != null)
        {
            PlaceCablePoint();
            FollowBoat();
            BreakCable();
        }
        
    }

    public void ConnectCables()
    {
        if (activeCable == null)
        {
            StartNewCable();
        }
        else
        {
            StopCable();
        }
    }
    
    public void StartNewCable()
    {
        if (activeCable == null)
        { 
            
            //Instantiates new cable and assigns its LineRenderer to the active cable
            GameObject startCable = Instantiate(cablePrefab, cableContainer.transform);
            
            //Sets the activeCable (currently placing) has the PowerCable of the GameOject
            activeCable = startCable.GetComponent<PowerCable>();
            
            //Checks for connectors close by to attach the first point of the cable
            var closetConnector = cableConnector.CheckCollision();
            //If it finds a connector (either a buoy or a land connector)
            if (closetConnector != null)
            {
                var connectionPort = closetConnector.transform.parent.gameObject.GetComponent<ConnectionPort>();
                //Sets the start connection point of the cable
                activeCable.AddStartConnection(connectionPort);
                
                //Adds the power cable to the connector
                connectionPort.Connect(activeCable); 
                
                //Sets the position of the start cable point
                activeCable.lineRenderer.SetPosition(0,closetConnector.transform.position);
                
                //Sets the power state of the cable
                activeCable.SetPowerState(connectionPort);
            }
            else if(numberOfBuoy > 0)
            {
                var connectionPort = PlaceBuoy();
                
                activeCable.lineRenderer.SetPosition(0,cablePlacementPoint.position - new Vector3(0,-0.5f,0));
                
                //Sets the start connection point of the cable
                activeCable.AddStartConnection(connectionPort);
                
                //Adds the powercable to the connector
                connectionPort.Connect(activeCable);
                
                //Sets the power state of the cable
                activeCable.SetPowerState(connectionPort);
                numberOfBuoy--;
            } 
            else
            {
                DestroyCable();
                return;
            }

            cableIndicator.sprite = indicators[1];
        }
    }
    public void StopCable()
    {
        if (activeCable == null) return;
        
        //Resets the last position
        lastPosition = boatAttachPoint.position;
            
        
        //Checks for connectors close by to attach the end point of the cable
        var closetConnector = cableConnector.CheckCollision();
        //If it finds a connector (either a buoy or a land connector)
        if (closetConnector != null && closetConnector.transform.parent.gameObject != activeCable.firstConnection.gameObject)
        {
            var connectionPort = closetConnector.transform.parent.gameObject.GetComponent<ConnectionPort>();
            //Sets the end connection point of the cable
            activeCable.AddEndConnection(connectionPort);
            
            //Adds the power cable to the connector
            connectionPort.Connect(activeCable); 
            
            //Sets the position of the end cable point
            activeCable.lineRenderer.SetPosition(activeCable.lineRenderer.positionCount-1,closetConnector.transform.position); 
            
            //Sets the power state of the cable
            activeCable.SetPowerState(connectionPort);
            
        }
        else if (numberOfBuoy > 0)
        {
            var connectionPort = PlaceBuoy();
            
            //Sets the start connection point of the cable
            activeCable.AddEndConnection(connectionPort);
                
            //Adds the powercable to the connector
            connectionPort.Connect(activeCable); 
            
            //Sets the position of the end cable point
            activeCable.lineRenderer.SetPosition(activeCable.lineRenderer.positionCount-1,connectionPort.transform.position - new Vector3(0,0f,0));
            
            //Sets the power state of the cable
            activeCable.SetPowerState(connectionPort);
            
            numberOfBuoy--;
        }
        else
        {
            DestroyCable();
            return;
        }
        
        
        activeCable.firstConnection.CheckForPower(activeCable);
        activeCable.endConnection.CheckForPower(activeCable);
        
        activeCable.lineRenderer.Simplify(0.25f);
        
        allCables.Add(activeCable);
        activeCable = null;
        timer = 0;
        lenghtOfActiveCable = 0;
        
        cableIndicator.sprite = indicators[0];
        
        OnCableConnection?.Invoke(this, EventArgs.Empty);
    }

    
    
    void PlaceCablePoint()
    {
        if (activeCable == null) return;

        timer += Time.deltaTime;
        
        if (timer > desiredTime)
        {
            var currentPosition = new Vector3(boatAttachPoint.position.x,0,boatAttachPoint.position.z);
            var lastVector = new Vector3(lastPosition.x, 0, lastPosition.z);
            
            var distance = Vector3.Distance(lastVector, currentPosition);
            if (distance > 0.5f)
            {
                activeCable.lineRenderer.positionCount++;
                activeCable.lineRenderer.SetPosition(activeCable.lineRenderer.positionCount -3,cablePlacementPoint.position);
                lastPosition = currentPosition;
                if (activeCable.lineRenderer.positionCount > 3)
                {
                    Vector3 offset = activeCable.lineRenderer.GetPosition(activeCable.lineRenderer.positionCount - 3) - activeCable.lineRenderer.GetPosition(activeCable.lineRenderer.positionCount - 4);
                    float sqrLen = offset.sqrMagnitude;
                    lenghtOfActiveCable += sqrLen;
                }
            }
            timer = 0;
        }
    }
    public ConnectionPort PlaceBuoy()
    {
        var newBuoy = Instantiate(buoyPrefab, cableContainer.transform);
        newBuoy.transform.position = cablePlacementPoint.position + new Vector3(0,0f,0f);

        return newBuoy.GetComponent<ConnectionPort>();
        
    }

    
    private void FollowBoat()
    {
        if (activeCable == null) return;
        
        activeCable.lineRenderer.SetPosition(activeCable.lineRenderer.positionCount-1, boatAttachPoint.position);
        activeCable.lineRenderer.SetPosition(activeCable.lineRenderer.positionCount-2, cablePlacementPoint.position);
        
    }

    public void BreakCable()
    {
        
        if (lenghtOfActiveCable > maxLenght)
        {
            if (numberOfBuoy > 0)
            {
                Buoy();
            }
            else
            {
                DestroyCable();
            }
            
            
            timer = 0;
            lenghtOfActiveCable = 0;
        }
    }

    public void Buoy()
    {
        numberOfBuoy--;
        var connectionPort = PlaceBuoy();
        
        if (activeCable != null)
        {
            
            //Sets the start connection point of the cable
            activeCable.AddEndConnection(connectionPort);
                
            //Adds the powercable to the connector
            connectionPort.Connect(activeCable); 
            
            //Sets the position of the end cable point
            activeCable.lineRenderer.SetPosition(activeCable.lineRenderer.positionCount-1,connectionPort.transform.position - new Vector3(0,1f,0));
            
            //Sets the power state of the cable
            activeCable.SetPowerState(connectionPort);
                
            activeCable.lineRenderer.Simplify(0.25f);
        
            allCables.Add(activeCable);
            activeCable = null;
                
            GameObject startCable = Instantiate(cablePrefab, cableContainer.transform);
            lenghtOfActiveCable = 0f;
            
            //Sets the activeCable (currently placing) has the PowerCable of the GameOject
            activeCable = startCable.GetComponent<PowerCable>();
                
            activeCable.lineRenderer.SetPosition(0,cablePlacementPoint.position - new Vector3(0,0,0));
                
            //Sets the start connection point of the cable
            activeCable.AddStartConnection(connectionPort);
                
            //Adds the powercable to the connector
            connectionPort.Connect(activeCable);
                
            //Sets the power state of the cable
            activeCable.SetPowerState(connectionPort);
        }
    }
    
    public void DestroyCable()
    {
        if (activeCable != null)
        {
            allCables.Add(activeCable);
            //activeCable.firstConnection.connectedCables.RemoveAt(activeCable.firstConnection.connectedCables.Count -1);
            activeCable = null;

            Destroy(allCables.Last().gameObject,0.1f);
        
            allCables.RemoveAt(allCables.Count-1);
        
            timer = 0;
            lenghtOfActiveCable = 0;
        
            cableIndicator.sprite = indicators[0];
        }
       
    }

    public void UpdateUI()
    {
        var lenght = (int)lenghtOfActiveCable;
        buoyUI.SetText("x " + (int)numberOfBuoy);
        
        if (activeCable != null)
        {
            leghtUI.SetText( lenght.ToString("000" + " m"));
        }
        else
        {
            leghtUI.SetText("000 m");
        }
        
        
    }
}
