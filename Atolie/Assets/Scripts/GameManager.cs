using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }

    private void OnDestroy()
    {
        if (this == Instance)
        {
            interactableData.ResetData();
        }
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
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleMainMenu()
    {
        throw new NotImplementedException();
    }

    private void HandleExploration()
    {

    }

    private void HandleInteraction()
    {

    }

    private void HandlePaintBucketMode()
    {

    }
}

public enum GameState
{
    MainMenu,
    Exploration,
    Interaction,
    PaintBucketMode
}