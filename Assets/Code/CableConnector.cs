using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableConnector : MonoBehaviour
{
    public LayerMask myLayerMask;
    public GameObject nearestCollider = null;
    private float minSqrDistance = Mathf.Infinity;

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 10f,myLayerMask);
        
        if (hitColliders.Length == 0)
        {
            if (nearestCollider != null)
            {
                if(nearestCollider.TryGetComponent<ActivateMark>(out ActivateMark exclamationMark))
                {
                    exclamationMark.Activate(false);
                    return;
                }
            }

        }
        
        foreach (var t in hitColliders)
        {
            float sqrDistanceToCenter = (this.transform.position - t.transform.position).sqrMagnitude;

            if (sqrDistanceToCenter < minSqrDistance)
            {
                minSqrDistance = sqrDistanceToCenter;

                if (nearestCollider !=null)
                {
                    if(nearestCollider.TryGetComponent<ActivateMark>(out ActivateMark exclamationMark))
                    {
                        exclamationMark.Activate(false);
                    }
                }
                nearestCollider = t.gameObject;
                
                if(nearestCollider.TryGetComponent<ActivateMark>(out ActivateMark exclamationMark1))
                {
                    exclamationMark1.Activate(true);
                }
            }
            
            if (t.gameObject.CompareTag("ConnexionTerre"))
            {
                if (nearestCollider != null)
                {
                    if(nearestCollider.TryGetComponent<ActivateMark>(out ActivateMark exclamationMark))
                    {
                        exclamationMark.Activate(false);
                    }
                }
                
                nearestCollider = t.gameObject;
                if(nearestCollider.TryGetComponent<ActivateMark>(out ActivateMark exclamationMark1))
                {
                    exclamationMark1.Activate(true);
                }
            }
        }
        
    }   

    public GameObject CheckCollision()
    {
        return nearestCollider; 
    }
}
