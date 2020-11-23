using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class VerticalMovePlatform : MonoBehaviour
{
    PhotonView view;
    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player2(Clone)")
        {
            if (other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                other.transform.parent = transform;
                view.RPC("Parenting_RPC", RpcTarget.Others, other.gameObject.GetPhotonView().ViewID);
                return;
            }
            Debug.Log("MovePlatform was parenting");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Player2(Clone)")
        {
            if (other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                view.RPC("DisParent_RPC", RpcTarget.Others, other.gameObject.GetPhotonView().ViewID);
                other.transform.parent = null;
                return;
            }
            Debug.Log("MovePlatform was realised");
        }
    }

    [PunRPC]
    public void Parenting_RPC(int child)
    {
        PhotonView CurrentParent = view;
        PhotonView CurrentChild = PhotonView.Find(child);
        CurrentChild.transform.parent = CurrentParent.transform;
    }
    [PunRPC]
    public void DisParent_RPC(int child)
    {
        PhotonView CurrentChild = PhotonView.Find(child);
        CurrentChild.transform.parent = null;
    }
}
