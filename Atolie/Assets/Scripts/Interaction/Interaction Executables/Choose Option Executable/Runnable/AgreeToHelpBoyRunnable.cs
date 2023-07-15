using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class AgreeToHelpBoyRunnable : Runnable
{
    public static event Action AgreeToHelpBoy;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAndAdd()
    {
        AgreeToHelpBoyRunnable temp = new AgreeToHelpBoyRunnable();
        Runnables.AddToRunnables(temp.GetType().ToString(), temp);
        Debug.Log(Runnables.RunnablesSize());
    }

    public override void Run()
    {
        AgreeToHelpBoy?.Invoke();
    }
}
