using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result == true)
        {
            Debug.Log("Item added");
        } else
        {
            Debug.Log("Inventory is full");
        }
    }

    //The two functions below are to test for usage of items but not implemented yet so you can ignore (for now)
    public void GetSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(false);
        if (receivedItem != null)
        {
            Debug.Log("Received Item: " + receivedItem);
        } else
        {
            Debug.Log("No Item Received");
        }
    }

    public void UseSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(true);
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
