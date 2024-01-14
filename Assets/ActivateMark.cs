using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMark : MonoBehaviour
{
    public GameObject mark; 
    public void Activate(bool state)
    {
        if (state != mark.activeSelf)
        {
            mark.SetActive(state);
        }
    }
}
