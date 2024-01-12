using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCable : MonoBehaviour
{
    public List<ConnectionPort> connections = new List<ConnectionPort>();
    public bool isPowered;
    public LineRenderer lineRenderer;
    
    public ConnectionPort firstConnection;
    public ConnectionPort endConnection;
    
    
    private void Awake()
    {
        isPowered = false;
    }
    
    private void LateUpdate()
    {
        if (connections.Count > 0)
        {
            CheckForPower();
        }
    }

    public void AddStartConnection(ConnectionPort connector)
    {
        firstConnection = connector;
        connections.Add(connector);
    }
    
    public void AddEndConnection(ConnectionPort connector)
    {
        endConnection = connector;
        connections.Add(connector);
    }

    public void SetPowerState(ConnectionPort connector)
    {
        if (isPowered) return;
        isPowered = connector.isPowered;
    }
    
   
    private void CheckForPower()
    {
        if (isPowered) return;
        if (firstConnection != null &&firstConnection.isPowered)
        {
            isPowered = true;
            return;
        }

        if (endConnection != null && endConnection.isPowered)
        {
            isPowered = true;
        }
    }
    
}
