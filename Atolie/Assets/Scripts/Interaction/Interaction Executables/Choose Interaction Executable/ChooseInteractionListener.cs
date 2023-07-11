using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChooseInteractionListener : ScriptableObject
{
    public ChooseInteractionExecutable executable;

    public abstract void SubscribeToAllEvents();

    public abstract void UnsubscribeFromAllEvents();
}
