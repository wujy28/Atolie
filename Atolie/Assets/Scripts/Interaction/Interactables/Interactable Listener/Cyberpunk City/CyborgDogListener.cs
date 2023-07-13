using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cyborg Dog Listener", menuName = "Interactable Listener/City/Cyborg Dog Listener")]
public class CyborgDogListener : InteractableListener
{
    public bool wirePuzzleCompleted;

    public override void SubscribeToAllEvents()
    {
        wirePuzzleCompleted = false;
        WirePuzzleTracker.WirePuzzleCompleted += WirePuzzleTracker_WirePuzzleCompleted;
        InventoryManager.OnObtainedItemEvent += InventoryManager_OnObtainedItemEvent;
        // CO: Agree to help boy
    }

    private void InventoryManager_OnObtainedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Dog Leg":
                interactableData.SetCurrentInteractionIndex(2);
                break;
        }
    }

    private void WirePuzzleTracker_WirePuzzleCompleted()
    {
        wirePuzzleCompleted = true;
    }

    public override void UnsubscribeFromAllEvents()
    {
        wirePuzzleCompleted = false;
        WirePuzzleTracker.WirePuzzleCompleted -= WirePuzzleTracker_WirePuzzleCompleted;
        InventoryManager.OnObtainedItemEvent -= InventoryManager_OnObtainedItemEvent;
        // CO: Agree to help boy
    }
}
