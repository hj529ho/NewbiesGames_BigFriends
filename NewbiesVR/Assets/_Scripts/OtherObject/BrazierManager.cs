using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrazierManager : MonoBehaviour
{
    GameObject Fire;
    bool isFire = false;
    private void Awake()
    {
        Fire = transform.Find("FX_Fire").gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (isFire)
        {
            Fire.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Toach")
        {
            if (other.gameObject.GetComponent<ToachManager>().GetisFire())
            {
                isFire = true;
            }
        }
    }
    public bool GetisFire()
    {
        return isFire;
    }

}
