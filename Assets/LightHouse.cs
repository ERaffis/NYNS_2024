using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouse : MonoBehaviour
{

    public GameObject light;

    public Transform lightParent;

    public float angle = 10f;
    // Update is called once per frame
    void Update()
    {
        light.transform.RotateAround(lightParent.position,Vector3.up, angle * Time.deltaTime);
    }
}
