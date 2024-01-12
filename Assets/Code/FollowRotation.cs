using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRotation : MonoBehaviour
{ 
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Should Turn");
        this.transform.rotation = Quaternion.Euler(transform.parent.transform.rotation.x, transform.parent.transform.rotation.y + 90, transform.parent.transform.rotation.z);
    }
}
