using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CamController : MonoBehaviour
{

    public float Mouse_rot_speed = 5.0f;
    public float Joy_rot_speed = 2.0f;
    public float Joy_rot_speed_vertical = 1.0f;

    public Camera MainCamera;

    float CameraMaxDistance = 5f;
    float CameraZeroPointWidth = 0;
    float CameraZeroPointHight = 3f;
    float camera_pitch;
    float mouseX;
    float mouseY;
    float RightHorizental;
    float RightVertical;
    RaycastHit hit;

    //Vector3 CameraZeroPosition;
    //private void Start()
    //{
    //    CameraZeroPosition = new Vector3(0, CameraZeroPointHight, CameraZeroPointWidth);
    //}
    private void Update()
    {
        Debug.DrawRay(transform.position, MainCamera.transform.position-transform.position, Color.red);
        Debug.DrawRay(transform.position, (MainCamera.transform.position-transform.position).normalized * CameraMaxDistance, Color.blue);

        if (Physics.Raycast(transform.position, (MainCamera.transform.position - transform.position).normalized, out hit, CameraMaxDistance))
        {
            if (hit.transform.gameObject.tag != "Player")
            {
                MainCamera.transform.localPosition = Vector3.Lerp(MainCamera.transform.localPosition, MainCamera.transform.localPosition + Vector3.forward, Time.deltaTime * 10);
                MainCamera.transform.position = hit.point;
            }
        }
        else
        {
            MainCamera.transform.localPosition = Vector3.Lerp(MainCamera.transform.localPosition,new Vector3(0,0, -CameraMaxDistance),Time.deltaTime*5f);
            //Debug.DrawRay(transform.position, MainCamera.transform.position, Color.red);        
        }

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        RightHorizental = Input.GetAxis("RightHorizontal");
        RightVertical = Input.GetAxis("RightVertical");

        camera_pitch = transform.eulerAngles.x > 180 ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;


        transform.Rotate(Vector3.up * mouseX * Mouse_rot_speed, Space.World);
        transform.Rotate(Vector3.up * RightHorizental * Joy_rot_speed, Space.World);
        if (camera_pitch <= 30 && camera_pitch >= -30)
        {
           transform.Rotate(Vector3.left * mouseY * Mouse_rot_speed, Space.Self);
            transform.Rotate(Vector3.left * RightVertical * Joy_rot_speed_vertical, Space.Self);
            camera_pitch = transform.eulerAngles.x > 180 ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;

            if (camera_pitch > 30)
            {
               transform.rotation = Quaternion.Euler(29.999f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            }
            else if (camera_pitch < -30)
            {
               transform.rotation = Quaternion.Euler(-30f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            }
        }
        //if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown("joystick button 5"))
        //{
        //    transform.rotation = Quaternion.Euler(0, 0, 0);
        //}
        //transform.position = PlayerCharacter.transform.position + Vector3.up * 3;

    }
}