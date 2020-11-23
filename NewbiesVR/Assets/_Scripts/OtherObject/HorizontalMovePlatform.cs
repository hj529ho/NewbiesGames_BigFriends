using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovePlatform : MonoBehaviour
{
      bool UpDownSwitch = true;
    public float maxHight = 1;
    public float minHight = 0;
    private void FixedUpdate()
    {
        if(transform.localPosition.z >= maxHight)
        {
            UpDownSwitch = false;

        }else if(transform.localPosition.z<= minHight)
        {
            UpDownSwitch = true;

        }

        if(UpDownSwitch)
        {
            transform.Translate(Vector3.forward*Time.deltaTime*3);
        }
        else if(!UpDownSwitch)
        {
            transform.Translate(Vector3.back*Time.deltaTime*3);
        }


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
