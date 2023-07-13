using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;
    public GameScene Scene;

    public static event Action<GameState> OnGameStateChanged;

    // Data
    // [SerializeField] private InteractablesData interactableData;

    // References
    private InventoryManager inventory;
    private DataManager dataManager;
    private ColorManager colorManager;

    private Interaction postSceneChangeInteraction;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            UpdateGameState(State);
        }
        inventory = FindObjectOfType<InventoryManager>();
        dataManager = FindObjectOfType<DataManager>();
        colorManager = FindObjectOfType<ColorManager>();
    }

    private void OnDestroy()
    {
        if (this == Instance)
        {
            // interactableData.ResetData();
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (postSceneChangeInteraction != null)
        {
            InteractionManager interactionManager = FindObjectOfType<InteractionManager>();
            if (interactionManager != null)
            {
                interactionManager.playPostPuzzleInteraction(postSceneChangeInteraction);
            }
        }
        postSceneChangeInteraction = null;
    }

    public void ChangeScene(GameScene scene)
    {
        switch (scene)
        {
            case GameScene.StartScreen:
                SceneManager.LoadScene(0);
                break;
            case GameScene.InstructionsScreen:
                SceneManager.LoadScene(6);
                break;
            case GameScene.Arcade:
                SceneManager.LoadScene(1);
                break;
            case GameScene.CyberpunkCity:
                SceneManager.LoadScene(2);
                break;
            case GameScene.MushroomGarden:
                SceneManager.LoadScene(3);
                break;
            case GameScene.DDR:
                SceneManager.LoadScene(7);
                break;
            case GameScene.WateringCansPuzzle:
                SceneManager.LoadScene(5);
                break;
            case GameScene.PondMaze:
                SceneManager.LoadScene(4);
                break;
            case GameScene.WireConnectingPuzzle:
                SceneManager.LoadScene(9);
                break;
            case GameScene.VendingMachineTangram:
                SceneManager.LoadScene(10);
                break;
            case GameScene.TrashCanSlidingPuzzle:
                SceneManager.LoadScene(11);
                break;
            case GameScene.Nonogram:
                SceneManager.LoadScene(12);
                break;
            case GameScene.TraderRos:
                SceneManager.LoadScene(13);
                break;
            case GameScene.DemoEnd:
                SceneManager.LoadScene(8);
                break;
        }

        this.Scene = scene;
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                HandleMainMenu();
                break;
            case GameState.Exploration:
                HandleExploration();
                break;
            case GameState.Interaction:
                HandleInteraction();
                break;
            case GameState.PaintBucketMode:
                HandlePaintBucketMode();
                break;
            case GameState.Puzzle:
                HandlePuzzle();
                break;
            case GameState.Finish:
                HandleFinish();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleFinish()
    {
        Debug.Log("Finish Demo");
        if (inventory != null)
        {
            Destroy(inventory.gameObject);
        }
        if (dataManager != null)
        {
            Destroy(dataManager.gameObject);
        }
        if (colorManager != null)
        {
            Destroy(colorManager.gameObject);
        }
    }

    private void HandlePuzzle()
    {
        Debug.Log("Enter puzzle");
        CursorController cursorController = FindObjectOfType<CursorController>();
        cursorController.setDefaultCursor();
        if (inventory != null)
        {
            inventory.gameObject.SetActive(false);
        }
        if (dataManager != null)
        {
            dataManager.gameObject.SetActive(true);
        }
        if (colorManager != null)
        {
            colorManager.gameObject.SetActive(false);
        }
    }

    private void HandleMainMenu()
    {
        Debug.Log("Go to Main Menu");
        if (inventory != null)
        {
            inventory.gameObject.SetActive(false);
        }
        if (dataManager != null)
        {
            dataManager.gameObject.SetActive(false);
        }
        if (colorManager != null)
        {
            colorManager.gameObject.SetActive(false);
        }
    }

    private void HandleExploration()
    {
        Debug.Log("Enter Exploration");
        if (inventory != null)
        {
            inventory.gameObject.SetActive(true);
        }
        if (dataManager != null)
        {
            dataManager.gameObject.SetActive(true);
        }
        if (colorManager != null)
        {
            colorManager.gameObject.SetActive(true);
        }
    }

    private void HandleInteraction()
    {
        CursorController cursorController = FindObjectOfType<CursorController>();
        if (cursorController != null)
        {
            cursorController.setDefaultCursor();
        }
    }

    private void HandlePaintBucketMode()
    {
        CursorController cursorController = FindObjectOfType<CursorController>();
        if (cursorController != null)
        {
            cursorController.setPaintBucketCursor();
        }
    }

    public void PlayInterationAfterSceneChange(Interaction interaction)
    {
        postSceneChangeInteraction = interaction;
    }
}

public enum GameState
{
    MainMenu,
    Exploration,
    Interaction,
    PaintBucketMode,
    Puzzle,
    Finish
}

public enum GameScene
{
    StartScreen,
    InstructionsScreen,
    Arcade,
    CyberpunkCity,
    MushroomGarden,
    DDR,
    WateringCansPuzzle,
    PondMaze,
    WireConnectingPuzzle,
    VendingMachineTangram,
    TrashCanSlidingPuzzle,
    Nonogram,
    TraderRos,
    DemoEnd
}