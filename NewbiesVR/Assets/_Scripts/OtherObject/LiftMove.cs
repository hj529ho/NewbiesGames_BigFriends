using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class LiftMove : MonoBehaviour
{
    public LevelAxis axis;
    bool Activate = false;
    PhotonView view;
    public BrazierManager Brazier1;
    public BrazierManager Brazier2;
    public BrazierManager Brazier3;
    public BrazierManager Brazier4;
    public RuneCartButton RuneCartButton;
    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (!view.IsMine)
        {
            return;
        }
        Activate = Brazier1.GetisFire() && Brazier2.GetisFire() && Brazier3.GetisFire() && Brazier4.GetisFire();
        if (Activate)
        {
            if (transform.position.y <= 1.3f)
            {
                transform.position += Vector3.up * 0.02f;
            }
            if (!RuneCartButton.GetisActivate() && transform.position.y <= 5.8f && transform.position.y >= 1.3f)
            {
                transform.position += Vector3.up * axis.GetLeverAxis() * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 1.3f, 5.8f), transform.position.z);
            }
            if (RuneCartButton.GetisActivate()&&transform.position.y <= 10.75f && transform.position.y >= 1.3f)
            {
                transform.position += Vector3.up * axis.GetLeverAxis()*Time.deltaTime;
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 1.3f, 10.75f), transform.position.z);
            }
        }
        else
        {
            if (transform.position.y >= 0)
            {
                transform.position -= Vector3.up * 0.01f;
            }
        }
        

    }

    public void GetOwnership()
    {
        view.RequestOwnership();
    }
    //Max 10.75
}
