using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Watering Cans Listener", menuName = "Interactable Listener/Garden/Watering Cans Listener")]
public class WateringCansListener : InteractableListener
{
    public override void SubscribeToAllEvents()
    {
        InventoryManager.OnObtainedItemEvent += InventoryManager_OnObtainedItemEvent;
        SubmitItemPopup.OnSubmittedItemEvent += SubmitItemPopup_OnSubmittedItemEvent;
        AgreeToHelpElderTruffleRunnable.AgreeToHelpElderTruffle += AgreeToHelpElderTruffleRunnable_AgreeToHelpElderTruffle;
    }

    private void AgreeToHelpElderTruffleRunnable_AgreeToHelpElderTruffle()
    {
        interactableData.SetCurrentInteractionIndex(1);
    }

    private void SubmitItemPopup_OnSubmittedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Cup":
                interactableData.SetCurrentInteractionIndex(3);
                break;
        }
    }

    private void InventoryManager_OnObtainedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Cup":
                interactableData.SetCurrentInteractionIndex(2);
                break;
            case "Elixir":
                interactableData.SetCurrentInteractionIndex(4);
                break;
        }
    }

    public override void UnsubscribeFromAllEvents()
    {
        InventoryManager.OnObtainedItemEvent -= InventoryManager_OnObtainedItemEvent;
        SubmitItemPopup.OnSubmittedItemEvent -= SubmitItemPopup_OnSubmittedItemEvent;
        AgreeToHelpElderTruffleRunnable.AgreeToHelpElderTruffle -= AgreeToHelpElderTruffleRunnable_AgreeToHelpElderTruffle;
    }
}
