using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class MoveDoor : MonoBehaviour
{
    bool isActive = false;
    public GemExplode GemExplode;
 
    private void Update()
    {
        isActive = GemExplode.GetisDestroyed();
        if (!isActive)
        {
            return;
        }
        if (transform.position.y>-5.0f)
        {
            transform.Translate(Vector3.down);
        }

    }
}
