using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HelpElderTruffleRunnable : Runnable
{
    public static event Action AgreeToHelpElderTruffle;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAndAdd()
    {
        HelpElderTruffleRunnable temp = new HelpElderTruffleRunnable();
        Runnables.AddToRunnables(temp.GetType().ToString(), temp);
        Debug.Log(Runnables.RunnablesSize());
    }

    public override void Run()
    {
        AgreeToHelpElderTruffle?.Invoke();
    }
}
