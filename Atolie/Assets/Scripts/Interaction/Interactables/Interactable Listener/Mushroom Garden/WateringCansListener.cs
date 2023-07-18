using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Watering Cans Listener", menuName = "Interactable Listener/Garden/Watering Cans Listener")]
public class WateringCansListener : InteractableListener
{
    private bool[] conditionsForWC2;

    public override void SubscribeToAllEvents()
    {
        InventoryManager.OnObtainedItemEvent += InventoryManager_OnObtainedItemEvent;
        SubmitItemPopup.OnSubmittedItemEvent += SubmitItemPopup_OnSubmittedItemEvent;
        AgreeToHelpElderTruffleRunnable.AgreeToHelpElderTruffle += AgreeToHelpElderTruffleRunnable_AgreeToHelpElderTruffle;
        InitiateConditionsForWC2();
    }

    private void AgreeToHelpElderTruffleRunnable_AgreeToHelpElderTruffle()
    {
        interactableData.SetCurrentInteractionIndex(1);
        MeetConditionForWC2(1);
    }

    private void SubmitItemPopup_OnSubmittedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Cup":
                interactableData.SetCurrentInteractionIndex(3);
                break;
        }
    }

    private void InventoryManager_OnObtainedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Cup":
                MeetConditionForWC2(0);
                break;
            case "Elixir":
                interactableData.SetCurrentInteractionIndex(4);
                break;
        }
    }

    public override void UnsubscribeFromAllEvents()
    {
        InventoryManager.OnObtainedItemEvent -= InventoryManager_OnObtainedItemEvent;
        SubmitItemPopup.OnSubmittedItemEvent -= SubmitItemPopup_OnSubmittedItemEvent;
        AgreeToHelpElderTruffleRunnable.AgreeToHelpElderTruffle -= AgreeToHelpElderTruffleRunnable_AgreeToHelpElderTruffle;
    }

    private void InitiateConditionsForWC2()
    {
        conditionsForWC2 = new bool[2];
        for (int i = 0; i < conditionsForWC2.Length; i++)
        {
            conditionsForWC2[i] = false;
        }
    }

    private void MeetConditionForWC2(int index)
    {
        conditionsForWC2[index] = true;
        CheckIfAllConditionsAreMet(conditionsForWC2);
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
        if (condition == conditionsForWC2)
        {
            interactableData.SetCurrentInteractionIndex(2);
        }
    }
}
