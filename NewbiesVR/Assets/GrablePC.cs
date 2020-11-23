using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GrablePC : MonoBehaviour
{
    PhotonView view;
    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }
    public void OwershipChage()
    {
        view.RequestOwnership();
    }
}
