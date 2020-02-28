using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class QuickStartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject quickStartButton; // button for create / join a game.
    [SerializeField]
    private GameObject quickCancelButton; // button for stop searching a game.
    [SerializeField]
    private int RoomSize; //Manual set the number of player in the room.


    public override void OnConnectedToMaster() // callback function for when the first connection is established.
    {
        PhotonNetwork.AutomaticallySyncScene = true; // sync the scene from the master client to the other clients.
        quickStartButton.SetActive(true);
    }

    public void QuickStart() // paired to the quickstart button
    {
        quickStartButton.SetActive(false);
        quickCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom(); // first tries to join an existing room
        Debug.Log("Quick start");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)// Callback function for re-create a room if all the rooms are not available
    {
        Debug.Log("Failed to join a room");
        CreateRoom();
    }

    void CreateRoom() // create our own room
    {
        Debug.Log("Creating a room now");
        int randomRoomNumber = Random.Range(0, 10000); //creating a random room
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)RoomSize };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps); // attempting to create a new room
        Debug.Log(randomRoomNumber);
    }

    public override void OnCreateRoomFailed(short returnCode, string message) // Callback function for create room failed
    {
        Debug.Log("Failed to create room... trying again");
        CreateRoom();
    }

    public void QuickCancel() // paired to the cancel button, for stop looking for a room.
    {
        quickCancelButton.SetActive(false);
        quickStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

}
