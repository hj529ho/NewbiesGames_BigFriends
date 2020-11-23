using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class Hand : MonoBehaviour
{

    public SteamVR_Action_Boolean GrabAction;
    public SteamVR_Action_Boolean BombRemoteAction;
    private SteamVR_Behaviour_Pose m_Pose = null;
    private SteamVR_Behaviour_Skeleton m_Skeleton = null;
    private FixedJoint m_Joint = null;
    //Bomb bomb;

    private Interactable m_CurrentInteractable = null;
    public List<Interactable> m_ContactInteractable = new List<Interactable>();

    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Skeleton = GetComponent<SteamVR_Behaviour_Skeleton>();
        m_Joint = GetComponent<FixedJoint>();
        //bomb = GameObject.Find("Bomb").GetComponent<Bomb>();

    }
    // Update is called once per frame
    void Update()
    {
        if (GrabAction.GetStateDown(m_Skeleton.inputSource))
        {
            print(m_Skeleton.inputSource + "Trigger Down");
            PickUp();
        
        }
        if (GrabAction.GetStateUp(m_Skeleton.inputSource))
        {
            print(m_Skeleton.inputSource + "Trigger Up");
            Drop();          
        }
        if (m_CurrentInteractable&&m_CurrentInteractable.gameObject.name == "RemoteController")
        {
            if (BombRemoteAction.GetStateDown(m_Skeleton.inputSource))
            {
                m_CurrentInteractable.gameObject.GetComponent<BombRemoteController>().Remote();
                //bomb.Explode();
            }
        }
        GetNearestInteractable();
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("InteractiveObjects"))
            return;

        m_ContactInteractable.Add(other.gameObject.GetComponent<Interactable>());
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("InteractiveObjects"))
            return;

        m_ContactInteractable.Remove(other.gameObject.GetComponent<Interactable>());
    }
    public void PickUp()
    {
        m_CurrentInteractable = GetNearestInteractable();
        if (!m_CurrentInteractable)
        {
            Lost();
            return;
        }
        if (m_CurrentInteractable.m_ActiveHand)
            m_CurrentInteractable.m_ActiveHand.Drop();
        m_CurrentInteractable.OwnerShipChange();
        m_CurrentInteractable.ChangeKinematic_true();
        m_CurrentInteractable.transform.position = transform.position;

        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targetBody;
        m_CurrentInteractable.m_ActiveHand = this;

       
    }
    public void Lost()
    {

        m_Joint.connectedBody = null;
        m_CurrentInteractable = null;
        m_ContactInteractable.Clear();
    }
    public void Drop()
    {
        if (!m_CurrentInteractable)
        {
            Lost();
            return;
        }
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        m_CurrentInteractable.ChangeKinematic_false();
        targetBody.velocity = m_Pose.GetVelocity() *    6;
        targetBody.angularVelocity = m_Pose.GetAngularVelocity();
        m_Joint.connectedBody = null;
        m_CurrentInteractable.m_ActiveHand = null;
        m_CurrentInteractable = null;

    }
    private Interactable GetNearestInteractable()
    {
        Interactable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;
        if (m_ContactInteractable.Count == 0)
        {
            return null;
        }
        foreach (Interactable interactable in m_ContactInteractable)
        {
            if (!interactable)
            {
                Debug.LogWarning("interactable is missing");
                return null;
            }
            distance = (interactable.transform.position - transform.position).sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interactable;
            }
        }
        if (nearest)
        {
            nearest.OutlineOn();
        }
        return nearest;
    }
}
