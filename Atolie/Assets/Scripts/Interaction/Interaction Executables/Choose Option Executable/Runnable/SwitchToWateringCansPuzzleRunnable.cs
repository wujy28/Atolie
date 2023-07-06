using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToWateringCansPuzzleRunnable : Runnable
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAndAdd()
    {
        SwitchToWateringCansPuzzleRunnable temp = new SwitchToWateringCansPuzzleRunnable();
        Runnables.AddToRunnables(temp.GetType().ToString(), temp);
        Debug.Log(Runnables.RunnablesSize());
    }

    public override void Run()
    {
        GameManager.Instance.ChangeScene(GameScene.WateringCansPuzzle);
    }
}
