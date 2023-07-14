using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToTraderRoRunnable : Runnable
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAndAdd()
    {
        SwitchToTraderRoRunnable temp = new SwitchToTraderRoRunnable();
        Runnables.AddToRunnables(temp.GetType().ToString(), temp);
        Debug.Log(Runnables.RunnablesSize());
    }

    public override void Run()
    {
        GameManager.Instance.ChangeScene(GameScene.TraderRos);
    }
}
