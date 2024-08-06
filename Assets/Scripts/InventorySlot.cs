using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    /**
     * Registers when an inventory item is dropped.
     * @param eventData - current event data (required for handler interface).
     */
    public void OnDrop(PointerEventData eventData) { 
        if (transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
    }
}
