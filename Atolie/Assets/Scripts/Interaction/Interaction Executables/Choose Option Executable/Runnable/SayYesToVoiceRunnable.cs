using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayYesToVoiceRunnable : Runnable
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitializeAndAdd()
    {
        SayYesToVoiceRunnable temp = new SayYesToVoiceRunnable();
        Runnables.AddToRunnables(temp.GetType().ToString(), temp);
        Debug.Log(Runnables.RunnablesSize());
    }

    public override void Run()
    {
        QuestManager.Instance.CompleteQuestStep(2, 0);
    }
}
