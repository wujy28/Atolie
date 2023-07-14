using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vending Machine Listener", menuName = "Interactable Listener/Arcade/Vending Machine Listener")]
public class VendingMachineListener : InteractableListener
{
    public override void SubscribeToAllEvents()
    {
        AgreeToHelpWithVMRunnable.AgreeToHelpWithVM += AgreeToHelpWithVMRunnable_AgreeToHelpWithVM;
        TangramManager.VendingMachineTangramCompleted += TangramManager_VendingMachineTangramCompleted;
    }

    private void TangramManager_VendingMachineTangramCompleted()
    {
        interactableData.SetCurrentInteractionIndex(2);
    }

    private void AgreeToHelpWithVMRunnable_AgreeToHelpWithVM()
    {
        interactableData.SetCurrentInteractionIndex(1);
    }

    public override void UnsubscribeFromAllEvents()
    {
        AgreeToHelpWithVMRunnable.AgreeToHelpWithVM -= AgreeToHelpWithVMRunnable_AgreeToHelpWithVM;
        TangramManager.VendingMachineTangramCompleted -= TangramManager_VendingMachineTangramCompleted;
    }
}
