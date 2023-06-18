using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [SerializeField] private List<Quest> allQuests;
    private Dictionary<Quest, List<Quest>> childQuests;
    private Dictionary<Quest, bool> isCompleted;
    [SerializeField] private List<Quest> activeQuests;

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
        allQuests = new List<Quest>();
        childQuests = new Dictionary<Quest, List<Quest>>();
        SetUpChildQuests();
        isCompleted = new Dictionary<Quest, bool>();
        SetUpIsCompleted();
        activeQuests = new List<Quest>();
        SetUpAllQuestStepsQueue();
    }

    private void SetUpAllQuestStepsQueue()
    {
        foreach (Quest quest in allQuests)
        {
            quest.SetUpQuestStepsQueue();
        }
    }

    private void SetUpChildQuests()
    {
        foreach (Quest quest in allQuests)
        {
            childQuests.Add(quest, quest.childQuests);
        }
    }

    private void SetUpIsCompleted()
    {
        foreach (Quest quest in allQuests)
        {
            isCompleted.Add(quest, false);
        }
    }

    public void CompleteQuest(Quest quest)
    {
        isCompleted.Add(quest, true);
        activeQuests.Remove(quest);
        foreach (Quest childQuest in quest.childQuests)
        {
            childQuest.AddToCompletedPrereq(quest);
        }
    }

    public void AddToActiveQuests(Quest quest)
    {
        activeQuests.Add(quest);
        
    }

    public void LoadNewQuestStep(Quest.QuestStep questStep)
    {
        SerializedDictionary<string, Interaction> interactions = questStep.interactions;
        foreach (string interactable in interactions.Keys)
        {
            Interaction interaction = interactions.GetValueOrDefault(interactable);
            InteractionManager.Instance.LoadInteraction(interactable, interaction);
        }
    }
}