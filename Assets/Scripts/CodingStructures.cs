using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CodingStructures : MonoBehaviour
{
}

[Serializable]
public struct GameObjectAndFloat
{
    public GameObject Item;
    public float Float;
}

[Serializable]
public struct GameObjectAndInt
{
    public GameObject Item;
    public int Int;
}

[Serializable]
public struct Recipe
{
    public GameObjectAndInt[] inputs;
    public GameObjectAndInt[] outputs;
    public bool researched;
}
