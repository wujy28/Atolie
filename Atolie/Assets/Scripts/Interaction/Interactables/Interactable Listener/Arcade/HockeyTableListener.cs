using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hockey Table Listener", menuName = "Interactable Listener/Arcade/Hockey Table Listener")]
public class HockeyTableListener : InteractableListener
{

    private bool[] conditionsForHT2;
    private bool[] conditionsForHT3;

    public override void SubscribeToAllEvents()
    {
        InventoryManager.OnObtainedItemEvent += InventoryManager_OnObtainedItemEvent;
        SubmitItemPopup.OnSubmittedItemEvent += SubmitItemPopup_OnSubmittedItemEvent;
        AgreeToFixHockeyStrikerRunnable.AgreeToFixHockeyStriker += AgreeToFixHockeyStrikerRunnable_AgreeToFixHockeyStriker;
        FixHockeyStrikerRunnable.OnHockeyStrikerFixed += FixHockeyStrikerRunnable_OnHockeyStrikerFixed;
        InitiateConditions(conditionsForHT2, 2);
        InitiateConditions(conditionsForHT3, 2);
    }

    private void FixHockeyStrikerRunnable_OnHockeyStrikerFixed()
    {
        interactableData.SetCurrentInteractionIndex(4);
    }

    private void AgreeToFixHockeyStrikerRunnable_AgreeToFixHockeyStriker()
    {
        interactableData.SetCurrentInteractionIndex(1);
    }

    private void SubmitItemPopup_OnSubmittedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Goop":
                MeetCondition(conditionsForHT3, 0);
                break;
            case "Handle":
                MeetCondition(conditionsForHT3, 1);
                break;
        }
    }

    private void InventoryManager_OnObtainedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Goop":
                MeetCondition(conditionsForHT2, 0);
                break;
            case "Handle":
                MeetCondition(conditionsForHT2, 1);
                break;
        }
    }

    public override void UnsubscribeFromAllEvents()
    {
        InventoryManager.OnObtainedItemEvent -= InventoryManager_OnObtainedItemEvent;
        SubmitItemPopup.OnSubmittedItemEvent -= SubmitItemPopup_OnSubmittedItemEvent;
        AgreeToFixHockeyStrikerRunnable.AgreeToFixHockeyStriker -= AgreeToFixHockeyStrikerRunnable_AgreeToFixHockeyStriker;
        FixHockeyStrikerRunnable.OnHockeyStrikerFixed -= FixHockeyStrikerRunnable_OnHockeyStrikerFixed;
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
        if (condition == conditionsForHT2)
        {
            interactableData.SetCurrentInteractionIndex(2);
        }
        if (condition == conditionsForHT3)
        {
            interactableData.SetCurrentInteractionIndex(3);
        }
    }
}
