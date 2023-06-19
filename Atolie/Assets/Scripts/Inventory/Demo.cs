using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    //public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    //Test spawning item
    public void PickupItem(int id)
    {
        bool result = InventoryManager.instance.AddItem(itemsToPickup[id]);
        if (result == true)
        {
            Debug.Log("Item added");
        } else
        {
            Debug.Log("Inventory is full");
        }
    }

    //Test getting an item based on selected slot
    public void GetSelectedItem()
    {
        Item receivedItem = InventoryManager.instance.GetSelectedItem(false);
        if (receivedItem != null)
        {
            Debug.Log("Received Item: " + receivedItem);
        } else
        {
            Debug.Log("No Item Received");
        }
    }

    //Test using an item based on selected slot (item should be destroyed if it exists and is used)
    public void UseSelectedItem()
    {
        Item receivedItem = InventoryManager.instance.GetSelectedItem(true);
        if (receivedItem != null)
        {
            Debug.Log("Used Item: " + receivedItem);
        }
        else
        {
            Debug.Log("No Item Used");
        }
    }
}
