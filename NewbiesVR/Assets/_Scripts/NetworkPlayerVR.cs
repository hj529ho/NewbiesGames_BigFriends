using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Photon.Pun;

public class NetworkPlayerVR : MonoBehaviourPunCallbacks
{
    public Transform Head;
    public Transform LeftHand;
    public Transform RightHand;

    private Transform cameraRig;
    private Transform LeftHandTransform;
    private Transform RightHandTransform;
    private GameObject vrCam;

    public GameObject HeadGameObject;
    public GameObject LeftHandGameObject;
    public GameObject RightHandGameObject;
    private void Start()
    {
        vrCam = GameObject.Find("[CameraRig](Clone)");
        if (vrCam)
        {
            cameraRig = vrCam.transform.Find("Camera (eye)");
            LeftHandTransform = vrCam.transform.Find("vr_glove_left_model_slim").transform;
            RightHandTransform = vrCam.transform.Find("vr_glove_right_model_slim").transform;
        }
    }
    private void Update()
    {
        if (photonView.IsMine)
        {

            HeadGameObject.gameObject.SetActive(false);
            LeftHandGameObject.gameObject.SetActive(false);
            RightHandGameObject.gameObject.SetActive(false);
            MapPosition(LeftHand, LeftHandTransform);
            MapPosition(RightHand, RightHandTransform);

            Head.position = cameraRig.transform.position;
            Head.rotation = cameraRig.transform.rotation;
        }
    }
    void MapPosition(Transform target, Transform form)
    {
        target.position = form.position;
        target.rotation = form.rotation;
        
    }
}
