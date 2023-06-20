using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitInteractionRunnable : Runnable
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAndAdd()
    {
        ExitInteractionRunnable temp = new ExitInteractionRunnable();
        Runnables.AddToRunnables(temp.GetType().ToString(), temp);
        Debug.Log(Runnables.RunnablesSize());
    }

    public override void Run()
    {
        InteractionManager.Instance.forceExitInteraction();
    }
}
