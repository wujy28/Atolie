using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mushroom House Listener", menuName = "Interactable Listener/Garden/Mushroom House Listener")]
public class MushroomHouseListener : InteractableListener
{
    public bool mushroomHouseUnlocked;

    public override void SubscribeToAllEvents()
    {
        mushroomHouseUnlocked = false;
        InventoryManager.OnObtainedItemEvent += InventoryManager_OnObtainedItemEvent;
        UnlockMushroomHouseRunnable.OnMushroomHouseUnlocked += UnlockMushroomHouseRunnable_OnMushroomHouseUnlocked;
    }

    private void UnlockMushroomHouseRunnable_OnMushroomHouseUnlocked()
    {
        mushroomHouseUnlocked = true;
    }

    private void InventoryManager_OnObtainedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Mushroom Key":
                interactableData.SetCurrentInteractionIndex(1);
                break;
        }
    }

    public override void UnsubscribeFromAllEvents()
    {
        mushroomHouseUnlocked = false;
        InventoryManager.OnObtainedItemEvent -= InventoryManager_OnObtainedItemEvent;
        UnlockMushroomHouseRunnable.OnMushroomHouseUnlocked -= UnlockMushroomHouseRunnable_OnMushroomHouseUnlocked;
    }
}
