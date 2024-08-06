using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class PlantTile : InteractableObjectBaseClass
{
    public bool isPlanted;
    bool isFullyGrown;
    int numberOfStages;
    public int stage;
    float growChance;
    string plantPath;
    public float water;
    int dimension;
    //dictionary<GameObject item, list<float> chances> drops

    public GameObjectAndFloat[] dropsList;

    private System.Random random;
    SpriteRenderer plantSprite;
    Texture2D newTexture;
    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random();
        //interactable = true;
        plantSprite = gameObject.AddComponent<SpriteRenderer>();
        isPlanted = false;

        Vector3 currentPosition = transform.position; 
        currentPosition.z = -1; 
        transform.position = currentPosition;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tick();
    }

    public override bool Interact(GameObject objectToInteractWith)
    {
        if (objectToInteractWith != null)
        {
            if (!isPlanted && objectToInteractWith.GetComponent<ItemScript>().type == "Seed")
            {
                Plant(objectToInteractWith);
                water = 10f;
                return true;
            }
            else if (isFullyGrown && (objectToInteractWith.GetComponent<ItemScript>().type == "Scythe"))
            {
                Harvest();
                return false;
            }
        }
        else
        {
            Debug.Log("There's already a plant here");
        }
        return false;
    }

    public override bool CheckInteraction(GameObject objectToInteractWith)
    {
        //Debug.Log("CheckInteraction");
        //Debug.Log(objectToInteractWith);
        if(objectToInteractWith == null)
        {
            return false;
        }
        if((isPlanted == false) && (objectToInteractWith.GetComponent<ItemScript>().type == "Seed"))
        {
            return true;
        }else if(isFullyGrown && (objectToInteractWith.GetComponent<ItemScript>().type == "Scythe"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Plant(GameObject objectToInteractWith)
    {
        isPlanted = true;
        stage = 1;
        interactTime = 0.5f;

        dropsList = objectToInteractWith.GetComponent<SeedScript>().dropsList;
        numberOfStages = objectToInteractWith.GetComponent<SeedScript>().numberOfStages;
        growChance = objectToInteractWith.GetComponent<SeedScript>().growChance;
        plantPath = objectToInteractWith.GetComponent<SeedScript>().plantPath;
        dimension = objectToInteractWith.GetComponent<SeedScript>().dimension;

        Redraw();
    }

    
    private void Harvest()
    {
        //Debug.Log("harvest");
        if (isFullyGrown)
        {
            isPlanted = false;
            stage = 0;
            numberOfStages = 0;
            growChance = 0f;
            plantPath = "";
            isFullyGrown = false;

            foreach(GameObjectAndFloat drop in dropsList)
            {
                int intDrop = random.Next((int)Mathf.Floor(drop.Float), (int)Mathf.Ceil(drop.Float + 1));
                if (intDrop > 0)
                {
                    GameObject loot = Instantiate(drop.Item, transform.position, transform.rotation);
                    loot.GetComponent<ItemScript>().myPrefab = drop.Item;
                    loot.GetComponent<ItemScript>().quantity = intDrop;
                }
            }

            
            Redraw();
            
        }
    }
    

    private void Grow()
    {
        //Debug.Log("grow");
        stage = stage + 1;

        if(stage >= numberOfStages)
        {
            interactTime = 2f;
            isFullyGrown = true;
        }
        Redraw();
    }

    //private void water() { };

    private void tick()
    {
        if (isPlanted)
        {
            if (isFullyGrown)
            {
                water = water - 0.01f;
            }
            else
            {
                water = water - 0.05f;
                if((float) random.NextDouble() <= growChance)
                {
                    Grow();
                }
            }
        }
    }

    private void Redraw()
    {
        if (plantPath != "")
        {
            newTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(String.Format("Assets/Art/Sprites/Plants/{0}.png", plantPath), typeof(Texture2D));
            sprite = Sprite.Create(newTexture, new Rect((stage - 1 )* dimension, 0, dimension, dimension), new Vector2(0.5f, 0.5f));
            plantSprite.sprite = sprite;
        }
        else
        {
            plantSprite.sprite = null;
        }
    }
}

