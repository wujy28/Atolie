using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pet Shop Salesman Listener", menuName = "Interactable Listener/City/Pet Shop Salesman Listener")]
public class PetShopSalesmanListener : InteractableListener
{
    private bool[] conditionsForPSS2;

    public override void SubscribeToAllEvents()
    {
        InventoryManager.OnObtainedItemEvent += InventoryManager_OnObtainedItemEvent;
        Coloring.OnColoredInEvent += Coloring_OnColoredInEvent;
        InitiateConditions(conditionsForPSS2, 2);
    }

    private void Coloring_OnColoredInEvent(Transform interactable)
    {
        switch (interactable.name)
        {
            case "Pet Shop":
                interactableData.SetCurrentInteractionIndex(1);
                MeetCondition(conditionsForPSS2, 1);
                break;
        }
    }

    private void InventoryManager_OnObtainedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Ripped Bill":
                MeetCondition(conditionsForPSS2, 0);
                break;
            case "Handle":
                interactableData.SetCurrentInteractionIndex(3);
                break;
            case "Money":
                interactableData.SetCurrentInteractionIndex(4);
                break;
            case "Winding Key":
                interactableData.SetCurrentInteractionIndex(5);
                break;
        }
    }

    public override void UnsubscribeFromAllEvents()
    {
        InventoryManager.OnObtainedItemEvent -= InventoryManager_OnObtainedItemEvent;
        Coloring.OnColoredInEvent -= Coloring_OnColoredInEvent;
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
        if (condition == conditionsForPSS2)
        {
            interactableData.SetCurrentInteractionIndex(2);
        }
    }
}
