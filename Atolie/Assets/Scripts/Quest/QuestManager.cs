using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [SerializeField] private List<Quest> allQuests;
    private Dictionary<int, Quest> quests;
    private Dictionary<Quest, List<Quest>> childQuests;
    private Dictionary<Quest, bool> isCompleted;
    [SerializeField] private List<Quest> activeQuests;

    private QuestObserver questObserver;

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
            InitializeQuests();
            questObserver = QuestObserver.Instance;
            quests = new Dictionary<int, Quest>();
            SetUpQuestDictionary();
            childQuests = new Dictionary<Quest, List<Quest>>();
            SetUpChildQuests();
            isCompleted = new Dictionary<Quest, bool>();
            SetUpIsCompleted();
            SetUpAllQuestStepsQueue();
            CheckActiveQuestsOnAwake();
        }
        LoadActiveQuests();
    }

    private void InitializeQuests()
    {
        foreach (Quest quest in allQuests)
        {
            quest.Init();
        }
    }

    private void CheckActiveQuestsOnAwake()
    {
        Debug.Log("Scanning Active Quests...");
        if (activeQuests.Count > 0)
        {
            foreach (Quest quest in activeQuests)
            {
                quest.StartQuest();
            }
        }
    }

    public void LoadActiveQuests()
    {
        if (activeQuests.Count > 0)
        {
            foreach (Quest quest in activeQuests)
            {
                quest.LoadQuest();
            }
        }
    }

    private void SetUpQuestDictionary()
    {
        foreach (Quest quest in allQuests)
        {
            quests.Add(quest.questID, quest);
        }
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
        if (!isCompleted.TryAdd(quest, true))
        {
            isCompleted.Remove(quest);
            isCompleted.Add(quest, true);
        }
        activeQuests.Remove(quest);
        foreach (Quest childQuest in quest.childQuests)
        {
            childQuest.AddToCompletedPrereq(quest);
        }
    }

    public void AddToActiveQuests(Quest quest)
    {
        activeQuests.Add(quest);
        quest.StartQuest();
    }

    public void AddToActiveQuests(int questID)
    {
        Quest quest = quests.GetValueOrDefault(questID);
        activeQuests.Add(quest);
        quest.StartQuest();
    }

    public void LoadQuestStep(Quest.QuestStep questStep)
    {
        SerializedDictionary<string, Interaction> interactions = questStep.interactions;
        foreach (string interactable in interactions.Keys)
        {
            Interaction interaction = interactions.GetValueOrDefault(interactable);
            GameObject.FindObjectOfType<InteractionManager>().GetComponent<InteractionManager>().LoadInteraction(interactable, interaction);
        }
    }

    public void CompleteQuestStep(int questId, int stepNumber)
    {
        try
        {
            if (quests.TryGetValue(questId, out Quest quest))
            {
                if (isCompleted.TryGetValue(quest, out bool completed))
                {
                    if (!completed)
                    {
                        Debug.Log("Complete Quest Step: Quest " + questId.ToString() + " Step " + stepNumber.ToString());
                        if (quest.IsCurrentStep(stepNumber))
                        {
                            quest.NextQuestStep();
                        }
                        else
                        {
                            quest.SkipToStep(stepNumber + 1);
                        }
                    }
                }
            }
        }
        catch (NullReferenceException e)
        {
            Debug.Log("Null reference exception?");
        }

    }

    public void SkipToQuestStep(int questId, int stepNumber)
    {
        Quest quest = quests.GetValueOrDefault(questId);
        quest.SkipToStep(stepNumber);
    }
}