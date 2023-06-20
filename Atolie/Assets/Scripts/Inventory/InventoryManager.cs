using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    int selectedSlot = -1;

    InventorySlot selectedObject;

    // Event to notify QuestManager/other managers when an item has been obtained
    public static event Action<Item> OnObtainedItemEvent;

    // Event to notify SubmitItemPopup when the selected slot changes
    public static event Action<Item> OnSelectedItemChangeEvent;

    //public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        //This is for using a mouse to click & select an inventory slot
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject.GetComponent<InventorySlot>();
            }

            int index = System.Array.IndexOf(inventorySlots, selectedObject);

            if (index >= 0 && index < inventorySlots.Length)
            {
                ChangeSelectedSlot(index);
            }
        }
    }

    //changes the inventory slot selected
    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;

        // Notifies SubmitItemPopup with newly selected item
        OnSelectedItemChangeEvent?.Invoke(GetSelectedItem(false));
    }

    //@return true is inventory is not full and item can be added, false if inventory is full and item cannot be added
    public bool AddItem(Item item)
    {
        //Find any empty inventory slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                // Sends 'notification' to QuestManager with item as the argument
                OnObtainedItemEvent?.Invoke(item);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    //For using items
    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if (use == true)
            {
                Destroy(itemInSlot.gameObject); //if item is used -- destroy it so player no longer has it
            }
            return item;
        }

        return null;
    }
}
