using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Runnable
{
    /*
        Please add the method below in all child classes (replace [Runnable Type]):

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void InitializeAndAdd()
        {
            [Runnable Type] temp = new [Runnable Type]();
            Runnables.AddToRunnables(temp.GetType().ToString(), temp);
            Debug.Log(Runnables.RunnablesSize());
        }
    */

    public abstract void Run();
}
