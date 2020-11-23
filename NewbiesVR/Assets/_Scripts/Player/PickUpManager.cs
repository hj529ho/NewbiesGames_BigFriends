using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PickUpManager : MonoBehaviour
{
    public PhotonView photonView;
    List<GameObject> ContactInteractable;
    List<GameObject> ContactSwitch;

    private void Start()
    {
        ContactInteractable = new List<GameObject>();
        ContactSwitch = new List<GameObject>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (photonView.IsMine == false)
        {
            return;
        }

        if (other.transform.gameObject.tag == "InteractiveObjects")
        {
            ContactInteractable.Add(other.transform.gameObject);
        }
        if (other.transform.gameObject.tag == "Switch")
        {
            ContactSwitch.Add(other.transform.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (photonView.IsMine == false)
        {
            return;
        }
        if (other.transform.gameObject.tag == "InteractiveObjects")
        {
            ContactInteractable.Remove(other.transform.gameObject);
        }
        if (other.transform.gameObject.tag == "Switch")
        {
            ContactSwitch.Remove(other.transform.gameObject);
        }
    }

    public List<GameObject> GetListInteractable()
    {
        return ContactInteractable;
    }
    public List<GameObject> GetListSwitch()
    {
        return ContactSwitch;
    }
    public void ClearListInsteractable()
    {
        ContactInteractable.Clear();
    }
}
