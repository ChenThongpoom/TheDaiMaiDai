using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.SceneManagement;
using Photon.Realtime;


public class GameSetuoController : MonoBehaviour
{
    [SerializeField]
    private GameObject delayCancelButton; //button to exit the room
    [SerializeField]
    private int menuSceneIndex;

    void Start()
    {
        CreatePlayer(); // Create a networked player object for each player that loads into the multiplayerindex
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), new Vector3(0f,-0.8f,2.3f), Quaternion.AngleAxis(180,Vector3.up));
        delayCancelButton.SetActive(true);
    }

    public void DelayCancel() // paired to the cancel button, for disconnect the room
    {
        delayCancelButton.SetActive(false);
        Debug.Log("Player disconnected !!");
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuSceneIndex);
    }

    public void OnPlayerLeftRoom(Player otherPlayer)
    {
        // Destroy player object
        
        PhotonNetwork.DestroyPlayerObjects(otherPlayer);
    }
}
