using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedStartingScript : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject[] listToAdd;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject gameObject in listToAdd)
        {
            GameObject newObject = Instantiate(gameObject);
            newObject.GetComponent<ItemScript>().myPrefab = gameObject;
            Inventory.GetComponent<Inventory>().PickUpFromFloor(newObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
