using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToSlidingPuzzleRunnable : Runnable
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAndAdd()
    {
        SwitchToSlidingPuzzleRunnable temp = new SwitchToSlidingPuzzleRunnable();
        Runnables.AddToRunnables(temp.GetType().ToString(), temp);
        Debug.Log(Runnables.RunnablesSize());
    }

    public override void Run()
    {
        GameManager.Instance.ChangeScene(GameScene.TrashCanSlidingPuzzle);
    }
}
