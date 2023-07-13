using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElderTruffle CI Listener", menuName = "Choose Interaction Listener/ElderTruffle CI Listener")]
public class ElderTruffleCIListener : ChooseInteractionListener
{
    public override void SubscribeToAllEvents()
    {
        HelpElderTruffleRunnable.AgreeToHelpElderTruffle += HelpElderTruffleRunnable_AgreeToHelpElderTruffle;
        TrimPondRunnable.OnPondTrimmed += TrimPondRunnable_OnPondTrimmed;
        WaterMushroomBedsRunnable.OnMushroomBedsWatered += WaterMushroomBedsRunnable_OnMushroomBedsWatered;
    }

    private void WaterMushroomBedsRunnable_OnMushroomBedsWatered()
    {
        executable.RemoveFromOptions(1);
    }

    private void TrimPondRunnable_OnPondTrimmed()
    {
        executable.RemoveFromOptions(0);
    }

    private void HelpElderTruffleRunnable_AgreeToHelpElderTruffle()
    {
        executable.AddToOptions(0);
        executable.AddToOptions(1);
    }

    public override void UnsubscribeFromAllEvents()
    {
        HelpElderTruffleRunnable.AgreeToHelpElderTruffle -= HelpElderTruffleRunnable_AgreeToHelpElderTruffle;
        TrimPondRunnable.OnPondTrimmed -= TrimPondRunnable_OnPondTrimmed;
        WaterMushroomBedsRunnable.OnMushroomBedsWatered -= WaterMushroomBedsRunnable_OnMushroomBedsWatered;
    }
}
