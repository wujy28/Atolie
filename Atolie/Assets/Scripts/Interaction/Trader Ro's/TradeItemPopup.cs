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
    [SerializeField] private Sprite nullItemImage;

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

    private void UpdatePopupOnReset()
    {
        Item selectedItem = InventoryManager.instance.GetSelectedItem(false);
        if (selectedItem != null)
        {
            submittedItem = selectedItem;
            UpdatePopup();
        }
    }

    private void ResetPopup()
    {
        selectedItemName.text = "";
        selectedItemImage.sprite = nullItemImage;
        submittedItem = null;
    }

    public void ShowPopup()
    {
        ResetPopup();
        popup.SetActive(true);
        UpdatePopupOnReset();
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
            if (TraderRoShop.instance.GetMode() != TraderRoShop.Mode.Trading)
            {
                if (selectedItem != null)
                {
                    submittedItem = selectedItem;
                    UpdatePopup();
                }
            }
        }
    }

    public void FinalizeSubmission()
    {
        TraderRoShop.instance.TradeItem(submittedItem);
    }
}
