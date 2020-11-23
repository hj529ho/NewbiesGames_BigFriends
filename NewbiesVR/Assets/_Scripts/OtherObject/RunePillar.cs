using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class RunePillar : MonoBehaviour
{
    PhotonView view;
    bool isActive = false;
    //GameObject halo;
    Animator anim;
    private void Start()
    {
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
    }
    public bool GetIsActive() 
    {
        return isActive;
    }
    public void SetIsActive()
    {
        isActive = true;
        if (!view.IsMine)
        {
            view.RequestOwnership();
            Invoke("setanim", 0.5f);
        }
        else
        {
            anim.SetBool("On", isActive);
        }
               
    }
    void setanim()
    {
        anim.SetBool("On", isActive);
    }
}
