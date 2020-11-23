using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GemExplode : MonoBehaviour
{
    PhotonView view;
    bool IsDestroyed = false;
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
            PhotonNetwork.Instantiate("Gem", transform.position + Vector3.up, transform.rotation);
            PhotonNetwork.Instantiate("Gem", transform.position + Vector3.up, transform.rotation);
            PhotonNetwork.Instantiate("Gem", transform.position + Vector3.up, transform.rotation);
        }
    }
    private void destroythisGO()
    {
        PhotonNetwork.Destroy(gameObject);
    }
    [PunRPC]
    void HideThisObject()
    {
        IsDestroyed = true;
        gameObject.SetActive(false);
    }
    public bool GetisDestroyed()
    {
        return IsDestroyed;
    }
}
