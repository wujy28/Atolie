using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WateringCansPuzzleTracker : MonoBehaviour
{
    public static WateringCansPuzzleTracker Instance;

    [SerializeField] private Stage currentStage;
    [SerializeField] private TutorialStep currentTutorialStep;
    [SerializeField] private Text text;
    [SerializeField] private GameObject stageCompletionScreen;
    [SerializeField] private GameObject tutorialInstructions;

    public bool inTutorial;

    public static event Action<Stage> OnStageChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        UpdateStage(Stage.Tutorial);
        stageCompletionScreen.SetActive(false);
    }

    public void UpdateStage(Stage newStage)
    {
        currentStage = newStage;

        switch (newStage)
        {
            case Stage.Tutorial:
                HandleTutorial();
                break;
            case Stage.Stage_1:
                UpdatePuzzleComponents(1);
                break;
            case Stage.Stage_2:
                UpdatePuzzleComponents(6);
                break;
            case Stage.Stage_3:
                UpdatePuzzleComponents(5);
                break;
            case Stage.Stage_4:
                UpdatePuzzleComponents(2);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newStage), newStage, null);
        }

        OnStageChanged?.Invoke(newStage);
    }

    public void UpdateTutorialStep(TutorialStep newStep)
    {
        currentTutorialStep = newStep;

        switch (newStep)
        {
            case TutorialStep.TargetDrag:
                tutorialInstructions.transform.Find("TargetDrag").gameObject.SetActive(true);
                break;
            case TutorialStep.TransferBetween:
                tutorialInstructions.transform.Find("TargetDrag").gameObject.SetActive(false);
                tutorialInstructions.transform.Find("TransferBetween").gameObject.SetActive(true);
                break;
            case TutorialStep.WateringCan:
                tutorialInstructions.transform.Find("TransferBetween").gameObject.SetActive(false);
                tutorialInstructions.transform.Find("WateringCan").gameObject.SetActive(true);
                break;
            case TutorialStep.Fertilizer:
                tutorialInstructions.transform.Find("WateringCan").gameObject.SetActive(false);
                tutorialInstructions.transform.Find("Fertilizer").gameObject.SetActive(true);
                break;
            case TutorialStep.EndTutorial:
                tutorialInstructions.SetActive(false);
                inTutorial = false;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newStep), newStep, null);
        }
    }

    public void HandleTutorial()
    {
        tutorialInstructions.SetActive(true);
        tutorialInstructions.transform.Find("Welcome").gameObject.SetActive(true);
    }

    public void EnterTutorial()
    {
        inTutorial = true;
        tutorialInstructions.transform.Find("Welcome").gameObject.SetActive(false);
        tutorialInstructions.transform.Find("Tutorial Label").gameObject.SetActive(true);
        UpdatePuzzleComponents(3);
        UpdateTutorialStep(TutorialStep.TargetDrag);
    }

    public void NextTutorialStep()
    {
        UpdateTutorialStep(currentTutorialStep + 1);
    }

    public void CompleteCurrentStage()
    {
        stageCompletionScreen.SetActive(true);
    }

    public void NextStage()
    {
        UpdateStage(currentStage + 1);
    }

    private void UpdatePuzzleComponents(int targetCapacity)
    {
        WateringCan.setTargetCapacity(targetCapacity);
        text.text = targetCapacity.ToString();
        stageCompletionScreen.SetActive(false);
    }

    public enum Stage
    {
        Tutorial,
        Stage_1,
        Stage_2,
        Stage_3,
        Stage_4
    }

    public enum TutorialStep
    {
        TargetDrag,
        TransferBetween,
        WateringCan,
        Fertilizer,
        EndTutorial
    }
}
