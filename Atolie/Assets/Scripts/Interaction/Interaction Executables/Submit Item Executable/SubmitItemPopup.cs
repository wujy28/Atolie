using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitItemPopup : MonoBehaviour
{
    public static SubmitItemPopup Instance;

    private Item requiredItem;
    private Item submittedItem;

    [SerializeField] private GameObject popupCanvas;
    [SerializeField] private Text requiredItemName;
    [SerializeField] private Image requiredItemImage;
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

    public void updatePopup(Item itemNeeded)
    {
        requiredItem = itemNeeded;
        submittedItem = null;
        requiredItemName.text = requiredItem.name;
        ChangeImageAlpha(requiredItemImage, 128);
        requiredItemImage.sprite = requiredItem.image;
    }

    private void ChangeImageAlpha(Image image, int a)
    {
        Color32 color = image.color;
        color.a = (byte)a;
        image.color = color;
    }

    public void ShowPopup()
    {
        popupCanvas.SetActive(true);
    }

    public void HidePopup()
    {
        popupCanvas.SetActive(false);
    }

    public void CloseWindow()
    {
        HidePopup();
        InteractionManager.Instance.forceExitInteraction();
    }

    private void InventoryManager_OnSelectedItemChangeEvent(Item selectedItem)
    {
        submittedItem = selectedItem;
        if (submittedItem == requiredItem)
        {
            submitButton.interactable = true;
            ChangeImageAlpha(requiredItemImage, 255);
        }
        else
        {
            submitButton.interactable = false;
            ChangeImageAlpha(requiredItemImage, 128);
        }
    }

    public void FinalizeSubmission()
    {
        InventoryManager.instance.GetSelectedItem(true);
        HidePopup();
        InteractionExecutable.currentExecutableCompleted();
        OnSubmittedItemEvent?.Invoke(requiredItem);
    }
}
