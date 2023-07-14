using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Arcade Manager Listener", menuName = "Interactable Listener/Arcade/Arcade Manager Listener")]
public class ArcadeManagerListener : InteractableListener
{

    private bool[] conditionsForAM2;
    private bool[] conditionsForAM7;

    public override void SubscribeToAllEvents()
    {
        InventoryManager.OnObtainedItemEvent += InventoryManager_OnObtainedItemEvent;
        Coloring.OnColoredInEvent += Coloring_OnColoredInEvent;
        TangramManager.VendingMachineTangramCompleted += TangramManager_VendingMachineTangramCompleted;
        InitiateConditions(conditionsForAM2, 2);
        InitiateConditions(conditionsForAM7, 3);
    }

    private void TangramManager_VendingMachineTangramCompleted()
    {
        MeetCondition(conditionsForAM7, 2);
    }

    private void Coloring_OnColoredInEvent(Transform interactable)
    {
        switch (interactable.name)
        {
            case "Prize Counter":
                interactableData.SetCurrentInteractionIndex(1);
                MeetCondition(conditionsForAM2, 1);
                break;
        }
    }

    private void InventoryManager_OnObtainedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Ticket":
                MeetCondition(conditionsForAM2, 0);
                break;
            case "Tamagotchi":
                interactableData.SetCurrentInteractionIndex(3);
                break;
            case "Battery":
                MeetCondition(conditionsForAM7, 0);
                break;
            case "Cup":
                MeetCondition(conditionsForAM7, 1);
                break;
            case "Dog Tag":
                interactableData.SetCurrentInteractionIndex(5);
                break;
        }
    }

    public override void UnsubscribeFromAllEvents()
    {
        InventoryManager.OnObtainedItemEvent -= InventoryManager_OnObtainedItemEvent;
        Coloring.OnColoredInEvent -= Coloring_OnColoredInEvent;
        TangramManager.VendingMachineTangramCompleted -= TangramManager_VendingMachineTangramCompleted;

    }

    private void InitiateConditions(bool[] condition, int size)
    {
        condition = new bool[size];
        for (int i = 0; i < condition.Length; i++)
        {
            condition[i] = false;
        }
    }

    private void MeetCondition(bool[] condition, int index)
    {
        condition[index] = true;
        CheckIfAllConditionsAreMet(condition);
    }

    private void CheckIfAllConditionsAreMet(bool[] condition)
    {
        foreach (bool met in condition)
        {
            if (!met)
            {
                return;
            }
        }
        AllConditionsMet(condition);
    }

    private void AllConditionsMet(bool[] condition)
    {
        if (condition == conditionsForAM2)
        {
            interactableData.SetCurrentInteractionIndex(2);
        }
        if (condition == conditionsForAM7)
        {
            interactableData.SetCurrentInteractionIndex(4);
        }
    }
}
