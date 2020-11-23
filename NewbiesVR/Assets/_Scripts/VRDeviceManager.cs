using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class VRDeviceManager : MonoBehaviour
{
    public GameObject PCUI;
    public GameObject PCCam;
    public GameObject VRUI;
    public GameObject VRCam;

    private void Start()
    {
        PCUI.SetActive(false);
        PCCam.SetActive(false);
        VRUI.SetActive(false);
        VRCam.SetActive(false);
    }
    private void Update()
    {
        Debug.Log(OpenVR.IsHmdPresent());
        if (OpenVR.IsHmdPresent() && (SteamVR.instance != null))
        {
            PCUI.SetActive(false);
            PCCam.SetActive(false);
            VRUI.SetActive(true);
            VRCam.SetActive(true);
        }
        else
        {
            VRUI.SetActive(false);
            VRCam.SetActive(false);
            PCUI.SetActive(true);
            PCCam.SetActive(true);
        }
    }
}
