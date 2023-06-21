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
            InteractionManager.Instance.LoadInteraction(interactable, interaction);
        }
    }

    public void CompleteQuestStep(int questId, int stepNumber)
    {
        Quest quest = quests.GetValueOrDefault(questId);
        if (quest.IsCurrentStep(stepNumber))
        {
            quest.NextQuestStep();
        }
    }

    public void SkipToQuestStep(int questId, int stepNumber)
    {
        Quest quest = quests.GetValueOrDefault(questId);
        quest.SkipToStep(stepNumber);
    }
}