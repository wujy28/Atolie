using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderRoTest : MonoBehaviour
{
    [SerializeField] private List<Item> itemsToAdd;

    void Start()
    {
        foreach (Item item in itemsToAdd)
        {
            InventoryManager.instance.AddItem(item);
        }
    }
}
