using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VRUI : MonoBehaviour
{
    public SteamVR_Action_Boolean m_GrabAction;
    private SteamVR_Behaviour_Skeleton m_Skeleton = null;
    private SteamVR_Behaviour_Pose m_Pose = null;

    private Interactable m_CurrentInteractable = null;
    public List<Interactable> m_ContactInteractable = new List<Interactable>();
    RaycastHit hit;

    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Skeleton = GetComponent<SteamVR_Behaviour_Skeleton>();
    }
    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(m_Pose.poseAction.localPosition, Vector3.forward, out hit, 15f);

        if (hit.collider.gameObject.tag == "VRUI")
        {
            if (m_GrabAction.GetStateDown(m_Pose.inputSource))
            {
                print(m_Skeleton.inputSource + "Trigger Down");
                //hit.collider.gameObject.GetComponent<Button>().
            }
        }

     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("VRUI"))
            return;

        m_ContactInteractable.Add(other.gameObject.GetComponent<Interactable>());
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("VRUI"))
            return;

        m_ContactInteractable.Remove(other.gameObject.GetComponent<Interactable>());
    }

    void Click()
    { 
    
    }
}
