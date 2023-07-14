using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class AgreeToFixHockeyStrikerRunnable : Runnable
{
    public static event Action AgreeToFixHockeyStriker;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAndAdd()
    {
        AgreeToFixHockeyStrikerRunnable temp = new AgreeToFixHockeyStrikerRunnable();
        Runnables.AddToRunnables(temp.GetType().ToString(), temp);
        Debug.Log(Runnables.RunnablesSize());
    }

    public override void Run()
    {
        AgreeToFixHockeyStriker?.Invoke();
    }
}
