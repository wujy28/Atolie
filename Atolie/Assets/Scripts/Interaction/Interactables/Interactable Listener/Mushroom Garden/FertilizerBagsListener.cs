using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fertilizer Bags Listener", menuName = "Interactable Listener/Garden/Fertilizer Bags Listener")]
public class FertilizerBagsListener : InteractableListener
{
    public override void SubscribeToAllEvents()
    {
        InventoryManager.OnObtainedItemEvent += InventoryManager_OnObtainedItemEvent;
        AgreeToHelpElderTruffleRunnable.AgreeToHelpElderTruffle += AgreeToHelpElderTruffleRunnable_AgreeToHelpElderTruffle;
    }

    private void AgreeToHelpElderTruffleRunnable_AgreeToHelpElderTruffle()
    {
        interactableData.SetCurrentInteractionIndex(1);
    }

    private void InventoryManager_OnObtainedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Growth Elixir":
                interactableData.SetCurrentInteractionIndex(2);
                break;
        }
    }

    public override void UnsubscribeFromAllEvents()
    {
        InventoryManager.OnObtainedItemEvent -= InventoryManager_OnObtainedItemEvent;
        AgreeToHelpElderTruffleRunnable.AgreeToHelpElderTruffle -= AgreeToHelpElderTruffleRunnable_AgreeToHelpElderTruffle;
    }
}
