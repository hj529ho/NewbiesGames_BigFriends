using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMovePlatform : MonoBehaviour
{
    bool UpDownSwitch = true;
    // Vector3 ZeroPoint = Vector3.zero;
    public Transform WayPoint;
    private void FixedUpdate()
    {
        if(transform.localPosition.magnitude >= WayPoint.localPosition.magnitude)
        {
            UpDownSwitch = false;
        }
        if(WayPoint.transform.localPosition.magnitude - (WayPoint.transform.localPosition-transform.localPosition).magnitude <= 0)
        {
            UpDownSwitch = true;

        }

        if(UpDownSwitch)
        {
            transform.localPosition = transform.localPosition + WayPoint.localPosition*Time.deltaTime*0.2f;
            // Debug.Log("up");
        }
        else if(!UpDownSwitch)
        {
            transform.localPosition = transform.localPosition - WayPoint.localPosition*Time.deltaTime*0.2f;

            // Debug.Log("down");
        }

        Debug.DrawLine(WayPoint.position,transform.parent.position,Color.red);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.parent = transform;
            // other.transform.localPosition = Vector3.zero;
            Debug.Log("MovePlatform was parenting");
            // other.transform.position += transform.position;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("MovePlatform was realised");

            other.transform.parent = null;
    
        }
    }
}
