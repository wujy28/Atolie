using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pond Listener", menuName = "Interactable Listener/Garden/Pond Listener")]
public class PondListener : InteractableListener
{
    public bool pondTrimmed;

    private bool[] conditionsForP3;

    public override void SubscribeToAllEvents()
    {
        pondTrimmed = false;
        InventoryManager.OnObtainedItemEvent += InventoryManager_OnObtainedItemEvent;
        SubmitItemPopup.OnSubmittedItemEvent += SubmitItemPopup_OnSubmittedItemEvent;
        AgreeToHelpElderTruffleRunnable.AgreeToHelpElderTruffle += AgreeToHelpElderTruffleRunnable_AgreeToHelpElderTruffle;
        TrimPondRunnable.OnPondTrimmed += TrimPondRunnable_OnPondTrimmed;
        InitiateConditionsForP3(2);
    }

    private void AgreeToHelpElderTruffleRunnable_AgreeToHelpElderTruffle()
    {
        interactableData.SetCurrentInteractionIndex(1);
    }

    private void SubmitItemPopup_OnSubmittedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Fish":
                MeetConditionForP3(1);
                break;
            case "Shears":
                interactableData.SetCurrentInteractionIndex(5);
                break;
            case "Battery":
                MeetConditionForP3(0);
                interactableData.SetCurrentInteractionIndex(7);
                break;
        }
    }

    private void TrimPondRunnable_OnPondTrimmed()
    {
        pondTrimmed = true;
        interactableData.SetCurrentInteractionIndex(6);
    }

    private void InventoryManager_OnObtainedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Fish":
                interactableData.SetCurrentInteractionIndex(2);
                break;
            case "Shears":
                interactableData.SetCurrentInteractionIndex(4);
                break;
        }
    }

    public override void UnsubscribeFromAllEvents()
    {
        pondTrimmed = false;
        InventoryManager.OnObtainedItemEvent -= InventoryManager_OnObtainedItemEvent;
        SubmitItemPopup.OnSubmittedItemEvent -= SubmitItemPopup_OnSubmittedItemEvent;
        AgreeToHelpElderTruffleRunnable.AgreeToHelpElderTruffle -= AgreeToHelpElderTruffleRunnable_AgreeToHelpElderTruffle;
        TrimPondRunnable.OnPondTrimmed -= TrimPondRunnable_OnPondTrimmed;
    }

    private void InitiateConditionsForP3(int size)
    {
        conditionsForP3 = new bool[size];
        for (int i = 0; i < conditionsForP3.Length; i++)
        {
            conditionsForP3[i] = false;
        }
    }

    private void MeetConditionForP3(int index)
    {
        conditionsForP3[index] = true;
        CheckIfAllConditionsAreMet(conditionsForP3);
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
        if (condition == conditionsForP3)
        {
            interactableData.SetCurrentInteractionIndex(3);
        }
    }
}
