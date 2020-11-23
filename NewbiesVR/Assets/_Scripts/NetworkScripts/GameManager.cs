using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;

using Photon.Pun;
using Photon.Realtime;
public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject Cam;
    public GameObject VRCam;
    public Transform VRPlayer;
    public Transform PCPlayer;
    public LiftMove lift;
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            if (OpenVR.IsHmdPresent() && (SteamVR.instance != null))
            {
                PhotonNetwork.Instantiate("NetworkPlayer_VR", VRPlayer.position, VRPlayer.rotation);
                Instantiate(VRCam, VRPlayer.position, VRPlayer.rotation);
                lift.GetOwnership();

            }
            else if (SteamVR.instance == null)
            {
                PhotonNetwork.Instantiate("Player2", PCPlayer.position + Vector3.up * 6,PCPlayer.rotation);
                Instantiate(Cam);
            }
        }
    }
    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
            LoadStage();
        }
    }
    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


            LoadStage();
        }
    }
    void LoadStage()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            //PhotonNetwork.DestroyAll();
            PhotonNetwork.LoadLevel("Matching");
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("Lobby");
        }
    }
    public override void OnLeftRoom()
    {
      
        //PhotonNetwork.DestroyAll();
        SceneManager.LoadScene(0);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
