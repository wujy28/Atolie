using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pond Listener", menuName = "Interactable Listener/Garden/Pond Listener")]
public class PondListener : InteractableListener
{
    public bool pondTrimmed;

    private bool[] conditionsForP2;
    private bool[] conditionsForP3;

    public override void SubscribeToAllEvents()
    {
        pondTrimmed = false;
        InventoryManager.OnObtainedItemEvent += InventoryManager_OnObtainedItemEvent;
        SubmitItemPopup.OnSubmittedItemEvent += SubmitItemPopup_OnSubmittedItemEvent;
        AgreeToHelpElderTruffleRunnable.AgreeToHelpElderTruffle += AgreeToHelpElderTruffleRunnable_AgreeToHelpElderTruffle;
        TrimPondRunnable.OnPondTrimmed += TrimPondRunnable_OnPondTrimmed;
        InitiateConditionsForP2(2);
        InitiateConditionsForP3(2);
    }

    private void AgreeToHelpElderTruffleRunnable_AgreeToHelpElderTruffle()
    {
        interactableData.SetCurrentInteractionIndex(1);
        MeetConditionForP2(1);
    }

    private void SubmitItemPopup_OnSubmittedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Cyborg Fish":
                MeetConditionForP3(1);
                break;
            case "Shears":
                interactableData.SetCurrentInteractionIndex(5);
                break;
            case "Battery":
                interactableData.SetCurrentInteractionIndex(7);
                MeetConditionForP3(0);
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
            case "Cyborg Fish":
                MeetConditionForP2(0);
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

    private void InitiateConditionsForP2(int size)
    {
        conditionsForP2 = new bool[size];
        for (int i = 0; i < conditionsForP2.Length; i++)
        {
            conditionsForP2[i] = false;
        }
    }

    private void InitiateConditionsForP3(int size)
    {
        conditionsForP3 = new bool[size];
        for (int i = 0; i < conditionsForP3.Length; i++)
        {
            conditionsForP3[i] = false;
        }
    }

    private void MeetConditionForP2(int index)
    {
        conditionsForP2[index] = true;
        CheckIfAllConditionsAreMet(conditionsForP2);
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
        if (condition == conditionsForP2)
        {
            interactableData.SetCurrentInteractionIndex(2);
        }
        if (condition == conditionsForP3)
        {
            interactableData.SetCurrentInteractionIndex(3);
        }
    }
}
