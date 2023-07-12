using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trader Ro Listener", menuName = "Shop Listener/Trader Ro Listener")]
public class TraderRoListener : ScriptableObject
{
    [SerializeField] private int initialTalkDialogueIndex;
    public bool firstTimeEntering;

    public int currentTalkDialogueIndex;

    public void Reset()
    {
        firstTimeEntering = true;
        currentTalkDialogueIndex = initialTalkDialogueIndex;
    }

    public void SubscribeToAllEvents()
    {

    }

    public void UnsubscribeFromAllEvents()
    {

    }
}
