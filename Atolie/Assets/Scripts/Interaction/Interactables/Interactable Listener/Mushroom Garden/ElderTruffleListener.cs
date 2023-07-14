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
        InitiateConditions(2);
    }

    private void WaterMushroomBedsRunnable_OnMushroomBedsWatered()
    {
        MeetCondition(1);
    }

    private void TrimPondRunnable_OnPondTrimmed()
    {
        MeetCondition(0);
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

    private void InitiateConditions(int size)
    {
        conditionsForET2 = new bool[size];
        for (int i = 0; i < conditionsForET2.Length; i++)
        {
            conditionsForET2[i] = false;
        }
    }

    private void MeetCondition(int index)
    {
        conditionsForET2[index] = true;
        CheckIfAllConditionsAreMet(conditionsForET2);
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
