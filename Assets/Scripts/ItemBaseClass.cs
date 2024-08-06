using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBaseClass : MonoBehaviour
{
    public int id;
    public string name;
    public string tooltip;
    public bool tool;
    public Sprite sprite;
    public string type;
    public Texture2D texture2D;
    public Dictionary<string, int> stats = new Dictionary<string, int>();



    public ItemBaseClass(int id, string name, string tooltip, bool tool, string type, Dictionary<string, int> stats, Texture2D texture2D)
    {
        this.id = id;
        this.name = name;
        this.tooltip = tooltip;
        this.stats = stats;
        this.type = type;
        this.tool = tool;
        this.texture2D = texture2D;
    }
}
