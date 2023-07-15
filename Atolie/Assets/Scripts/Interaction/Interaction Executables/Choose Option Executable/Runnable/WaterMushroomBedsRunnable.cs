using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class WaterMushroomBedsRunnable : Runnable
{
    public static event Action OnMushroomBedsWatered;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAndAdd()
    {
        WaterMushroomBedsRunnable temp = new WaterMushroomBedsRunnable();
        Runnables.AddToRunnables(temp.GetType().ToString(), temp);
        Debug.Log(Runnables.RunnablesSize());
    }

    public override void Run()
    {
        OnMushroomBedsWatered?.Invoke();
    }
}
