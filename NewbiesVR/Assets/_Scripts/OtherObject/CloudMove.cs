using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector3.forward*0.1f);

        if (transform.position.z > 250)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -200);
        }
    }
}
