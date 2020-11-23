using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class Teleport : MonoBehaviour
{
    public GameObject Pointer;
    public GameObject NoPointer;
    public SteamVR_Action_Boolean TeleportAction;

    private SteamVR_Behaviour_Pose pose = null;
    private bool HasPosition = false;
    private bool IsTelePorting = false;
    private bool start = false;
    private float FadeTime = 0.5f;

    private void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        Pointer.SetActive(false);
        NoPointer.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        //pointer
        HasPosition = UpdatePointer();

        if (start)
        {
            if (HasPosition)
            {
                Pointer.SetActive(true);
                NoPointer.SetActive(false);
            }
            else
            {
                Pointer.SetActive(false);
                NoPointer.SetActive(true);
            }
        }
        if (TeleportAction.GetStateDown(pose.inputSource))
        {
            
            start = true;

        }
        else if (TeleportAction.GetStateUp(pose.inputSource))
        {
                start = false;
                TryTeleport();
                Pointer.SetActive(false);
                NoPointer.SetActive(false);
        }

    }
    private void TryTeleport()
    {
        //Check for vlid positio and if already teleporting
        if (!HasPosition || IsTelePorting)
        {
            Debug.Log("Teleport Failed : HasPosition (" + HasPosition+"), IsTeleporting ("+IsTelePorting+")");
            return;
        }
        Transform cameraRig = SteamVR_Render.Top().origin;
        Vector3 headPosition = SteamVR_Render.Top().head.position;
        Vector3 groundPosition = new Vector3(headPosition.x, cameraRig.position.y, headPosition.z);
        Vector3 traslateVector = Pointer.transform.position - groundPosition;

        StartCoroutine(MoveRig(cameraRig, traslateVector));
    }
    private IEnumerator MoveRig(Transform cameraRig, Vector3 translation)
    {

        IsTelePorting = true;
        SteamVR_Fade.Start(Color.black, FadeTime, true);
        yield return new WaitForSeconds(FadeTime);
        cameraRig.position += translation;

        SteamVR_Fade.Start(Color.clear, FadeTime, true);
        IsTelePorting = false;
        yield return null;
    }
    private bool UpdatePointer()
    {
        //Ray from controller;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag != "Player")
            {
               
                NoPointer.transform.position = hit.point;
                Pointer.transform.position = hit.point;
                if (hit.transform.gameObject.tag == "Teleportable")
                {
                    Debug.Log(hit.transform.gameObject);
                    return true;
                }
            }
        }
        return false;
    }
}
