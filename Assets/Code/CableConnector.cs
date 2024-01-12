using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableConnector : MonoBehaviour
{
    public SphereCollider boatCheckSphere;
    public LayerMask myLayerMask;

    public GameObject CheckCollision()
    {
        GameObject nearestCollider = null;
        float minSqrDistance = Mathf.Infinity;
        
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, boatCheckSphere.radius,myLayerMask);
        
        foreach (var t in hitColliders)
        {
            float sqrDistanceToCenter = (this.transform.position - t.transform.position).sqrMagnitude;

            if (t.CompareTag("ConnexionTerre"))
            {
                return t.gameObject;
            }
            
            if (sqrDistanceToCenter < minSqrDistance)
            {
                minSqrDistance = sqrDistanceToCenter;
                nearestCollider = t.gameObject;
            }
        }

        return nearestCollider;
    }
}
