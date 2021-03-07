using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public int id;
    public bool isTrapdoor = false;
    public bool isGate = false;

    public GameObject door;
    MeshCollider doorCollider;
    MeshRenderer doorRenderer;

    public float tweenOpen;
    public float tweenClose;
    private void Start()
    {
        GameEvents.current.onDoorwayTriggerEnter += OnDoorwayOpen;
        GameEvents.current.onDoorwayTriggerExit += OnDoorwayClose;

        if (!isGate || !isTrapdoor)
        {
            doorCollider = door.GetComponent<MeshCollider>();
            doorRenderer = door.GetComponent<MeshRenderer>();
        }
        

    }


 
    private void OnDoorwayClose(int id)
    {
        if (id == this.id)
        {
            if (isTrapdoor == true)
            {
                LeanTween.moveLocalZ(gameObject, tweenClose, .2f);
            }
            else if (isGate == true)
            {
                LeanTween.moveLocalZ(gameObject, tweenClose, .2f);
            }
            else if (isTrapdoor == false && isGate == false)
            {
                doorCollider.enabled = true;
                //doorRenderer.enabled = true;
                LeanTween.rotateY(gameObject, tweenClose, 1f).setEaseInQuad();
            }
        }
    }
    private void OnDoorwayOpen(int id)
    {
        if (id == this.id)
        {
            if (isTrapdoor == true)
            {
                LeanTween.moveLocalZ(gameObject, tweenOpen, .2f);
            }
            else if (isGate == true)
            {
                LeanTween.moveLocalZ(gameObject, tweenOpen, .2f);
            }
            else if (isTrapdoor == false && isGate == false)
            {
                doorCollider.enabled = false;
                //doorRenderer.enabled = false;
                LeanTween.rotateY(gameObject, tweenOpen, 1f).setEaseOutQuad();
            }
        }
    }



    private void OnDestroy()
    {
        GameEvents.current.onDoorwayTriggerEnter -= OnDoorwayOpen;
        GameEvents.current.onDoorwayTriggerExit -= OnDoorwayClose;
    }
}
