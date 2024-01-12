using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMark : MonoBehaviour
{
    public GameObject mark;
    public SphereCollider collider;

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, collider.radius);
        
        foreach (var t in hitColliders)
        {
            if (t.CompareTag("BoatConnection"))
            {
                mark.SetActive(true);
                return;
            }
            else
            {
                mark.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.CompareTag("BoatConnection"))
        {
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.CompareTag("BoatConnection"))
        {
            mark.SetActive(false);
        }
    }
}
