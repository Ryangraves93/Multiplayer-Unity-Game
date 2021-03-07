using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public bool isFirstTrapdoor = false;
    //public GameObject trapdoor;
    [HideInInspector]
    public Rigidbody rb;
    public int id;

    private bool triggered = false;
    bool playerTrigger = false;
    bool itemTrigger = false;

    public void Awake()
    {
        if (isFirstTrapdoor)
        {
            //rb = trapdoor.GetComponent<Rigidbody>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTrigger = true;
        }
        else if (other.CompareTag("Item"))
        {
            itemTrigger = true;
        }
        GameEvents.current.DoorwayTriggerEnter(id);
         if (isFirstTrapdoor)
         {
            rb.constraints = RigidbodyConstraints.None;
         }
        
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTrigger = false;
        }
        else if (other.CompareTag("Item"))
        {
            itemTrigger = false;
        }
        if (itemTrigger == false && playerTrigger == false)
        {
            GameEvents.current.DoorwayTriggerExit(id);
        }
        
    }
}
