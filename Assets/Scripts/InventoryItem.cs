using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image image;
    public GameObject inventorySlotObject;
    public int itemQuantity;

    [HideInInspector] public Transform parentAfterDrag;

    /**
     * Function to register before an inventory item is dragged. implemented in IBeginDragHandler.
     * @param eventData - current event data.
     */
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }

        // Disable raycasting until the item is set down again.
        Debug.Log("OnBeginDrag()");
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    /**
     * Function to register when an inventory item is being dragged. Implemented in IDragHandler
     * @param eventData - current event data.
     */
    public void OnDrag(PointerEventData eventData)
    {
        // Set position of inventory item to mouse position.
        transform.position = Input.mousePosition;
    }

    /**
     * Function to register when an inventory item has finished being dragged. Implemented in IEndDragHandler.
     * @param eventData - current event data.
     */
    public void OnEndDrag(PointerEventData eventdata)
    {
        Debug.Log("OnEndDrag()");

        // re-enable target to be 'ray-castable'.
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);

    }
}
