using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionExecutable : ScriptableObject
{
    public abstract void execute();

    public static void currentExecutableCompleted()
    {
        InteractionManager.Instance.executeNextInteractionExecutable();
    }
}
