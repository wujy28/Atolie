using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mushroom House Listener", menuName = "Interactable Listener/Mushroom House Listener")]
public class MushroomHouseListener : InteractableListener
{
    public override void SubscribeToAllEvents()
    {
        InventoryManager.OnObtainedItemEvent += InventoryManager_OnObtainedItemEvent;

    }

    private void InventoryManager_OnObtainedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Goop":
                interactableData.SetCurrentInteractionIndex(1);
                break;
        }
    }

    public override void UnsubscribeFromAllEvents()
    {
        
    }
}
