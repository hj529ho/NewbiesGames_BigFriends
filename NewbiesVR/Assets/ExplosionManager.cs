using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ExplosionManager : MonoBehaviour
{
    PhotonView view;
    private void Awake()
    {
        view = GetComponent<PhotonView>();   
    }
    void Start()
    {
        if (!view.IsMine)
        {
            return;
        }
        Invoke("Destroythis", 5f);
    }
    void Destroythis()
    {
        PhotonNetwork.Destroy(gameObject);
    
    }
}
