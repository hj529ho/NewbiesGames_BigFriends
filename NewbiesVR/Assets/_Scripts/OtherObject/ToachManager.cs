using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToachManager : MonoBehaviour
{
    bool isFire = false;
    GameObject Fire;
    private void Awake()
    {
        Fire = transform.Find("FX_Fire").gameObject;
    }
    private void Update()
    {
        if (isFire)
        {
            Fire.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fire")
        {
            isFire = true;
        }
    }
    public bool GetisFire()
    {
        return isFire;
    }
}
