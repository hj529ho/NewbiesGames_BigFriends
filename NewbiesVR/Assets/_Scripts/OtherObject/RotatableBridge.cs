using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableBridge : MonoBehaviour
{
    public GameObject Cylinder;
    Animator anim;

    private void Start()
    {
        anim = Cylinder.GetComponent<Animator>();
    }
    public void BridgeActive() 
    {
        anim.SetBool("Active", true);
    
    }
    public void BridgeDisable()
    {
        anim.SetBool("Active", false);

    }

}
