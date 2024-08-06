using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    private Inventory inventory;
    public GameObject inventoryGameObject;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            inventory = inventoryGameObject.GetComponent<Inventory>();
            inventory.PickUpFromFloor(other.gameObject);
        }
    }
}
