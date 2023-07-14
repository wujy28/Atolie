using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class AgreeToHelpElderTruffleRunnable : Runnable
{
    public static event Action AgreeToHelpElderTruffle;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAndAdd()
    {
        AgreeToHelpElderTruffleRunnable temp = new AgreeToHelpElderTruffleRunnable();
        Runnables.AddToRunnables(temp.GetType().ToString(), temp);
        Debug.Log(Runnables.RunnablesSize());
    }

    public override void Run()
    {
        AgreeToHelpElderTruffle?.Invoke();
    }
}
