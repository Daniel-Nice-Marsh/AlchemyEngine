using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GrassScript : InteractableObjectBaseClass
{
    public RuleTile highlightTile;
    GameObject TileMapObject;
    GameObject mainCharacter;
    //private ItemBaseClass currentlBeingHeld;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override bool Interact(GameObject objectToInteractWith)
    {
        //mainCharacter = GameObject.Find("MainCharacter");
        //currentlBeingHeld = mainCharacter.GetComponent<PlayerItemClass>().beingHeld;
        //Debug.Log(currentlBeingHeld.type);

        //if (currentlBeingHeld.type == "Hoe")
        //{
        if (objectToInteractWith.GetComponent<ItemScript>().type == "Spade")
        {
            Debug.Log("ToDirt");
            TileMapObject = GameObject.Find("Layer1");
            Tilemap highlightMap = TileMapObject.GetComponent<Tilemap>();

            Vector3Int currentCell = highlightMap.WorldToCell(transform.position);

            highlightMap.SetTile(currentCell, highlightTile);

            
        }
        return false;
        //PlantTile plantTile = this.gameObject.AddComponent<PlantTile>();
        //Destroy(this.gameObject.GetComponent<GrassScript>());

        //}
    }

    public override bool CheckInteraction(GameObject objectToInteractWith)
    { 
        if(objectToInteractWith == null)
        {
            return false;
        }
        return objectToInteractWith.GetComponent<ItemScript>().type == "Spade";
    }
}
