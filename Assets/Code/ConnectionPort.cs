using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionPort : MonoBehaviour
{
    public bool isConnected;
    public bool isPowered;
    public GameObject cableExtension;
    public List<PowerCable> connectedCables = new List<PowerCable>();

    public MeshRenderer redLightMesh;
    public MeshRenderer greenLightMesh;

    public Material greenLight;
    public Material redLight;
    public Material lightOff;
    
    
    private void Start()
    {
        
        if (isPowered)
        {
            greenLightMesh.material = greenLight;
            redLightMesh.material = lightOff;
        }
        else
        {
            greenLightMesh.material = lightOff;
            redLightMesh.material = redLight;
        }
    }

    private void LateUpdate()
    {
        CheckForPower();
    }

    public void Connect(PowerCable cable)
    {
        isConnected = true;
        if(cableExtension != null) cableExtension.SetActive(true);
        connectedCables.Add(cable);
    }

    public void CheckForPower(PowerCable pwCable)
    {
        if (!pwCable.isPowered) return;
        
        if (this.isPowered != false) return;
        this.isPowered = true;
        greenLightMesh.material = greenLight;
        redLightMesh.material = lightOff;
    }
    
    private void CheckForPower()
    {
        if (isPowered) return;
    
        foreach (var cable in connectedCables)
        {
            if (cable.isPowered)
            {
                isPowered = true;
                greenLightMesh.material = greenLight;
                redLightMesh.material = lightOff;
                break;
            }
        }
    }
   
    
}
