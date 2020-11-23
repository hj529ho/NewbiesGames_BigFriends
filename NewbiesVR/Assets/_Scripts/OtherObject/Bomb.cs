using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Bomb : MonoBehaviour
{

   
    PhotonView view;
    public GameObject hitbox;
    bool isExploding = false;
    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    public void Explode()
    {
        if (!view.IsMine)
        {
            view.RequestOwnership();
        }
        if (isExploding)
        {
            return;
        }
        isExploding = true;
        view.RPC("HideThisObject", RpcTarget.All);
        PhotonNetwork.Instantiate("FX_Explosion",transform.position,transform.rotation);
        Instantiate(hitbox,transform.position,transform.rotation);
        Invoke("destroythisGO", 5f);
    }
    private void destroythisGO()
    {
        isExploding = false;
        PhotonNetwork.Destroy(gameObject);
    }

    [PunRPC]
    void HideThisObject()
    {
        gameObject.SetActive(false);
    }

}
