using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullRunnable : Runnable
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAndAdd()
    {
        NullRunnable temp = new NullRunnable();
        Runnables.Instance.AddToRunnables(temp.GetType().ToString(), temp);
        Debug.Log(Runnables.Instance.RunnablesSize());
    }

    public override void Run()
    {

    }
}
