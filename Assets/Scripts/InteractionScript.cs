using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System;

public class InteractionScript : MonoBehaviour
{
    private GameObject interactionObject;
    private float dist;
    private float lowestDist;
    private float buttonPressedTime;
    private bool holding;
    private InteractableObjectBaseClass interactableObject;
    public GameObject markerPrefab;
    public float reachDistance = 0.5f;
    public int xSelect, ySelect;
    GameObject actualSelectionMarker;
    GameObject mainCharacter;
    Vector3 cellPos;
    Vector3 drawCellPoss;
    GameObject tileObject;
    InteractableObjectBaseClass? interactScript;
    float TimeT;
    float timer;
    GameObject interactObject;
    public Canvas canvas;
    [Range(0, 1)]
    private float myFillAmount;
    Image image;
    GameObject progressCircle;
    GameObject progress;
    RectTransform rectTransform;
    bool inRange;
    SpriteRenderer selectionSprite;
    public GameObject InventoryObject;
    GameObject inventoryInteractItem;


    void Start()
    {
        holding = false;

        actualSelectionMarker = new GameObject("SelectionMarker");
        actualSelectionMarker.transform.position = Vector3.zero;
        actualSelectionMarker.transform.rotation = Quaternion.identity;

        mainCharacter = GameObject.Find("MainCharacter");
        //canvas.GetComponent<Canvas>().enabled = false;
        progressCircle = canvas.transform.GetChild(0).gameObject;
        progressCircle.SetActive(false);

        rectTransform = progressCircle.GetComponent<RectTransform>();

        progress = progressCircle.transform.GetChild(1).gameObject;
        image = progress.GetComponent<Image>();

        
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        GameObject TileMapObject = GameObject.Find("Layer1");
        Tilemap highlightMap = TileMapObject.GetComponent<Tilemap>();



        Vector3Int currentCell = highlightMap.WorldToCell(worldMousePos);
        Vector3 cellPos = highlightMap.CellToWorld(currentCell);


        drawCellPoss.z = -0.9f;
        drawCellPoss.x = cellPos.x + 0.08f;
        drawCellPoss.y = cellPos.y + 0.08f;

        actualSelectionMarker.transform.position = drawCellPoss;

        int numberToLeft = (int) (Mathf.Ceil(xSelect / 2));
        int numberToRight = (int) (xSelect - Mathf.Ceil(xSelect / 2));

        int numberToBottom = (int) (Mathf.Ceil(ySelect / 2));
        int numberToTop = (int) (ySelect - Mathf.Ceil(ySelect / 2));


        for (int i = 0; i < actualSelectionMarker.transform.childCount; i++)
        {
            Destroy(actualSelectionMarker.transform.GetChild(i).gameObject);

        }
        GameObject[,] array2D = new GameObject[ySelect, xSelect];

        for (int y = 0; y < ySelect; y++)
        {
            for (int x = 0; x < xSelect; x++)
            {
                array2D[y,x] = GameObject.Instantiate(markerPrefab, new Vector3(drawCellPoss.x - 0.16f * numberToLeft + 0.16f * x, drawCellPoss.y - 0.16f * numberToBottom + 0.16f * y, drawCellPoss.z), Quaternion.identity, actualSelectionMarker.transform);
            }
        }




        if ((find2DDist(mainCharacter, cellPos) <= reachDistance) && (inRange == false)){
            //selectionSprite.color = Color.white;

            markerPrefab.GetComponent<SpriteRenderer>().color = Color.white;

            inRange = true;
        }
        else if(inRange && (find2DDist(mainCharacter, cellPos) > reachDistance))
        {
            //selectionSprite.color = Color.red;
            markerPrefab.GetComponent<SpriteRenderer>().color = Color.red;
            inRange = false;
        }



        if (Input.GetMouseButton(0))
        {
            //Debug.Log(find2DDist(mainCharacter,cellPos));
            tileObject = highlightMap.GetInstantiatedObject(currentCell);
            if (tileObject != null)
            {
                if (tileObject != interactObject || inventoryInteractItem != InventoryObject.GetComponent<Inventory>().SelectInventorySlot().Item)
                {
                    holding = false;
                }
                if (holding == false)
                {
                    interactScript = tileObject.GetComponent<InteractableObjectBaseClass>();
                    if (interactScript != null && find2DDist(mainCharacter, cellPos) <= reachDistance && interactScript.CheckInteraction(InventoryObject.GetComponent<Inventory>().SelectInventorySlot().Item))
                    {
                        //canvas.GetComponent<Canvas>().enabled = true;
                        progressCircle.SetActive(true);
                        interactObject = highlightMap.GetInstantiatedObject(currentCell);
                        inventoryInteractItem = InventoryObject.GetComponent<Inventory>().SelectInventorySlot().Item;
                        TimeT = timer;
                        holding = true;

                    }
                    else
                    {
                        //canvas.GetComponent<Canvas>().enabled = false;
                        progressCircle.SetActive(false);
                    }
                }
                else if (TimeT + interactScript.interactTime <= timer)
                {
                    //Finished holding
                    if (interactScript.Interact(InventoryObject.GetComponent<Inventory>().SelectInventorySlot().Item))
                    {
                        InventoryObject.GetComponent<Inventory>().RemoveFromInventory();
                    }
                    holding = false;
                    progressCircle.SetActive(false);
                    //canvas.GetComponent<Canvas>().enabled = false;
                }
                else
                {
                    myFillAmount = (timer - TimeT) / interactScript.interactTime;
                    image.fillAmount = myFillAmount;
                    if (find2DDist(mainCharacter, cellPos) > reachDistance)
                    {
                        holding = false;
                        //canvas.GetComponent<Canvas>().enabled = false;
                        progressCircle.SetActive(false);
                    }
                }
            }
            else if (inRange)
            {
                //selectionSprite.color = Color.red;
                markerPrefab.GetComponent<SpriteRenderer>().color = Color.red;
                inRange = false;
            }

        }
        else
        {
            holding = false;
            progressCircle.SetActive(false);
            //canvas.GetComponent<Canvas>().enabled = false;
        }


    }

    private double find2DDist(GameObject obj1, Vector3 obj2)
    {
        double xDist = obj1.transform.position.x - obj2.x;
        double yDist = obj1.transform.position.y - obj2.y;

        return Math.Sqrt(xDist* xDist + yDist * yDist);
    }
}
