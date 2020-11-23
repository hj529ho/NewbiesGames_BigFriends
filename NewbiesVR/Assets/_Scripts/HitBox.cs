using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HitBox : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3f);
    }
   private void OnTriggerEnter(Collider other)
   {
       if(other.gameObject.tag == "Obstacle")
       {

            PhotonView view = other.gameObject.GetPhotonView();

            if (!view.IsMine)
            { 
                view.RequestOwnership();
            }
        }
   }
}
