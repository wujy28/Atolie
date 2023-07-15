using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WirePuzzleTracker : MonoBehaviour
{
    public static WirePuzzleTracker Instance;

    [SerializeField] private Stage currentStage;
    [SerializeField] private GameObject levelSelectScreen;
    [SerializeField] private WirePuzzleLevelSelectManager levelSelectManager;
    [SerializeField] private GameObject stage_1;
    [SerializeField] private GameObject stage_2;
    [SerializeField] private GameObject stage_3;
    [SerializeField] private GameObject stage_4;
    [SerializeField] private GameObject welcomeScreen;
    [SerializeField] private GameObject instructions;
    [SerializeField] private GameObject stageCompletionScreen;
    [SerializeField] private GameObject gridBlocker;
    [SerializeField] private GameObject puzzleCompletionScreen;
    [SerializeField] private GameObject exitGameConfirmationScreen;

    [SerializeField] private Interaction postPuzzleInteraction;

    public static event Action<Stage> OnStageChanged;

    public static event Action WirePuzzleCompleted;

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
        levelSelectScreen.SetActive(false);
        stage_1.SetActive(false);
        stage_2.SetActive(false);
        stage_3.SetActive(false);
        stage_4.SetActive(false);
        welcomeScreen.SetActive(false);
        instructions.SetActive(false);
        stageCompletionScreen.SetActive(false);
        gridBlocker.SetActive(false);
        puzzleCompletionScreen.SetActive(false);
        exitGameConfirmationScreen.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.Puzzle);
        UpdateStage(Stage.Welcome);
    }

    public void UpdateStage(Stage newStage)
    {
        currentStage = newStage;

        switch (newStage)
        {
            case Stage.Welcome:
                welcomeScreen.SetActive(true);
                levelSelectScreen.SetActive(true);
                break;
            case Stage.Stage_1:
                levelSelectManager.UnlockLevel(1);
                break;
            case Stage.Stage_2:
                levelSelectManager.UnlockLevel(2);
                break;
            case Stage.Stage_3:
                levelSelectManager.UnlockLevel(3);
                break;
            case Stage.Stage_4:
                levelSelectManager.UnlockLevel(4);
                break;
            case Stage.PuzzleComplete:
                PuzzleCompleted();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newStage), newStage, null);
        }

        OnStageChanged?.Invoke(newStage);
    }

    public void EnterStage(int stageNumber)
    {
        switch (stageNumber)
        {
            case 1:
                levelSelectScreen.SetActive(false);
                stage_1.SetActive(true);
                OpenInstructionsScreen();
                break;
            case 2:
                levelSelectScreen.SetActive(false);
                stage_2.SetActive(true);
                break;
            case 3:
                levelSelectScreen.SetActive(false);
                stage_3.SetActive(true);
                break;
            case 4:
                levelSelectScreen.SetActive(false);
                stage_4.SetActive(true);
                break;
        }
    }

    public void ExitStage(int stageNumber)
    {
        switch (stageNumber)
        {
            case 1:
                stage_1.SetActive(false);
                levelSelectScreen.SetActive(true);
                break;
            case 2:
                stage_2.SetActive(false);
                levelSelectScreen.SetActive(true);
                break;
            case 3:
                stage_3.SetActive(false);
                levelSelectScreen.SetActive(true);
                break;
            case 4:
                stage_4.SetActive(false);
                levelSelectScreen.SetActive(true);
                break;
        }

        DisableStageBlocker();
    }

    public void EnterPuzzle()
    {
        welcomeScreen.SetActive(false);
        UpdateStage(Stage.Stage_1);
    }

    public void OpenInstructionsScreen()
    {
        instructions.SetActive(true);
    }

    public void CloseInstructionsScreen()
    {
        instructions.SetActive(false);
    }

    public void EnableStageBlocker()
    {
        gridBlocker.SetActive(true);
    }

    public void DisableStageBlocker()
    {
        gridBlocker.SetActive(false);
    }

    public void CompleteStage(Stage stage)
    {
        switch (stage)
        {
            case Stage.Stage_1:
                levelSelectManager.CompleteLevel(1);
                break;
            case Stage.Stage_2:
                levelSelectManager.CompleteLevel(2);
                break;
            case Stage.Stage_3:
                levelSelectManager.CompleteLevel(3);
                break;
            case Stage.Stage_4:
                levelSelectManager.CompleteLevel(4);
                break;
        }
        stageCompletionScreen.SetActive(true);
    }

    public void NextStage()
    {
        stageCompletionScreen.SetActive(false);
        EnableStageBlocker();
        UpdateStage(currentStage + 1);
    }


    private void PuzzleCompleted()
    {
        stageCompletionScreen.SetActive(false);
        puzzleCompletionScreen.SetActive(true);
    }

    public enum Stage
    {
        Welcome,
        Stage_1,
        Stage_2,
        Stage_3,
        Stage_4,
        PuzzleComplete
    }

    public void PausePuzzle()
    {
        exitGameConfirmationScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumePuzzle()
    {
        Time.timeScale = 1;
        exitGameConfirmationScreen.SetActive(false);
    }

    public void ExitPuzzle()
    {
        Time.timeScale = 1;
        GameManager.Instance.UpdateGameState(GameState.Exploration);
        GameManager.Instance.ChangeScene(GameScene.CyberpunkCity);
    }

    public void ExitCompletedPuzzle()
    {
        WirePuzzleCompleted?.Invoke();
        GameManager.Instance.UpdateGameState(GameState.Exploration);
        GameManager.Instance.PlayInterationAfterSceneChange(postPuzzleInteraction);
        GameManager.Instance.ChangeScene(GameScene.CyberpunkCity);
    }
}
