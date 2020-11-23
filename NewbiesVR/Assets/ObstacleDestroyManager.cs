using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ObstacleDestroyManager : MonoBehaviour
{
    PhotonView view;
    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HitBox")
        {
            view.RPC("HideThisObject", RpcTarget.All);
            Invoke("destroythisGO", 5f);
        }
    }
    private void destroythisGO()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    [PunRPC]
    void HideThisObject()
    {
        gameObject.SetActive(false);
    } 
}

