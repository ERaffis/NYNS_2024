using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableConnector : MonoBehaviour
{
    public LayerMask myLayerMask;
    public GameObject nearestCollider = null;
    private float minSqrDistance = Mathf.Infinity;
    public GameObject previousCollider;

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 5f,myLayerMask);
        minSqrDistance = Mathf.Infinity;
        
        if (hitColliders.Length == 0)
        {
            nearestCollider = null;
            if(previousCollider !=null) previousCollider.GetComponent<ActivateMark>().Activate(false);
        }
        
        foreach (var t in hitColliders)
        {
            if (t.gameObject != previousCollider)
            {
                t.gameObject.GetComponent<ActivateMark>().Activate(false);
            }
            
            float sqrDistanceToCenter = (this.transform.position - t.transform.position).sqrMagnitude;

            if (sqrDistanceToCenter < minSqrDistance)
            {
                minSqrDistance = sqrDistanceToCenter;
                previousCollider = nearestCollider;
                nearestCollider = t.gameObject;
                if(previousCollider !=null) previousCollider.GetComponent<ActivateMark>().Activate(false);
                nearestCollider.GetComponent<ActivateMark>().Activate(true);
            }
            
            if (t.gameObject.CompareTag("ConnexionTerre"))
            {
                previousCollider = nearestCollider;
                nearestCollider = t.gameObject;
                if(previousCollider !=null) previousCollider.GetComponent<ActivateMark>().Activate(false);
                nearestCollider.GetComponent<ActivateMark>().Activate(true);
                break;
            }
        }
    }   

    public GameObject CheckCollision()
    {
        return nearestCollider; 
    }
}
