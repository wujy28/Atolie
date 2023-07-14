using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class FixHockeyStrikerRunnable : Runnable
{
    public static event Action OnHockeyStrikerFixed;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAndAdd()
    {
        FixHockeyStrikerRunnable temp = new FixHockeyStrikerRunnable();
        Runnables.AddToRunnables(temp.GetType().ToString(), temp);
        Debug.Log(Runnables.RunnablesSize());
    }

    public override void Run()
    {
        OnHockeyStrikerFixed?.Invoke();
    }
}
