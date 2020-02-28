using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class QuickStartRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int waitingRoomSceneIndex; //Number for the build index to the multiplay scene

    public override void OnEnable()
    {
        //register to photon callback
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        //unregister to photon callback
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom() // Callback function for when we successfully create or join a room
    {
        //called when our player joins the room
        //load into waiting room scene
        Debug.Log("Joined Room");
        SceneManager.LoadScene(waitingRoomSceneIndex);
    }
}
