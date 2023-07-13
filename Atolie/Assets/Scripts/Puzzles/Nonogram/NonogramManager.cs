using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonogramManager : MonoBehaviour
{
    public static NonogramManager instance;

    public GameObject welcomeScreen;
    public GameObject stageCompletionScreen;
    public GameObject puzzleCompletionScreen;
    [SerializeField] private GameObject exitGameConfirmationScreen;

    public GameObject[] stages;
    public int currentStage;

    // Interaction to play after completing puzzle
    [SerializeField] private Interaction postPuzzleInteraction;

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
    }

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
        GameManager.Instance.UpdateGameState(GameState.Exploration);
        GameManager.Instance.PlayInterationAfterSceneChange(postPuzzleInteraction);
        GameManager.Instance.ChangeScene(GameScene.Arcade);
    }
}
