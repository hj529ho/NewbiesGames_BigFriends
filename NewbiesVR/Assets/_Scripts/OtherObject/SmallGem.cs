using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SmallGem : MonoBehaviour
{
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    public void DestroyGem()
    {    
        if (view.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
