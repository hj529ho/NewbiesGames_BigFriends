using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObj : MonoBehaviour
{
    public GameObject LeverHandle;
    private LeverState CurrentState;
    bool onandoff = true;
    public enum LeverState
    {
        ON,
        Middle,
        OFF,
    }
    // Update is called once per frame
    void Update()
    {
        if(CurrentState == LeverState.ON)
        {
            LeverHandle.transform.localRotation = Quaternion.Euler(45,0,0);
        }  
        else if(CurrentState == LeverState.OFF)
        {
            LeverHandle.transform.localRotation = Quaternion.Euler(-45,0,0);            
        } 
        else
        {
            LeverHandle.transform.localRotation = Quaternion.Euler(0,0,0);
        }
    }
    public void Interact()
    {
        
        if(CurrentState == LeverState.ON)
        {
            CurrentState = LeverState.Middle;
            onandoff = false;
        }  
        else if(CurrentState == LeverState.OFF)
        {
            CurrentState = LeverState.Middle;
            onandoff = true;
        } 
        else
        {
            if(onandoff == true)
            {
                CurrentState = LeverState.ON;
            }
            else
            {
                CurrentState = LeverState.OFF;

            }
        }
    }
}
