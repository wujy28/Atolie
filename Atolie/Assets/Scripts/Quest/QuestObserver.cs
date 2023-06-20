using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObserver : MonoBehaviour
{
    public static QuestObserver Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        // Subscribe to inventory and color palette's events
    }

    private void OnDestroy()
    {
        // Unsubscribe from inventory and color palette's events
    }

    private void InventoryManager_OnItemObtained(Item item)
    {
        switch (item)
        {
            // cases for all possible obtainable items, along with corresponding CompleteQuestStep() method calls.
        }
    }
}