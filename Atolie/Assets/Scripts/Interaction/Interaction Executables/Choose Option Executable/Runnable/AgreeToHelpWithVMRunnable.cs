using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class AgreeToHelpWithVMRunnable : Runnable
{
    public static event Action AgreeToHelpWithVM;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAndAdd()
    {
        AgreeToHelpWithVMRunnable temp = new AgreeToHelpWithVMRunnable();
        Runnables.AddToRunnables(temp.GetType().ToString(), temp);
        Debug.Log(Runnables.RunnablesSize());
    }

    public override void Run()
    {
        AgreeToHelpWithVM?.Invoke();
    }
}
