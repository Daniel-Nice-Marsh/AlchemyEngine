using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject parentObject;
    public GameObject prefab;
    public GameObject MainInventoryGroup;
    public GameObject ShowMainInventoryButton;
    int inventorySlotNumber;

    private bool inventoryActivated = false;
    
    // Start is called before the first frame update
    void Start()
    {
        // Initialise inventory slot number.
        inventorySlotNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Constantly poll and assign inventory slot to key pressed.
        if (Input.GetKey(KeyCode.Alpha1))
        {
            inventorySlotNumber = 0;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            inventorySlotNumber = 1;
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            inventorySlotNumber = 2;
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            inventorySlotNumber = 3;
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            inventorySlotNumber = 4;
        }
        else if (Input.GetKey(KeyCode.Delete))
        {
            DeleteStack();
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            dropItemOnTheFloor();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // also need to disabale the inventory button.
            if (!inventoryActivated)
            {
                MainInventoryGroup.SetActive(true);
                inventoryActivated = true;
                ShowMainInventoryButton.SetActive(false);
            }
            else
            {
                MainInventoryGroup.SetActive(false);
                inventoryActivated = false;
                ShowMainInventoryButton.SetActive(true);
            }
        }
        else
        {
            // do nothing.
        }

        // Returns transform child at given index [1].
        Transform inventorySlotTransformPos = transform.GetChild(inventorySlotNumber);

        // Return the child of this object at the given index (5).
        Transform inventorySlotIndicatorPos = transform.GetChild(5);

        // if we're currently dealing with the hotbar.
        if (transform.name == "Hotbar")
        {
            inventorySlotIndicatorPos.position = new Vector3(inventorySlotTransformPos.position.x, inventorySlotTransformPos.position.y + 40, inventorySlotTransformPos.position.z);
        }
    }

    /**
    * Function to move items that are collided with into inventory.
    * @param ItemToPickUp - the other gameobject the player walked over.
    * @returns - nothing.
    */
    public void PickUpFromFloor(GameObject ItemToPickUp)
    {
        // 'transform' of the object the script itself is attached to i.e. 'Hotbar'.
        foreach (Transform inventorySlot in transform)
        {
            // if the inventory slot has a child. (is full).
            if (inventorySlot.childCount > 0)
            {
                // get the object in the slot.
                GameObject childOfInventorySlot = inventorySlot.GetChild(0).gameObject;

                // if existing item in slot and gameobject same and gameobject type match. 
                if ((inventorySlot.childCount > 0) && (childOfInventorySlot.GetComponent<InventoryItem>().inventorySlotObject == ItemToPickUp.GetComponent<ItemScript>().myPrefab))
                {
                    //Check number of items is less than max stack
                    if (childOfInventorySlot.GetComponent<InventoryItem>().inventorySlotObject.GetComponent<ItemScript>().maxStack >= childOfInventorySlot.GetComponent<InventoryItem>().itemQuantity + ItemToPickUp.GetComponent<ItemScript>().quantity)
                    {
                        childOfInventorySlot.GetComponent<InventoryItem>().itemQuantity++;

                        // Destroy item on the floor.
                        Destroy(ItemToPickUp);
                        break;
                    }
                }
            }

            // if the inventory slot is empty (slot has no children).
            if (inventorySlot.childCount == 0)
            {
                // create blank object in slot.
                GameObject newObject = Instantiate(prefab, inventorySlot.transform);

                // render image of gameobject picked up.
                newObject.GetComponent<Image>().sprite = ItemToPickUp.GetComponent<SpriteRenderer>().sprite;

                // get copy of prefab of item picked up to put in new inv item.
                newObject.GetComponent<InventoryItem>().inventorySlotObject = ItemToPickUp.GetComponent<ItemScript>().myPrefab;

                newObject.GetComponent<InventoryItem>().itemQuantity = ItemToPickUp.GetComponent<ItemScript>().quantity;

                Destroy(ItemToPickUp);
                break;
            }

        }
    }

    /**
     * when the function is called it returns the prefab in the currently
     * selected inventory slot.
     * @param - none.
     * @returns - nothing.
     */
    public GameObjectAndInt SelectInventorySlot()
    {
        GameObjectAndInt returnObject = new GameObjectAndInt();
        if (transform.GetChild(inventorySlotNumber).childCount != 0)
        {
            var inventoryItem = transform.GetChild(inventorySlotNumber).GetChild(0);

            if (inventoryItem != null)
            {
                returnObject.Item = inventoryItem.GetComponent<InventoryItem>().inventorySlotObject;
                returnObject.Int = inventoryItem.GetComponent<InventoryItem>().itemQuantity;

                return returnObject;
            }
            else
            {
                return returnObject;
            }
        }
        else
        {
            return returnObject;
        }
    }

    /**
     * Removes items from slot by 1 unless
     * last item in slot in which case delete gameobject.
     * @param - none.
     * @returns - nothing.
     */
    public void RemoveFromInventory()
    {
        Debug.Log("RemoveFromInventory");

        Transform inventorySlot = transform.GetChild(inventorySlotNumber);

        GameObject childOfInventorySlot = inventorySlot.GetChild(0).gameObject;

        if (transform.GetChild(inventorySlotNumber).childCount != 0)
        {
            if (childOfInventorySlot.GetComponent<InventoryItem>().itemQuantity > 1)
            {
                childOfInventorySlot.GetComponent<InventoryItem>().itemQuantity--;
            }
            else
            {
                Destroy(transform.GetChild(inventorySlotNumber).GetChild(0).gameObject);
            }

        }
    }

    /**
     * When the player hits the Del key,
     * the whole item stack shall be removed
     * for the currently selected inventory.
     * @param - none.
     * @returns - nothing.
     */
    public void DeleteStack()
    {
        if (transform.GetChild(inventorySlotNumber).childCount != 0)
        {
            Destroy(transform.GetChild(inventorySlotNumber).GetChild(0).gameObject);
        }
    }


    public void dropItemOnTheFloor()
    {
        GameObjectAndInt dropObject = SelectInventorySlot();
        if (dropObject.Item != null && dropObject.Int > 0)
        {
            GameObject newObject = Instantiate(dropObject.Item);
            newObject.GetComponent<ItemScript>().myPrefab = dropObject.Item;
            newObject.GetComponent<ItemScript>().quantity = dropObject.Int;

            DeleteStack();
            if(parentObject != null)
            {
                Vector3 placePos = parentObject.transform.position;
                placePos.x += 0.25f * parentObject.GetComponent<MovementScript>().direction.x;
                //placePos.y += 0.1f * parentObject.GetComponent<MovementScript>().direction.y;
                placePos.z = -2;
                newObject.transform.position = placePos;
            }
        }
    }

    public bool checkIfThereIsSpaceInInventory(GameObject itemToCheck)
    {
        // for every inventory slot in inventory...
        foreach (Transform inventorySlot in transform)
        {
            if (inventorySlot.childCount > 0)
            {
                
                GameObject childOfInventorySlot = inventorySlot.GetChild(0).gameObject;
                // if existing item in slot and gameobject same and gameobject type match. 
                if ((childOfInventorySlot.GetComponent<InventoryItem>().inventorySlotObject == itemToCheck.GetComponent<ItemScript>().myPrefab))
                {
                    //Check number of items is less than max stack
                    if (childOfInventorySlot.GetComponent<InventoryItem>().inventorySlotObject.GetComponent<ItemScript>().maxStack >= childOfInventorySlot.GetComponent<InventoryItem>().itemQuantity + itemToCheck.GetComponent<ItemScript>().quantity)
                    {
                        return true;
                    }
                }
            }

            // if the inventory slot is empty (slot has no children).
            if (inventorySlot.childCount == 0)
            {
                return true;
            }

        }
        return false;
    }
    
    public bool checkIfThisIsInInventory(GameObjectAndInt gameObjectAndInt)
    {
        // for every inventory slot in inventory...
        foreach (Transform inventorySlot in transform)
        {
            if (inventorySlot.childCount > 0)
            {
                GameObject childOfInventorySlot = inventorySlot.GetChild(0).gameObject;

                // if existing item in slot and gameobject same and gameobject type match. 
                if (childOfInventorySlot.GetComponent<InventoryItem>().inventorySlotObject == gameObjectAndInt.Item)
                {
                    //Check number of items is less than max stack
                    if (childOfInventorySlot.GetComponent<InventoryItem>().itemQuantity >= gameObjectAndInt.Int)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void removeSpecificItem(GameObjectAndInt gameObjectAndInt)
    {
        if (checkIfThisIsInInventory(gameObjectAndInt))
        {
            foreach (Transform inventorySlot in transform)
            {
                if (inventorySlot.childCount > 0)
                {
                    GameObject childOfInventorySlot = inventorySlot.GetChild(0).gameObject;
                    // if existing item in slot and gameobject same and gameobject type match. 
                    if ((inventorySlot.childCount > 0) && (childOfInventorySlot.GetComponent<InventoryItem>().inventorySlotObject == gameObjectAndInt.Item))
                    {
                        //Check number of items is less than max stack
                        if (childOfInventorySlot.GetComponent<InventoryItem>().itemQuantity >= gameObjectAndInt.Int)
                        {
                            childOfInventorySlot.GetComponent<InventoryItem>().itemQuantity -= gameObjectAndInt.Int;
                            if(childOfInventorySlot.GetComponent<InventoryItem>().itemQuantity <= 0)
                            {
                                //Destroy(transform.GetChild(inventorySlotNumber).GetChild(0).gameObject);
                                Destroy(childOfInventorySlot);
                            }
                        }
                    }
                }
            }
        }
    }


    public void addSpecificItem(GameObjectAndInt gameObjectAndInt)
    {
        bool added = false;
        foreach (Transform inventorySlot in transform)
        {
            if (inventorySlot.childCount > 0)
            {
                GameObject childOfInventorySlot = inventorySlot.GetChild(0).gameObject;
                // if existing item in slot and gameobject same and gameobject type match. 
                if ((inventorySlot.childCount > 0) && (childOfInventorySlot.GetComponent<InventoryItem>().inventorySlotObject == gameObjectAndInt.Item))
                {
                    //Check number of items is less than max stack
                    if (childOfInventorySlot.GetComponent<InventoryItem>().inventorySlotObject.GetComponent<ItemScript>().maxStack >= childOfInventorySlot.GetComponent<InventoryItem>().itemQuantity + gameObjectAndInt.Int)
                    {
                        childOfInventorySlot.GetComponent<InventoryItem>().itemQuantity++;
                        added = true;
                        break;
                    }
                }
            }
        }
        if (added == false)
        {
            foreach (Transform inventorySlot in transform)
            {
                // if the inventory slot is empty (slot has no children).
                if (inventorySlot.childCount == 0)
                {
                    // create blank object in slot.
                    GameObject newObject = Instantiate(prefab, inventorySlot.transform);

                    // render image of gameobject picked up.
                    newObject.GetComponent<Image>().sprite = gameObjectAndInt.Item.GetComponent<SpriteRenderer>().sprite;

                    // get copy of prefab of item picked up to put in new inv item.
                    newObject.GetComponent<InventoryItem>().inventorySlotObject = gameObjectAndInt.Item;

                    newObject.GetComponent<InventoryItem>().itemQuantity = gameObjectAndInt.Int;
                    added = true;
                    break;
                }

            }
        }
        if (added == false)
        {
            if (gameObjectAndInt.Item != null && gameObjectAndInt.Int > 0)
            {
                GameObject newObject = Instantiate(gameObjectAndInt.Item);
                newObject.GetComponent<ItemScript>().myPrefab = gameObjectAndInt.Item;
                newObject.GetComponent<ItemScript>().quantity = gameObjectAndInt.Int;

                DeleteStack();
                if (parentObject != null)
                {
                    Vector3 placePos = parentObject.transform.position;
                    placePos.x += 0.25f * parentObject.GetComponent<MovementScript>().direction.x;
                    //placePos.y += 0.1f * parentObject.GetComponent<MovementScript>().direction.y;
                    placePos.z = -2;
                    newObject.transform.position = placePos;
                }
            }
        }
    }
}
