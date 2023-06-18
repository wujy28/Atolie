using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    public int questID;
    public List<Quest> prerequisiteQuests;
    public List<Quest> childQuests;
    public List<QuestStep> questSteps;

    private QuestStep currentStep;
    private Queue<QuestStep> questStepsQueue;
    private bool isCompleted;
    private List<Quest> completedPrereq;

    private void Awake()
    {
        questStepsQueue = new Queue<QuestStep>();
        isCompleted = false;
        completedPrereq = new List<Quest>();
    }

    [System.Serializable]
    public class QuestStep
    {
        [SerializedDictionary("Interactable Name", "Interaction")]
        public SerializedDictionary<string, Interaction> interactions;
    }

    public void SetUpQuestStepsQueue()
    {
        foreach(QuestStep step in questSteps)
        {
            questStepsQueue.Enqueue(step);
        }
    }

    public void QuestCompleted()
    {
        isCompleted = true;
        QuestManager.Instance.CompleteQuest(this);
    }

    public void AddToCompletedPrereq(Quest quest)
    {
        if (prerequisiteQuests.Contains(quest))
        {
            completedPrereq.Add(quest);
        }
        CheckIfAllPrereqCompleted();
    }

    private void CheckIfAllPrereqCompleted()
    {
        if (completedPrereq.Count == prerequisiteQuests.Count)
        {
            QuestManager.Instance.AddToActiveQuests(this);
        }
    }

    public void NextQuestStep()
    {
        if (questStepsQueue.Count == 0)
        {
            QuestCompleted();
            return;
        }

        QuestStep nextStep = questStepsQueue.Dequeue();
        currentStep = nextStep;
        QuestManager.Instance.LoadNewQuestStep(nextStep);
    }
}
