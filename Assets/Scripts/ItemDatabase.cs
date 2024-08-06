using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemDatabase : MonoBehaviour
{
    public List<ItemBaseClass> items = new List<ItemBaseClass>();
    private int currentID; //Do not edit manually 

    private int returnNextID()
    {
        return currentID++;
    }

    void Start()
    {

        ItemBaseClass test = new ItemBaseClass(returnNextID(), "Something", "Something Description", false, "unknown",
            new Dictionary<string, int>
            {
                { "Swing Speed", 1},
                { "Something else", 1 }
            }, ((Texture2D) AssetDatabase.LoadAssetAtPath("Assets/Art/Sprites/Plants/BasicSeeds.png", typeof(Texture2D))));
        items.Add(test);


        //Debug.Log(items[0].texture2D);
        /*
        Texture2D temp = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets / Art / Sprites / Plants /BasicSeeds.png", typeof(Texture2D));
        items.Add(new ItemBaseClass(returnNextID(), "Something", "Something Description", false, "unknown",
            new Dictionary<string, int>
            {
                { "Swing Speed", 1},
                { "Something else", 1 }
            }, temp));

        temp = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets / Art / Sprites / Plants /BasicSeeds1.png", typeof(Texture2D));
        items.Add(new ItemBaseClass(returnNextID(), "Something Else", "Something Description", false, "unknown",
            new Dictionary<string, int>
            {
                { "Swing Speed", 1},
                { "Something else", 1 }
            }, temp));
            */
    }

    public ItemBaseClass GetItem(int id)
    {
        return items.Find(x => x.id == id);
    }


}