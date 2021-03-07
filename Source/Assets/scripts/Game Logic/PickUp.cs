using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    Rigidbody rb;
    public Transform respawn, respawn2;
    public GameObject hoverText;
    private void Start()
    {

        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {

        
        if (other.CompareTag("Respawn"))
        {
            transform.position = respawn.position;
            transform.rotation = respawn.rotation;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        else if (other.CompareTag("Respawn2"))
        {
            transform.position = respawn2.position;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        else if (other.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            if (player.transform.Find("Text"))
            {
                player.transform.Find("Text").gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject player = other.gameObject;
        if (player.transform.Find("Text"))
        {
            player.transform.Find("Text").gameObject.SetActive(true);
        }
    }


}
