using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToPondMazeRunnable : Runnable
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAndAdd()
    {
        SwitchToPondMazeRunnable temp = new SwitchToPondMazeRunnable();
        Runnables.AddToRunnables(temp.GetType().ToString(), temp);
        Debug.Log(Runnables.RunnablesSize());
    }

    public override void Run()
    {
        GameManager.Instance.ChangeScene(GameScene.PondMaze);
    }
}
