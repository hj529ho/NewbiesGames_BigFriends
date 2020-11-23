using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PushableObstacle : MonoBehaviour
{
    PhotonView view;
    Vector3 PlayerPosition;
    bool active = false;
    Animator anim;
    GameObject gem = null;
    private void Start()
    {
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (anim.GetBool("b1")&& anim.GetBool("b2") && anim.GetBool("b3"))
        {
            active = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Gem(Clone)")
        {
            if (!gem || gem != other.gameObject)
            {
                if (!view.IsMine)
                {
                    view.RequestOwnership();
                    Invoke("setanim", 0.5f);
                    gem = other.gameObject;
                    other.gameObject.GetComponent<SmallGem>().DestroyGem();
                }
                else
                {
                    if (anim.GetBool("b1") && anim.GetBool("b2"))
                    {
                        anim.SetBool("b3", true);
                        gem = other.gameObject;
                        other.gameObject.GetComponent<SmallGem>().DestroyGem();
                        
                    }
                    else if (anim.GetBool("b1") && !anim.GetBool("b2"))
                    {
                        anim.SetBool("b2", true);
                        gem = other.gameObject;
                        other.gameObject.GetComponent<SmallGem>().DestroyGem();

                    }
                    else
                    {
                        anim.SetBool("b1", true);
                        gem = other.gameObject;
                        other.gameObject.GetComponent<SmallGem>().DestroyGem();

                    }
                }
            }
        }
        if (active)
        {
            if (other.gameObject.name == "Player2(Clone)")
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
    }
    void setanim()
    {
        if (anim.GetBool("b1") && anim.GetBool("b2"))
        {
            anim.SetBool("b3", true);
 
        }
        else if (anim.GetBool("b1") && !anim.GetBool("b2"))
        {
            anim.SetBool("b2", true);

        }
        else if (!anim.GetBool("b1"))
        {
            anim.SetBool("b1", true);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (active)
        {
            if (other.gameObject.name == "Player2(Clone)" && other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                if (!view.IsMine)
                {
                    view.RequestOwnership();
                }
                PlayerPosition = other.gameObject.transform.position;
                PlayerPosition.y = 0;
                if (Mathf.Abs(PlayerPosition.x) < Mathf.Abs(PlayerPosition.z))
                {
                    PlayerPosition.x = 0;
                }
                if (Mathf.Abs(PlayerPosition.x) > Mathf.Abs(PlayerPosition.z))
                {
                    PlayerPosition.z = 0;
                }
                if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.W))
                {
                    if (transform.position.z <= 37.36f)
                    {
                        transform.Translate(-PlayerPosition * Time.deltaTime * 4);
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (active)
        {
            if (other.gameObject.name == "Player2(Clone)")
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
