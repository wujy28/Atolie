using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TangramManager : MonoBehaviour
{
    public static TangramManager instance;

    public Canvas canvas; //try

    public GameObject[] stages;
    public GameObject[] grids; //array of the grids
    public GameObject[] shapeStorages; //array of ShapeStorages
    public int currentStage;

    public GameObject welcomeScreen;
    public GameObject stageCompletionScreen;
    public GameObject puzzleCompletionScreen;
    [SerializeField] private GameObject exitGameConfirmationScreen;

    // Interaction to play after completing puzzle
    [SerializeField] private Interaction postPuzzleInteraction;

    // Event for completion of tangram
    public static event Action VendingMachineTangramCompleted;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        currentStage = 0;
        welcomeScreen.SetActive(true);
        stageCompletionScreen.SetActive(false);
        puzzleCompletionScreen.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.Puzzle);
    }

    //Function to reset stage
    public void ResetStage()
    {
        this.grids[currentStage].GetComponent<Grid>().ResetGrid();
        this.shapeStorages[currentStage].GetComponent<ShapeStorage>().ResetAllShapes();
    }

    //Functions to manage screens that appear in puzzle
    public void CloseWelcomeScreen()
    {
        welcomeScreen.SetActive(false);
        stages[currentStage].SetActive(true);
    }

    public void OpenStageCompletedScreen()
    {
        stageCompletionScreen.SetActive(true);
    }

    public void OpenPuzzleCompletionScreen()
    {
        puzzleCompletionScreen.SetActive(true);
    }
    
    public void GoToNextStage()
    {
        stageCompletionScreen.SetActive(false);
        stages[currentStage].SetActive(false);
        stages[currentStage + 1].SetActive(true);
        currentStage++;
    }

    // Functions for puzzle pause, exit and completion
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
        GameManager.Instance.ChangeScene(GameScene.Arcade);
    }

    public void ExitCompletedPuzzle()
    {
        VendingMachineTangramCompleted?.Invoke();
        GameManager.Instance.UpdateGameState(GameState.Exploration);
        GameManager.Instance.PlayInterationAfterSceneChange(postPuzzleInteraction);
        GameManager.Instance.ChangeScene(GameScene.Arcade);
    }
}
