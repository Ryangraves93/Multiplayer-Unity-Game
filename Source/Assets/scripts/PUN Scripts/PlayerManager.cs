using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PUN2_RoomController roomController;
    [HideInInspector]
    public GameObject playerRef;

    PlayerScript player1;
    PlayerScript player2;

    bool player1Ready = false;
    bool player2Ready = false;


    // Update is called once per frame
    void Update()
    {
        /*if (player1 == null && roomController.isPlayer1 == true)
        {
            player1 = roomController.player1.GetComponent<PlayerScript>();
        }

        if (player2 == null && roomController.isPlayer2 == true)
        {
            player2 = roomController.player2.GetComponent<PlayerScript>();
        }*/

        /*  else if (player2 == null)
          {
              player2 = roomController.player2.GetComponent<PlayerScript>();
          }*/


        //Debug.Log("Player 1 ready - " + player1.playerReady);
        //Debug.Log("Player 2 ready - " + player2.playerReady);
    }

    
}
