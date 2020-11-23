using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{
    GameObject CamRig;
    GameObject InteractiveObject;
    GameObject PickupObject;
    CharacterController controller;
    Vector3 rig;
    Vector3 Axis;
    Vector3 MoveDirection;
    float H;
    float V;
    float Gravity = 1f;
    float JumpForce = 0.3f;
    bool IsSomethingCarryUp = false;

    float time = 0;
    public float characterSpeed = 2f;
    Vector3 notwork = new Vector3(0, -0.1f, 0);
    Animator anim;
    RaycastHit[] RaycastHits;
    PhotonView view;
    // Grabable Grab;
    // public TextMesh NickName;
    // [SerializeField]private TextMeshProUGUI NickName;
    float shortDistace;
    public PickUpManager pickUpManager;

    private void Start()
    {
        // // SetName();
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        view = GetComponent<PhotonView>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        CamRig = GameObject.Find("TPCameraRig(Clone)");
        MoveDirection = Vector3.zero;

    }
    private void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        if (IsSomethingCarryUp && !PickupObject)
        {
            PickUpLost();
        }
        if (IsSomethingCarryUp && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("joystick button 2")))
        {
            PutDown();
        }
        else if (!IsSomethingCarryUp && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("joystick button 2")))
        {
            PickUp(GetNearestInteractable(pickUpManager.GetListInteractable()));
        }
        if (!IsSomethingCarryUp&& (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown("joystick button 3")))
        {
            GameObject closer = GetNearestInteractable(pickUpManager.GetListSwitch());

            closer.GetComponent<RunePillar>().SetIsActive();
            Debug.Log("Switch");
        }
    }
    
    private void FixedUpdate()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        if (controller.isGrounded)
        {
            MoveDirection.y = 0;
        
             if (Input.GetKey(KeyCode.Space)||Input.GetKey("joystick button 0"))
            {
                    anim.SetBool("Jump",true);
                    MoveDirection.y = JumpForce;
            }
        }
        if(!controller.isGrounded)
        {
            anim.SetBool("Jump",false);

            MoveDirection.y -= Gravity * Time.deltaTime;
        }
              if(MoveDirection != Vector3.zero)
        {
            controller.Move(MoveDirection);
            // Debug.Log(MoveDirection);
        }

    
        H = Input.GetAxis("Horizontal");
        V = Input.GetAxis("Vertical");

        rig = CamRig.transform.rotation.eulerAngles;
        //카메라로 보고있는 방향
        rig = new Vector3(0, rig.y, 0);
        //컨트롤러로부터 입력받은 값의 방향 (2D방향)
        Axis = new Vector3(H, 0, V);
        //Axis를 Rig.y각도만큼 회전. 회전된 Axis는 로컬좌표임.
        Axis = Quaternion.Euler(rig) * Axis;
        Axis = Vector3.ClampMagnitude(Axis, 1.0f);
        MoveDirection.x = characterSpeed * Axis.x;
        MoveDirection.z = characterSpeed * Axis.z;
        // MoveDirection.y = VerticalVellocity;
        anim.SetFloat("Speed",Axis.magnitude);

        if(Input.GetKey(KeyCode.P)||Input.GetKey("joystick button 5"))
        {
            CamRig.transform.rotation = Quaternion.Lerp(CamRig.transform.rotation,transform.rotation,Time.deltaTime*13);
        }
        
        if (!(H == 0 && V == 0))
        {
            //Axis를 캐릭터의 위치까지 평행이동 후 캐릭터가 바라보게 만든다.
            transform.LookAt(Axis + transform.position);
        }

        //Follow cam to player
        CamRig.transform.position = Vector3.Lerp(CamRig.transform.position,this.transform.position + Vector3.up *1.5f,Time.deltaTime*13);
    }

    private GameObject GetCloserObject()
    {
        GameObject CloserObject;
        RaycastHits = Physics.SphereCastAll(transform.position, 15, Vector3.up, 0f, 1 << LayerMask.NameToLayer("Default"));
        if (RaycastHits.GetLongLength(0) != 0)
        {
            shortDistace = (transform.position - RaycastHits[0].transform.position).sqrMagnitude;
            CloserObject = RaycastHits[0].transform.gameObject;
        }
        else
        {
            CloserObject = null;
        }
        foreach (RaycastHit raycasthit in RaycastHits)
        {
            float Distance = (transform.position - raycasthit.transform.position).sqrMagnitude;
            if (Distance < shortDistace)
            {
                shortDistace = Distance;
                CloserObject = raycasthit.transform.gameObject;
            }
        }
        if (CloserObject)
        {
            Debug.Log(CloserObject.name + " is Closer");
        }
        return CloserObject;
    }
    private void SetName()
    {

            // NickName.text = "empty_Nickname";

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.name == "HitBox")
        {
            MoveDirection = (transform.position - collision.transform.position) * 3;
            IsSomethingCarryUp = false;
            anim.SetBool("Pick", IsSomethingCarryUp);
            if (PickupObject)
            {
                PutDown();
            }
        }
    }
    private void PickUp(GameObject obj)
    {
        if (IsSomethingCarryUp)
        {
            return;
        }
        PickupObject = obj;
        PickupObject.GetComponent<GrablePC>().OwershipChage();
        PickupObject.transform.parent = transform;
        
        IsSomethingCarryUp = true;
        PickupObject.transform.GetComponent<Rigidbody>().isKinematic = IsSomethingCarryUp;
        view.RPC("Parenting_RPC", RpcTarget.Others, PickupObject.GetPhotonView().ViewID, GameObject.Find("Player2(Clone)").GetPhotonView().ViewID);
        InvokeRepeating ("PickUpMove",0.6f,0.01f);
        // PickupObject.transform.localPosition = new Vector3(0,0.5f,0.5f);
        anim.SetBool("Pick",IsSomethingCarryUp);
        Debug.Log("PickUp");   

    }
    private void PickUpMove()
    {
        time += Time.deltaTime;
        if(time < 1.0f){
        PickupObject.transform.localPosition = Vector3.Lerp(PickupObject.transform.localPosition,new Vector3(0,0.7f,0.7f),Time.deltaTime*2);
            PickupObject.transform.rotation = Quaternion.Lerp(PickupObject.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 5);
        }
        else 
        {
             CancelInvoke ("PickUpMove");
        }
    }
    private void PutDown()
    {
        if (!IsSomethingCarryUp)
        {
            return;
        }
        IsSomethingCarryUp = false;
        time = 0;
        PickupObject.transform.parent = null;
        pickUpManager.ClearListInsteractable();
        view.RPC("DisParent_PPC", RpcTarget.Others, PickupObject.GetPhotonView().ViewID);
        PickupObject.transform.GetComponent<Rigidbody>().isKinematic = IsSomethingCarryUp;
        anim.SetBool("Pick",IsSomethingCarryUp);
        Debug.Log("PutDown");   
    }
    private void PickUpLost()
    {
        if (!IsSomethingCarryUp)
        {
            return;
        }
        IsSomethingCarryUp = false;
        time = 0;
        PickupObject = null;
        pickUpManager.ClearListInsteractable();
        anim.SetBool("Pick", IsSomethingCarryUp);
        Debug.Log("Lost");
    }
    
    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(transform.position,15);
        //Gizmos.DrawWireSphere(transform.position + transform.forward, 1f);
    }

    [PunRPC]
    public void Parenting_RPC(int child, int parent)
    {
        PhotonView CurrentChild = PhotonView.Find(child);
        PhotonView CurrentParent = PhotonView.Find(parent);
        CurrentChild.transform.parent = CurrentParent.transform;
        CurrentChild.transform.GetComponent<Rigidbody>().isKinematic = true;
        //view.RPC("Parenting_RPC", RpcTarget.Others, PickupObject.GetPhotonView().ViewID, GameObject.Find("Player2(Clone)").GetPhotonView().ViewID);
    }
    [PunRPC]
    public void DisParent_PPC(int child)
    {
        PhotonView CurrentChild = PhotonView.Find(child);
        CurrentChild.transform.parent = null;
        CurrentChild.transform.GetComponent<Rigidbody>().isKinematic = false;

    }
    private GameObject GetNearestInteractable(List<GameObject> objects)
    {
        GameObject nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach (GameObject interactable in objects)
        {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interactable;
            }
        }
        return nearest;
    }
}
