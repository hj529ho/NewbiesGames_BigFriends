using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class DoorOpen : MonoBehaviour
{
    PhotonView view;
    public RunePillar pillar;
    public float rotationAngle = 0;


    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    private void Update()
    {

        if (!view.IsMine && pillar.GetIsActive())
        {
            view.RequestOwnership();
        }
        if (pillar.GetIsActive())
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, rotationAngle, 0), Time.deltaTime * 2);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * 2);
        }
    }
}
