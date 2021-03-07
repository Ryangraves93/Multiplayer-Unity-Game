using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class Chest : MonoBehaviour
{
    BoxCollider boxCollider;
    public GameObject hoverText;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameSparksManager.Instance.GetPlayerScore() >= 10 && Input.GetKeyDown(KeyCode.E))
            {
                GameSparksManager.Instance.RetrieveFromLootTable();
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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
        if (other.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            if (player.transform.Find("Text"))
            {
                player.transform.Find("Text").gameObject.SetActive(false);
            }
        }
    }

}
