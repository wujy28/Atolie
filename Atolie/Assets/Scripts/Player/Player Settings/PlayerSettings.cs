using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    private PlayerInteraction playerInteraction;
    private MovementController2D movementController2D;
    private MovementController movementController;

    // Start is called before the first frame update
    void Start()
    {
        playerInteraction = GetComponent<PlayerInteraction>();
        if (TryGetComponent<MovementController2D>(out MovementController2D controller))
        {
            movementController2D = controller;
        }
        else
        {
            movementController = GetComponent<MovementController>();
        }
    }

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
        playerInteraction = GetComponent<PlayerInteraction>();
        if (TryGetComponent<MovementController2D>(out MovementController2D controller))
        {
            movementController2D = controller;
        }
        else
        {
            movementController = GetComponent<MovementController>();
        }
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                break;
            case GameState.Exploration:
                resumePlayer();
                break;
            case GameState.Interaction:
                pausePlayer();
                break;
            case GameState.PaintBucketMode:
                pausePlayer();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void pausePlayer()
    {
        Debug.Log("Player Paused");
        disablePlayerInteraction();
        disablePlayerMovement();
    }

    public void resumePlayer()
    {
        Debug.Log("Player Resumed");
        enablePlayerInteraction();
        enablePlayerMovement();
    }

    public bool playerMovementEnabled()
    {
        if (movementController != null)
        {
            return movementController.enabled;
        }
        else
        {
            return movementController2D.enabled;
        }
    }

    public bool playerInteractionEnabled()
    {
        return playerInteraction.enabled;
    }

    public void disablePlayerMovement()
    {
        if (movementController != null)
        {
            movementController.enabled = false;
        }
        if (movementController2D != null)
        {
            movementController2D.enabled = false;
        }
    }

    public void enablePlayerMovement()
    {
        if (movementController != null)
        {
            movementController.enabled = true;
        }
        if (movementController2D != null)
        {
            movementController2D.enabled = true;
        }
    }

    public void disablePlayerInteraction()
    {
        playerInteraction.enabled = false;
    }

    public void enablePlayerInteraction()
    {
        playerInteraction.enabled = true;
    }
}