using System.Collections;
using System.Collections.Generic;
using FlatKit;
using UnityEngine;

public class Buoy : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        var waterPlane = GameObject.FindGameObjectWithTag("WaterPlane");
        GetComponent<BuoyancyStatic>().water = waterPlane.transform;
    }
}
