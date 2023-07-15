using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mushroom Beds Listener", menuName = "Interactable Listener/Garden/Mushroom Beds Listener")]
public class MushroomBedsListener : InteractableListener
{
    public bool watered;

    public override void SubscribeToAllEvents()
    {
        watered = false;
        InventoryManager.OnObtainedItemEvent += InventoryManager_OnObtainedItemEvent;
        SubmitItemPopup.OnSubmittedItemEvent += SubmitItemPopup_OnSubmittedItemEvent;
        WaterMushroomBedsRunnable.OnMushroomBedsWatered += WaterMushroomBedsRunnable_OnMushroomBedsWatered;
    }

    private void WaterMushroomBedsRunnable_OnMushroomBedsWatered()
    {
        watered = true;
        interactableData.SetCurrentInteractionIndex(3);
    }

    private void SubmitItemPopup_OnSubmittedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Elixir":
                interactableData.SetCurrentInteractionIndex(2);
                break;
        }
    }

    private void InventoryManager_OnObtainedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Elixir":
                interactableData.SetCurrentInteractionIndex(1);
                break;
        }
    }

    public override void UnsubscribeFromAllEvents()
    {
        watered = false;
        InventoryManager.OnObtainedItemEvent -= InventoryManager_OnObtainedItemEvent;
        SubmitItemPopup.OnSubmittedItemEvent -= SubmitItemPopup_OnSubmittedItemEvent;
        WaterMushroomBedsRunnable.OnMushroomBedsWatered += WaterMushroomBedsRunnable_OnMushroomBedsWatered;
    }
}
