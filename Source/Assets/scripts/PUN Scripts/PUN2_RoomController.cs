﻿using UnityEngine;
using Photon.Pun;

public class PUN2_RoomController : MonoBehaviourPunCallbacks
{

    //Player instance prefab, must be located in the Resources folder
    public GameObject playerPrefab;
    public GameObject playerPrefab2;
    //Player spawn point
    public Transform spawnPoint, spawnPoint2;

    [HideInInspector]
    public GameObject player1;
    [HideInInspector]
    public GameObject player2;


    public PlayerManager playerManager;

    public GameObject item;

    void Start()
    {

        if (PhotonNetwork.CurrentRoom == null)
        {
            Debug.Log("Is not in the room, returning back to Lobby");
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameLobby");
            return;
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            //player1 = 
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, Quaternion.identity, 0);                //as GameObject;
          /* player1.tag = "Player";
           isPlayer1 = true;*/
        }
        else 
        {
            //player2 = 
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint2.position, Quaternion.identity, 0);
                //as GameObject;
            /*player2.tag = "Player2";
            isPlayer2 = true;*/
        }

    }

 
    void OnGUI()
    {
        if (PhotonNetwork.CurrentRoom == null)
            return;

        //Leave this Room
        if (GUI.Button(new Rect(5, 5, 125, 25), "Leave Room"))
        {
            PhotonNetwork.LeaveRoom();
        }

        //Show the Room name
        GUI.Label(new Rect(135, 5, 200, 25), PhotonNetwork.CurrentRoom.Name);

        //Show the list of the players connected to this Room
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            //Show if this player is a Master Client. There can only be one Master Client per Room so use this to define the authoritative logic etc.)
            string isMasterClient = (PhotonNetwork.PlayerList[i].IsMasterClient ? ": MasterClient" : "");
            GUI.Label(new Rect(5, 35 + 30 * i, 200, 25), PhotonNetwork.PlayerList[i].NickName + isMasterClient);
        }
    }

    public override void OnLeftRoom()
    {
        //We have left the Room, return back to the GameLobby
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameLobby");
    }
}