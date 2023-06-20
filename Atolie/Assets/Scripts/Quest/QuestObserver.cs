using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class QuestObserver : MonoBehaviour
{
    public static QuestObserver Instance { get; private set; }

    private QuestManager questManager = QuestManager.Instance;

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
        InventoryManager.OnObtainedItemEvent += InventoryManager_OnObtainedItemEvent;
    }

    private void OnDestroy()
    {
        InventoryManager.OnObtainedItemEvent -= InventoryManager_OnObtainedItemEvent;
    }

    private void InventoryManager_OnObtainedItemEvent(Item item)
    {
        switch (item.name)
        {
            // cases for each possible collectible
            case "Coin":
                questManager.CompleteQuestStep(1, 0);
                break;
            case "Goop":
                break;
            case "Ticket":
                break;
        }
    }
}