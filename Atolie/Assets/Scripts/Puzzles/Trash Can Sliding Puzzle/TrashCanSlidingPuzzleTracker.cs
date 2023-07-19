using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashCanSlidingPuzzleTracker : MonoBehaviour
{
    public static TrashCanSlidingPuzzleTracker Instance;

    [SerializeField] private Stage currentStage;
    [SerializeField] private GameObject welcomeScreen;
    [SerializeField] private GameObject puzzleCompletionScreen;
    [SerializeField] private GameObject exitGameConfirmationScreen;
    [SerializeField] private GameObject blockerScreen;
    [SerializeField] private Transform dollarBill;
    [SerializeField] private SlidingPuzzleDifficultyManager difficultyManager;

    [SerializeField] private Interaction postPuzzleInteraction;

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
        UpdateStage(Stage.Welcome);
        puzzleCompletionScreen.SetActive(false);
        exitGameConfirmationScreen.SetActive(false);
        blockerScreen.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.Puzzle);
    }

    public void EnterPuzzle()
    {
        UpdateStage(Stage.PuzzleActive);
        difficultyManager.StartTimer();
    }

    private void PuzzleCompleted()
    {
        puzzleCompletionScreen.SetActive(true);
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
        GameManager.Instance.UpdateGameState(GameState.Exploration);
        GameManager.Instance.PlayInterationAfterSceneChange(postPuzzleInteraction);
        GameManager.Instance.ChangeScene(GameScene.CyberpunkCity);
    }

    public void UpdateStage(Stage newStage)
    {
        currentStage = newStage;

        switch (newStage)
        {
            case Stage.Welcome:
                welcomeScreen.SetActive(true);
                break;
            case Stage.PuzzleActive:
                welcomeScreen.SetActive(false);
                break;
            case Stage.GoalReached:
                blockerScreen.SetActive(true);
                break;
            case Stage.PuzzleComplete:
                blockerScreen.SetActive(false);
                PuzzleCompleted();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newStage), newStage, null);
        }
    }

    public enum Stage
    {
        Welcome,
        PuzzleActive,
        GoalReached,
        PuzzleComplete
    }
}
