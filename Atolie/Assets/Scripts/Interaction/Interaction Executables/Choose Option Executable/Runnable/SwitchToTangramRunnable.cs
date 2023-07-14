using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToTangramRunnable : Runnable
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAndAdd()
    {
        SwitchToTangramRunnable temp = new SwitchToTangramRunnable();
        Runnables.AddToRunnables(temp.GetType().ToString(), temp);
        Debug.Log(Runnables.RunnablesSize());
    }

    public override void Run()
    {
        GameManager.Instance.ChangeScene(GameScene.VendingMachineTangram);
    }
}
