using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElderTruffle Listener", menuName = "Interactable Listener/Garden/ElderTruffle Listener")]
public class ElderTruffleListener : InteractableListener
{

    private bool[] conditionsForET2;

    public override void SubscribeToAllEvents()
    {
        InventoryManager.OnObtainedItemEvent += InventoryManager_OnObtainedItemEvent;
        AgreeToHelpElderTruffleRunnable.AgreeToHelpElderTruffle += AgreeToHelpElderTruffleRunnable_AgreeToHelpElderTruffle;
        TrimPondRunnable.OnPondTrimmed += TrimPondRunnable_OnPondTrimmed;
        WaterMushroomBedsRunnable.OnMushroomBedsWatered += WaterMushroomBedsRunnable_OnMushroomBedsWatered;
        InitiateConditions(conditionsForET2, 2);
    }

    private void WaterMushroomBedsRunnable_OnMushroomBedsWatered()
    {
        MeetCondition(conditionsForET2, 1);
    }

    private void TrimPondRunnable_OnPondTrimmed()
    {
        MeetCondition(conditionsForET2, 0);
    }

    private void AgreeToHelpElderTruffleRunnable_AgreeToHelpElderTruffle()
    {
        interactableData.SetCurrentInteractionIndex(1);
    }

    private void InventoryManager_OnObtainedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Truffle":
                interactableData.SetCurrentInteractionIndex(3);
                break;
        }
    }

    public override void UnsubscribeFromAllEvents()
    {
        InventoryManager.OnObtainedItemEvent -= InventoryManager_OnObtainedItemEvent;
        AgreeToHelpElderTruffleRunnable.AgreeToHelpElderTruffle -= AgreeToHelpElderTruffleRunnable_AgreeToHelpElderTruffle;
        TrimPondRunnable.OnPondTrimmed -= TrimPondRunnable_OnPondTrimmed;
        WaterMushroomBedsRunnable.OnMushroomBedsWatered -= WaterMushroomBedsRunnable_OnMushroomBedsWatered;
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
        if (condition == conditionsForET2)
        {
            interactableData.SetCurrentInteractionIndex(2);
        }
    }
}
