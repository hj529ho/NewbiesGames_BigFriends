using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BombRemoteController : MonoBehaviour
{
    bool isSpawnBomb;
    Bomb bomb;

    private void Update()
    {
        if (bomb)
        {
            isSpawnBomb = true;
        }
        else
        {
            isSpawnBomb = false;
        }
    }
    public void Remote()
    {
        if (isSpawnBomb)
        {
            bomb.Explode();
        }
        else
        {
            SpawnBomb();
        }
    }
    public void SpawnBomb()
    {
        bomb = PhotonNetwork.Instantiate("Bomb", transform.position + Vector3.up * 5, Quaternion.identity).GetComponent<Bomb>();
    }
}
