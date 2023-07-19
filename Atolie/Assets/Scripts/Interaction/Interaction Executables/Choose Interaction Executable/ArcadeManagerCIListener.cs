using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Arcade Manager CI Listener", menuName = "Choose Interaction Listener/Arcade Manager CI Listener")]
public class ArcadeManagerCIListener : ChooseInteractionListener
{
    [SerializeField] private bool interactedWithHT;
    [SerializeField] private bool interactedWithVM;

    public override void SubscribeToAllEvents()
    {
        interactedWithHT = false;
        interactedWithVM = false;
        InventoryManager.OnObtainedItemEvent += InventoryManager_OnObtainedItemEvent;
        // Coloring.OnColoredInEvent += Coloring_OnColoredInEvent;
        InteractionTrigger.OnInteractedWith += InteractionTrigger_OnInteractedWith;
        TangramManager.VendingMachineTangramCompleted += TangramManager_VendingMachineTangramCompleted;
        FixHockeyStrikerRunnable.OnHockeyStrikerFixed += FixHockeyStrikerRunnable_OnHockeyStrikerFixed;
        AgreeToFixHockeyStrikerRunnable.AgreeToFixHockeyStriker += AgreeToFixHockeyStrikerRunnable_AgreeToFixHockeyStriker;
        AgreeToHelpWithVMRunnable.AgreeToHelpWithVM += AgreeToHelpWithVMRunnable_AgreeToHelpWithVM;
    }

    private void AgreeToHelpWithVMRunnable_AgreeToHelpWithVM()
    {
        executable.GoToStep(2, 1);
    }

    private void AgreeToFixHockeyStrikerRunnable_AgreeToFixHockeyStriker()
    {
        executable.GoToStep(1, 2);
    }

    private void FixHockeyStrikerRunnable_OnHockeyStrikerFixed()
    {
        executable.GoToStep(1, 1);
    }

    private void TangramManager_VendingMachineTangramCompleted()
    {
        executable.RemoveFromOptions(2);
    }

    /*
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
    */

    private void InteractionTrigger_OnInteractedWith(Transform interactable)
    {
        switch (interactable.name)
        {
            case "Hockey Table":
                if (!interactedWithHT)
                {
                    executable.AddToOptions(1);
                    interactedWithHT = true;
                }
                break;
            case "Vending Machine":
                if (!interactedWithVM)
                {
                    executable.AddToOptions(2);
                    interactedWithVM = true;
                }
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
        interactedWithHT = false;
        interactedWithVM = false;
        InventoryManager.OnObtainedItemEvent -= InventoryManager_OnObtainedItemEvent;
        // Coloring.OnColoredInEvent -= Coloring_OnColoredInEvent;
        InteractionTrigger.OnInteractedWith -= InteractionTrigger_OnInteractedWith;
        TangramManager.VendingMachineTangramCompleted -= TangramManager_VendingMachineTangramCompleted;
        FixHockeyStrikerRunnable.OnHockeyStrikerFixed -= FixHockeyStrikerRunnable_OnHockeyStrikerFixed;
        AgreeToFixHockeyStrikerRunnable.AgreeToFixHockeyStriker -= AgreeToFixHockeyStrikerRunnable_AgreeToFixHockeyStriker;
        AgreeToHelpWithVMRunnable.AgreeToHelpWithVM -= AgreeToHelpWithVMRunnable_AgreeToHelpWithVM;
    }
}
