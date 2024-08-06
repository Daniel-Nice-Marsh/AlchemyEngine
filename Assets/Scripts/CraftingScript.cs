using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingScript : MonoBehaviour
{
    public GameObject InventoryGameObject;
    public Recipe testRecipe;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            craft(testRecipe);
        }
    }




    public bool canBeCrafted(Recipe recipe)
    {
        Inventory InventoryScript = InventoryGameObject.GetComponent<Inventory>();
        bool craftable = recipe.researched;
        foreach (GameObjectAndInt gameObjectAndInt in recipe.inputs)
        {
            craftable = craftable && InventoryScript.checkIfThisIsInInventory(gameObjectAndInt);
        }
        return craftable;
    }

    
    public void craft(Recipe recipe)
    {
        Inventory InventoryScript = InventoryGameObject.GetComponent<Inventory>();
        if (canBeCrafted(recipe))
        {
            foreach (GameObjectAndInt gameObjectAndInt in recipe.inputs)
            {
                InventoryScript.removeSpecificItem(gameObjectAndInt);
            }

            foreach (GameObjectAndInt gameObjectAndInt in recipe.outputs)
            {
                InventoryScript.addSpecificItem(gameObjectAndInt);
            }
        }
    }
    
}
