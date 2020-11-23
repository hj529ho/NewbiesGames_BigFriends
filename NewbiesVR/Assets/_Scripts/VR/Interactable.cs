using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public Hand m_ActiveHand;
    private PhotonView view;
    private Rigidbody body;
    private Outline outline;
    Color Oncolor;
    Color Offcolor;
    private void Awake()
    {
        Oncolor = Color.yellow;
        Offcolor = Color.white;
        Offcolor.a = 0;
        view = GetComponent<PhotonView>();
        body = GetComponent<Rigidbody>();
        outline = GetComponent<Outline>();
        if (outline)
        {
            outline.OutlineColor = Offcolor;
            outline.OutlineWidth = 15;
        }
    }
    public void OwnerShipChange()
    {
        view.RequestOwnership();
    }

    public void ChangeKinematic_true()
    {
        view.RPC("ChangeKinematic_true_RPC", RpcTarget.Others);
    }
    public void ChangeKinematic_false()
    {
        view.RPC("ChangeKinematic_false_RPC", RpcTarget.Others);
    }
    public void OutlineOn()
    {
        if (outline)
        {
            outline.OutlineColor = Oncolor;
        }
    }
    public void OutlineOff()
    {
        if (outline)
        {
            outline.OutlineColor = Offcolor;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            OutlineOff();
        }
    }
    [PunRPC]
    public void ChangeKinematic_true_RPC()
    {
        if (body)
        {
            body.isKinematic = true;
        }
    }

    [PunRPC]
    public void ChangeKinematic_false_RPC()
    {
        if (body)
        {
            body.isKinematic = false;
        }
    }
}