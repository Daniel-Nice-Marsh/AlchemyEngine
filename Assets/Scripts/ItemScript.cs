using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public GameObject myPrefab;
    public int id;
    public int quantity = 1;
    public string type;
    public int maxStack = 1;
    private ItemBaseClass thisItem;
    Sprite sprite;
    SpriteRenderer spriteRenderer;
    GameObject itemDataBaseObject;
    ItemDatabase itemDatabaseScript;
    public GameObjectAndFloat[] dropsList;

}
