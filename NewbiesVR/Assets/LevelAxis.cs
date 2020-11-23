using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAxis : MonoBehaviour
{
    public GameObject lever;
    float LevelX;
    float Axis;
    private void Update()
    {
        if (lever.transform.localRotation.eulerAngles.x <= 180)
        {
            LevelX = Mathf.Clamp(lever.transform.localRotation.eulerAngles.x,0,45);
        }
        else if (lever.transform.localRotation.eulerAngles.x > 180)
        {
            LevelX = Mathf.Clamp(lever.transform.localRotation.eulerAngles.x - 360,-45,0);
        }
        Axis = LevelX / 45;
    }
    public float GetLeverAxis()
    {
        return Axis;
    }
}
