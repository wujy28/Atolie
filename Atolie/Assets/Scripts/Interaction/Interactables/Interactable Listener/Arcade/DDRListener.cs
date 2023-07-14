using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DDR Listener", menuName = "Interactable Listener/Arcade/DDR Listener")]
public class DDRListener : InteractableListener
{
    public override void SubscribeToAllEvents()
    {
        InventoryManager.OnObtainedItemEvent += InventoryManager_OnObtainedItemEvent;
        SubmitItemPopup.OnSubmittedItemEvent += SubmitItemPopup_OnSubmittedItemEvent;
    }

    private void SubmitItemPopup_OnSubmittedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Token":
                interactableData.SetCurrentInteractionIndex(2);
                break;
        }
    }

    private void InventoryManager_OnObtainedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Ticket":
                interactableData.SetCurrentInteractionIndex(3);
                break;
            case "Token":
                interactableData.SetCurrentInteractionIndex(1);
                break;
        }
    }

    public override void UnsubscribeFromAllEvents()
    {
        InventoryManager.OnObtainedItemEvent -= InventoryManager_OnObtainedItemEvent;
        SubmitItemPopup.OnSubmittedItemEvent -= SubmitItemPopup_OnSubmittedItemEvent;
    }
}
