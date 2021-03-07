using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{

    public bool playerReady = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerReady = true;
        }
       
    }

     void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerReady = false;
        }
       
    }
}
