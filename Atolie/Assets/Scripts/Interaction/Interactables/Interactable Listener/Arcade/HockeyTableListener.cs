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
        InitiateConditionsForHT2(2);
        InitiateConditionsForHT3(2);
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
                MeetConditionForHT3(0);
                break;
            case "Handle":
                MeetConditionForHT3(1);
                break;
        }
    }

    private void InventoryManager_OnObtainedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Goop":
                MeetConditionForHT2(0);
                break;
            case "Handle":
                MeetConditionForHT2(1);
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

    private void InitiateConditionsForHT2(int size)
    {
        conditionsForHT2 = new bool[size];
        for (int i = 0; i < conditionsForHT2.Length; i++)
        {
            conditionsForHT2[i] = false;
        }
    }

    private void InitiateConditionsForHT3(int size)
    {
        conditionsForHT3 = new bool[size];
        for (int i = 0; i < conditionsForHT3.Length; i++)
        {
            conditionsForHT3[i] = false;
        }
    }

    private void MeetConditionForHT2(int index)
    {
        conditionsForHT2[index] = true;
        CheckIfAllConditionsAreMet(conditionsForHT2);
    }

    private void MeetConditionForHT3(int index)
    {
        conditionsForHT3[index] = true;
        CheckIfAllConditionsAreMet(conditionsForHT3);
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
