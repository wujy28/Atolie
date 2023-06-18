using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchToPondMazeRunnable : Runnable
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAndAdd()
    {
        SwitchToPondMazeRunnable temp = new SwitchToPondMazeRunnable();
        Runnables.Instance.AddToRunnables(temp.GetType().ToString(), temp);
        Debug.Log(Runnables.Instance.RunnablesSize());
    }

    public override void Run()
    {
        SceneManager.LoadScene(3);
    }
}
