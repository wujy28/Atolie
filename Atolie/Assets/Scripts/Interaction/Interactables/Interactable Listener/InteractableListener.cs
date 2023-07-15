using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableListener : ScriptableObject
{
    public InteractableData interactableData;

    public abstract void SubscribeToAllEvents();

    public abstract void UnsubscribeFromAllEvents();
}
