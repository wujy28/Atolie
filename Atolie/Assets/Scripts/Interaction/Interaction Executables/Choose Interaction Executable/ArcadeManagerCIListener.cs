using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Arcade Manager CI Listener", menuName = "Choose Interaction Listener/Arcade Manager CI Listener")]
public class ArcadeManagerCIListener : ChooseInteractionListener
{
    public override void SubscribeToAllEvents()
    {
        InventoryManager.OnObtainedItemEvent += InventoryManager_OnObtainedItemEvent;
        Coloring.OnColoredInEvent += Coloring_OnColoredInEvent;
        TangramManager.VendingMachineTangramCompleted += TangramManager_VendingMachineTangramCompleted;
        FixHockeyStrikerRunnable.OnHockeyStrikerFixed += FixHockeyStrikerRunnable_OnHockeyStrikerFixed;
    }

    private void FixHockeyStrikerRunnable_OnHockeyStrikerFixed()
    {
        executable.GoToStep(1, 1);
    }

    private void TangramManager_VendingMachineTangramCompleted()
    {
        executable.RemoveFromOptions(2);
    }

    private void Coloring_OnColoredInEvent(Transform interactable)
    {
        switch (interactable.name)
        {
            case "Hockey Table":
                executable.AddToOptions(1);
                break;
            case "Vending Machine":
                executable.AddToOptions(2);
                break;
        }
    }

    private void InventoryManager_OnObtainedItemEvent(Item item)
    {
        switch (item.name)
        {
            case "Tamagotchi":
                executable.AddToOptions(0);
                break;
            case "Battery":
                executable.RemoveFromOptions(0);
                break;
            case "Cup":
                executable.RemoveFromOptions(1);
                break;
        }

    }

    public override void UnsubscribeFromAllEvents()
    {
        InventoryManager.OnObtainedItemEvent -= InventoryManager_OnObtainedItemEvent;
        Coloring.OnColoredInEvent -= Coloring_OnColoredInEvent;
        TangramManager.VendingMachineTangramCompleted -= TangramManager_VendingMachineTangramCompleted;
        FixHockeyStrikerRunnable.OnHockeyStrikerFixed -= FixHockeyStrikerRunnable_OnHockeyStrikerFixed;
    }
}
