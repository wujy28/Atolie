using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    public int questID;
    public string questName;
    public string questDescription;
    public List<Quest> prerequisiteQuests;
    public List<Quest> childQuests;
    public List<QuestStep> questSteps;

    private bool isActive;
    private QuestStep currentStep;
    private Queue<QuestStep> questStepsQueue;
    private bool isCompleted;
    private List<Quest> completedPrereq;

    public void Init()
    {
        questStepsQueue = new Queue<QuestStep>();
        currentStep = null;
        isActive = false;
        isCompleted = false;
        completedPrereq = new List<Quest>();
    }

    [System.Serializable]
    public class QuestStep
    {
        public int stepNumber;
        public string description;
        [SerializedDictionary("Interactable Name", "Interaction")]
        public SerializedDictionary<string, Interaction> interactions;
    }

    public void SetUpQuestStepsQueue()
    {
        int steps = 0;
        foreach(QuestStep step in questSteps)
        {
            step.stepNumber = steps;
            steps++;
            questStepsQueue.Enqueue(step);
        }
    }

    public void QuestCompleted()
    {
        isActive = false;
        isCompleted = true;
        QuestManager.Instance.CompleteQuest(this);
        Debug.Log("Quest: " + questName + " completed.");
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

    public void StartQuest()
    {
        Debug.Log("Quest Started: " + this.name);
        if (!isActive && !isCompleted)
        {
            isActive = true;
            NextQuestStep();
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
        QuestManager.Instance.LoadQuestStep(currentStep);
    }

    private void LoadAllStepsAndComplete()
    {
        while (questStepsQueue.Count > 0)
        {
            QuestStep nextStep = questStepsQueue.Dequeue();
            currentStep = nextStep;
            QuestManager.Instance.LoadQuestStep(currentStep);
        }
        QuestCompleted();
    }

    public void SkipToStep(int step)
    {
        Debug.Log("Quest: " + questName + " skipped to step " + step.ToString());
        if (step > questSteps.Count - 1)
        {
            LoadAllStepsAndComplete();
            return;
        }
        QuestStep questStep = questSteps[step];
        if (questStep.stepNumber < currentStep.stepNumber || questStep == null)
        {
            Debug.LogError("SkipToStep: Currently at Step " + currentStep.stepNumber + ". Cannot skip to step " + step + ".");
            return;
        }
        while (currentStep.stepNumber < step)
        {
            QuestStep nextStep = questStepsQueue.Dequeue();
            currentStep = nextStep;
            QuestManager.Instance.LoadQuestStep(currentStep);
        }
        QuestManager.Instance.LoadQuestStep(currentStep);
    }

    public bool IsCurrentStep(int step)
    {
        return currentStep.stepNumber == step;
    }

    public void LoadQuest()
    {
        QuestManager.Instance.LoadQuestStep(currentStep);
    }
}
