using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Future Game Manager???
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    // Data
    [SerializeField] private InteractableData interactableData;

    // References
    private InventoryManager inventory;
    private QuestManager questSystem;

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
        }
        inventory = FindObjectOfType<InventoryManager>();
        questSystem = FindObjectOfType<QuestManager>();
    }

    private void OnDestroy()
    {
        if (this == Instance)
        {
            interactableData.ResetData();
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
                interactionManager.playInteraction(postSceneChangeInteraction);
            }
        }
        postSceneChangeInteraction = null;
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
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
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
        if (questSystem != null)
        {
            questSystem.gameObject.SetActive(true);
        }
    }

    private void HandleMainMenu()
    {
        
    }

    private void HandleExploration()
    {
        Debug.Log("Enter Exploration");
        if (inventory != null)
        {
            inventory.gameObject.SetActive(true);
        }
        if (questSystem != null)
        {
            questSystem.gameObject.SetActive(true);
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
    Puzzle
}