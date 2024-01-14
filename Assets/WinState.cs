using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinState : MonoBehaviour
{

    public ConnectionPort[] connectionPorts;
    public bool hasWon;

    public GameObject winCanvas;
    
    // Update is called once per frame

    private void Awake()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        var val = 0;
        foreach (var tPort in connectionPorts)
        {
            if (tPort.isPowered)
            {
                val++;
            }

            if (!hasWon && val == 4)
            {
                hasWon = true;
                PlayerWon();
            }
        }
    }

    public void PlayerWon()
    {
        winCanvas.SetActive(true);
    }
}
