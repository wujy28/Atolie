using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TangramManager : MonoBehaviour
{
    public static TangramManager instance;

    public Canvas canvas; //try

    /*public GameObject firstStage;
    public GameObject secondStage;
    public GameObject thirdStage;*/
    public GameObject[] stages;
    public GameObject[] grids; //array of the grid?
    public GameObject[] shapeStorages; //array of ShapeStorage
    public int currentStage;

    public GameObject welcomeScreen;
    public GameObject stageCompletionScreen;
    public GameObject puzzleCompletionScreen;
    [SerializeField] private GameObject exitGameConfirmationScreen;

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

    public void ResetStage()
    {
        /*var resetGameobject = GameObject.Instantiate(stage);
        resetGameobject.transform.parent = this.canvas.transform;
        resetGameobject.transform.localScale = new Vector3(1f, 1f, 1f);
        Destroy(stage);

        //GameObject temp = stage.GetComponent<GameObject>();
        //Instantiate(temp);
        //stage.SetActive(false);
        //stage.SetActive(true);

        //SceneManager.LoadScene("Vending Machine Puzzle");*/

        this.grids[currentStage].GetComponent<Grid>().ResetGrid();
        this.shapeStorages[currentStage].GetComponent<ShapeStorage>().ResetAllShapes(); //this is the problem
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
