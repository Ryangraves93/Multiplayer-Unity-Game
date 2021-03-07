using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class PlayerScript : MonoBehaviourPunCallbacks
{

    Collider barrel;
    public Transform destination;
    bool itemPickedUp = false;
    bool inRange = false;
    public bool fallingBarrel = false;
    public float thrust = .8f;
    Rigidbody rb;
    

    public GameObject pickupText;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
       

            
        } 

        if (Input.GetKeyDown(KeyCode.E) && itemPickedUp == false && inRange == true)
        {
            PickUpItem();
        }

        else if (Input.GetKeyDown(KeyCode.E) && itemPickedUp == true)
        {
            DropItem();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            inRange = true;
            barrel = other;
            pickupText.SetActive(true);
        }

     
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            inRange = false;
            pickupText.SetActive(false);
        }
    }

    void PickUpItem()
    {
        barrel.attachedRigidbody.useGravity = false;
        barrel.GetComponent<PhotonView>().RequestOwnership();
  
        barrel.transform.position = destination.position;
        barrel.transform.parent = destination.transform;
        Physics.IgnoreCollision(gameObject.GetComponent<CapsuleCollider>(), barrel, true);

        barrel.attachedRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        itemPickedUp = true;
    }

    void DropItem()
    {
        barrel.attachedRigidbody.useGravity = true;
        barrel.transform.parent = null;
        barrel.attachedRigidbody.freezeRotation = false;
        Physics.IgnoreCollision(gameObject.GetComponent<CapsuleCollider>(), barrel, false);
        barrel.attachedRigidbody.constraints = RigidbodyConstraints.None;
        itemPickedUp = false;
        barrel = null;
    }
}
