using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boy Listener", menuName = "Interactable Listener/City/Boy Listener")]
public class BoyListener : InteractableListener
{
    public bool wirePuzzleCompleted;

    public override void SubscribeToAllEvents()
    {
        wirePuzzleCompleted = false;
        WirePuzzleTracker.WirePuzzleCompleted += WirePuzzleTracker_WirePuzzleCompleted;
        InventoryManager.OnObtainedItemEvent += InventoryManager_OnObtainedItemEvent;
        Coloring.OnColoredInEvent += Coloring_OnColoredInEvent;
    }

    private void Coloring_OnColoredInEvent(Transform interactable)
    {
        switch (interactable.name)
        {
            case "Cyborg Dog":
                interactableData.SetCurrentInteractionIndex(1);
                break;
        }
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
        Coloring.OnColoredInEvent -= Coloring_OnColoredInEvent;
    }
}
