using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PondMazeTracker : MonoBehaviour
{
    public static PondMazeTracker Instance;

    [SerializeField] private Stage currentStage;
    [SerializeField] private GameObject welcomeScreen;
    [SerializeField] private GameObject puzzleCompletionScreen;
    [SerializeField] private GameObject exitGameConfirmationScreen;
    [SerializeField] private Interaction postPuzzleInteraction;
    [SerializeField] private Transform player;

    public static event Action PuzzleStart;

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
        GameManager.Instance.UpdateGameState(GameState.Puzzle);
    }

    public void UpdateStage(Stage newStage)
    {
        currentStage = newStage;

        switch (newStage)
        {
            case Stage.Welcome:
                player.GetComponent<FishMovement>().enabled = false; 
                welcomeScreen.SetActive(true);
                break;
            case Stage.PuzzleActive:
                player.GetComponent<FishMovement>().enabled = true;
                welcomeScreen.SetActive(false);
                PuzzleStart?.Invoke();
                break;
            case Stage.PuzzleComplete:
                player.GetComponent<FishMovement>().enabled = false;
                PuzzleCompleted();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newStage), newStage, null);
        }
    }

    public void EnterPuzzle()
    {
        UpdateStage(Stage.PuzzleActive);
    }

    private void PuzzleCompleted()
    {
        puzzleCompletionScreen.SetActive(true);
    }

    public enum Stage
    {
        Welcome,
        PuzzleActive,
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
        GameManager.Instance.ChangeScene(GameScene.MushroomGarden);
    }

    public void ExitCompletedPuzzle()
    {
        GameManager.Instance.UpdateGameState(GameState.Exploration);
        GameManager.Instance.PlayInterationAfterSceneChange(postPuzzleInteraction);
        GameManager.Instance.ChangeScene(GameScene.MushroomGarden);
    }
}
