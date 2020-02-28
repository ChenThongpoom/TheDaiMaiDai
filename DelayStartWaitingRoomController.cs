using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DelayStartWaitingRoomController : MonoBehaviourPunCallbacks
{
    /*This object must be attached to an object
     * in the waiting room scene of your project.*/

    //photon view for sending rpc that updates the timer
    private PhotonView myPhotonView;

    // scene navigation indexes
    [SerializeField]
    private int multiplayerSceneIndex;
    [SerializeField]
    private int menuSceneIndex;
    //number of the players in the room out of the total room size
    private int playerCount;
    private int roomSize;
    [SerializeField]
    private int maxPlayersToStart;
    private bool startingGame;

    //text variables for holding the displays for the player count
    [SerializeField]
    private Text playerCountDisplay;

    //text variables for display the user status.
    [SerializeField]
    private GameObject statusPrefab;
    public GameObject chatContainer;

    private void Start()
    {
        //initialize variables
        myPhotonView = GetComponent<PhotonView>();
        PlayerCountUpdate();

        //instantiate the player status
        GameObject go = Instantiate(statusPrefab, chatContainer.transform);
        go.GetComponentInChildren<Text>().text = "USER " + playerCount + " IS CONNECTED !!";
    }

    void PlayerCountUpdate()
    {
        //updates player count when players join the room
        //displays player count
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        playerCountDisplay.text = playerCount + " / " + roomSize;




    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //called whenever a new player joins the room
        PlayerCountUpdate();

        //instantiate the player status
        GameObject go = Instantiate(statusPrefab, chatContainer.transform);
        go.GetComponentInChildren<Text>().text = "USER " + playerCount + " IS CONNECTED !!";
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //called whenever a player leaves the room.
        PlayerCountUpdate();
    }

    private void Update()
    {
        WaitingForMorePlayers();
    }

    void WaitingForMorePlayers()
    {
        if (playerCount >= roomSize)
        {
            if (startingGame)
            {
                return;
            }
            StartGame();
        }

    }

    public void StartGame()
    {
        //Multiplayer scene is loaded to start the game
        startingGame = true;
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(multiplayerSceneIndex);
    }

    public void DelayCancel()
    {
        //public function paired to cancel button on waiting room scene
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuSceneIndex);
    }
}
