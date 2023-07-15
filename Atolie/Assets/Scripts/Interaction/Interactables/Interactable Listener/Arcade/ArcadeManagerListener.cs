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
        InitiateConditionsForAM2();
        InitiateConditionsForAM7();
    }

    private void TangramManager_VendingMachineTangramCompleted()
    {
        MeetConditionForAM7(2);
    }

    private void Coloring_OnColoredInEvent(Transform interactable)
    {
        switch (interactable.name)
        {
            case "Prize Counter":
                interactableData.SetCurrentInteractionIndex(1);
                MeetConditionForAM2(1);
                break;
        }
    }

    private void InventoryManager_OnObtainedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Ticket":
                MeetConditionForAM2(0);
                break;
            case "Tamagotchi":
                interactableData.SetCurrentInteractionIndex(3);
                break;
            case "Battery":
                MeetConditionForAM7(0);
                break;
            case "Cup":
                MeetConditionForAM7(1);
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

    /*
    private void InitiateConditions(bool[] condition, int size)
    {
        condition = new bool[size];
        for (int i = 0; i < size; i++)
        {
            condition[i] = false;
        }
    }
    */

    private void InitiateConditionsForAM2()
    {
        conditionsForAM2 = new bool[2];
        for (int i = 0; i < conditionsForAM2.Length; i++)
        {
            conditionsForAM2[i] = false;
        }
    }

    private void InitiateConditionsForAM7()
    {
        conditionsForAM7 = new bool[3];
        for (int i = 0; i < conditionsForAM7.Length; i++)
        {
            conditionsForAM7[i] = false;
        }
    }

    private void MeetConditionForAM2(int index)
    {
        conditionsForAM2[index] = true;
        CheckIfAllConditionsAreMet(conditionsForAM2);
    }

    private void MeetConditionForAM7(int index)
    {
        conditionsForAM7[index] = true;
        CheckIfAllConditionsAreMet(conditionsForAM7);
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
