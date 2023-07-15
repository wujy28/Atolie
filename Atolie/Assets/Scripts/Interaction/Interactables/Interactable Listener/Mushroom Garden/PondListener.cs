using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pond Listener", menuName = "Interactable Listener/Garden/Pond Listener")]
public class PondListener : InteractableListener
{
    public bool pondTrimmed;

    public override void SubscribeToAllEvents()
    {
        pondTrimmed = false;
        InventoryManager.OnObtainedItemEvent += InventoryManager_OnObtainedItemEvent;
        SubmitItemPopup.OnSubmittedItemEvent += SubmitItemPopup_OnSubmittedItemEvent;
        AgreeToHelpElderTruffleRunnable.AgreeToHelpElderTruffle += AgreeToHelpElderTruffleRunnable_AgreeToHelpElderTruffle;
        TrimPondRunnable.OnPondTrimmed += TrimPondRunnable_OnPondTrimmed;
    }

    private void AgreeToHelpElderTruffleRunnable_AgreeToHelpElderTruffle()
    {
        interactableData.SetCurrentInteractionIndex(1);
    }

    private void SubmitItemPopup_OnSubmittedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Fish":
                interactableData.SetCurrentInteractionIndex(3);
                break;
            case "Shears":
                interactableData.SetCurrentInteractionIndex(5);
                break;
        }
    }

    private void TrimPondRunnable_OnPondTrimmed()
    {
        pondTrimmed = true;
        interactableData.SetCurrentInteractionIndex(6);
    }

    private void InventoryManager_OnObtainedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Fish":
                interactableData.SetCurrentInteractionIndex(2);
                break;
            case "Shears":
                interactableData.SetCurrentInteractionIndex(4);
                break;
        }
    }

    public override void UnsubscribeFromAllEvents()
    {
        pondTrimmed = false;
        InventoryManager.OnObtainedItemEvent -= InventoryManager_OnObtainedItemEvent;
        SubmitItemPopup.OnSubmittedItemEvent -= SubmitItemPopup_OnSubmittedItemEvent;
        AgreeToHelpElderTruffleRunnable.AgreeToHelpElderTruffle -= AgreeToHelpElderTruffleRunnable_AgreeToHelpElderTruffle;
        TrimPondRunnable.OnPondTrimmed -= TrimPondRunnable_OnPondTrimmed;
    }
}
