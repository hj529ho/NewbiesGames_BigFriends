using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class RuneTile : MonoBehaviour
{
    public RunePillar pillar;
    public Vector3 targetPos = Vector3.zero;
    public Vector3 zeroPos = Vector3.zero;
    public float Speed = 1f;
    PhotonView view;
    private void Start()
    {
        view = GetComponent<PhotonView>();
        zeroPos = transform.position;
    }
    private void Update()
    {
        if (!view.IsMine&&pillar.GetIsActive())
        {
            view.RequestOwnership();
        }
        if (pillar.GetIsActive())
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.01f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, zeroPos, 0.01f);
        }
    }
}
