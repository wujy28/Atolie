using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeItemPopup : MonoBehaviour
{
    public static TradeItemPopup Instance;

    private Item submittedItem;

    [SerializeField] private GameObject popup;
    [SerializeField] private Text selectedItemName;
    [SerializeField] private Image selectedItemImage;
    [SerializeField] private Button submitButton;

    // Event to notify QuestManager/other managers when an item is submitted
    public static event Action<Item> OnSubmittedItemEvent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        InventoryManager.OnSelectedItemChangeEvent += InventoryManager_OnSelectedItemChangeEvent;
    }

    private void OnDestroy()
    {
        InventoryManager.OnSelectedItemChangeEvent -= InventoryManager_OnSelectedItemChangeEvent;
    }

    private void UpdatePopup()
    {
        selectedItemName.text = submittedItem.name;
        selectedItemImage.sprite = submittedItem.image;
    }

    public void ShowPopup()
    {
        popup.SetActive(true);
    }

    public void HidePopup()
    {
        popup.SetActive(false);
    }

    public void CloseWindow()
    {
        HidePopup();
    }

    public void DisableSubmission()
    {
        submitButton.interactable = false;
    }

    public void EnableSubmission()
    {
        submitButton.interactable = true;
    }

    private void InventoryManager_OnSelectedItemChangeEvent(Item selectedItem)
    {
        if (popup.activeSelf)
        {
            if (selectedItem != null)
            {
                submittedItem = selectedItem;
                UpdatePopup();
            }
        }
    }

    public void FinalizeSubmission()
    {
        TraderRoShop.instance.TradeItem(submittedItem);
    }
}
