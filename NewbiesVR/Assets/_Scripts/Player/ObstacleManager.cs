using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private Rigidbody rigid;

    private Vector3 playerPosition;
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerPosition = Quaternion.Euler(0,180,0)*other.transform.position;
            rigid.AddForce(playerPosition*Time.deltaTime*100);
            Debug.Log("trigger");
    
        }
    }
}
