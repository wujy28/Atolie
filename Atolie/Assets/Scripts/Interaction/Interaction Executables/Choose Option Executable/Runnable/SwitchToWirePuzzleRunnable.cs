using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToWirePuzzleRunnable : Runnable
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAndAdd()
    {
        SwitchToWirePuzzleRunnable temp = new SwitchToWirePuzzleRunnable();
        Runnables.AddToRunnables(temp.GetType().ToString(), temp);
        Debug.Log(Runnables.RunnablesSize());
    }

    public override void Run()
    {
        GameManager.Instance.ChangeScene(GameScene.WireConnectingPuzzle);
    }
}
