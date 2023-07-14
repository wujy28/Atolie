using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToNonogramRunnable : Runnable
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAndAdd()
    {
        SwitchToNonogramRunnable temp = new SwitchToNonogramRunnable();
        Runnables.AddToRunnables(temp.GetType().ToString(), temp);
        Debug.Log(Runnables.RunnablesSize());
    }

    public override void Run()
    {
        GameManager.Instance.ChangeScene(GameScene.Nonogram);
    }
}
