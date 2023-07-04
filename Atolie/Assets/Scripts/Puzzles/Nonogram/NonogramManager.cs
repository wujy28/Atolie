using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonogramManager : MonoBehaviour
{
    public static NonogramManager instance;

    public GameObject welcomeScreen;
    public GameObject stageCompletionScreen;
    public GameObject puzzleCompletionScreen;

    public GameObject[] stages;
    public int currentStage;

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
}
