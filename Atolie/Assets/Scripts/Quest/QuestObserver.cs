using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class QuestObserver : MonoBehaviour
{
    public static QuestObserver Instance { get; private set; }

    // private static QuestManager questManager = QuestManager.Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        InventoryManager.OnObtainedItemEvent += InventoryManager_OnObtainedItemEvent;
        Coloring.OnColoredInEvent += Coloring_OnColoredInEvent;
        SubmitItemPopup.OnSubmittedItemEvent += SubmitItemPopup_OnSubmittedItemEvent;
    }

    private void OnDestroy()
    {
        InventoryManager.OnObtainedItemEvent -= InventoryManager_OnObtainedItemEvent;
        Coloring.OnColoredInEvent -= Coloring_OnColoredInEvent;
        SubmitItemPopup.OnSubmittedItemEvent -= SubmitItemPopup_OnSubmittedItemEvent;
    }

    private void InventoryManager_OnObtainedItemEvent(Item item)
    {
        switch (item.name)
        {
            // cases for each possible collectible
            case "Token":
                break;
            case "Fish":
                GetComponent<QuestManager>().CompleteQuestStep(5, 0);
                break;
            case "Ticket":
                GetComponent<QuestManager>().CompleteQuestStep(4, 0);
                GetComponent<QuestManager>().CompleteQuestStep(3, 1);
                break;
            case "Cup":
                GetComponent<QuestManager>().CompleteQuestStep(6, 0);
                break;
            case "Elixir":
                GetComponent<QuestManager>().CompleteQuestStep(6, 2);
                break;
            case "Shears":
                GetComponent<QuestManager>().CompleteQuestStep(8, 1);
                break;

        }
    }

    private void Coloring_OnColoredInEvent(Transform interactable)
    {/*
        switch (interactable.name)
        {
            case "Pet Shop":
                GetComponent<QuestManager>().CompleteQuestStep(5, 0);
                break;
            case "Prize Counter":
                GetComponent<QuestManager>().CompleteQuestStep(4, 0);
                break;
        }
        */
    }

    private void SubmitItemPopup_OnSubmittedItemEvent(Item item)
    {
        switch (item.name)
        {
            // cases for each possible collectible
            case "Token":
                GetComponent<QuestManager>().CompleteQuestStep(3, 0);
                break;
            case "Ticket":
                break;
            case "Cup":
                GetComponent<QuestManager>().CompleteQuestStep(6, 1);
                break;
            case "Fish":
                GetComponent<QuestManager>().CompleteQuestStep(8, 0);
                break;
            case "Elixir":
                GetComponent<QuestManager>().CompleteQuestStep(9, 0);
                break;
            case "Shears":
                GetComponent<QuestManager>().CompleteQuestStep(10, 0);
                break;
        }
    }
}