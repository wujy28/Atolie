using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class UnlockMushroomHouseRunnable : Runnable
{
    public static event Action OnMushroomHouseUnlocked;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAndAdd()
    {
        UnlockMushroomHouseRunnable temp = new UnlockMushroomHouseRunnable();
        Runnables.AddToRunnables(temp.GetType().ToString(), temp);
        Debug.Log(Runnables.RunnablesSize());
    }

    public override void Run()
    {
        OnMushroomHouseUnlocked?.Invoke();
    }
}
