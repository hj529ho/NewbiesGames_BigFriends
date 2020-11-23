using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneCartButton : MonoBehaviour
{
    bool isActivate = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Minecart")
        {
            isActivate = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Minecart")
        {
            isActivate = false;
        }
    }
    public bool GetisActivate()
    {
        return isActivate;
    }
}
