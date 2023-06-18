using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractionManager : MonoBehaviour
{
    enum Type
    {
        InteractableObject,
        NPC,
        Collectible
    }
    public static InteractionManager instance;

    private Transform player;
    private PlayerInteraction playerInteraction;
    // private PlayerSettings playerSettings;

    [SerializeField] private bool inInteraction;
    [SerializeField] private Transform currentTarget;
    [SerializeField] private Queue<InteractionExecutable> currentInteraction;

    public static InteractionManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        } else
        {
            Instance = this;
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerInteraction = player.GetComponent<PlayerInteraction>();
        // playerSettings = player.GetComponent<PlayerSettings>();
        currentTarget = null;
        inInteraction = false;
        currentInteraction = new Queue<InteractionExecutable>();
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;

    }

    private void Update()
    {
    }

    public void GameManager_OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                break;
            case GameState.Exploration:
                InteractionTrigger.interactionsAllowed = true;
                break;
            case GameState.Interaction:
                InteractionTrigger.interactionsAllowed = false;
                break;
            case GameState.PaintBucketMode:
                InteractionTrigger.interactionsAllowed = false;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    public void setCurrentTarget(Transform newTarget)
    {
        currentTarget = newTarget;
        playerInteraction.setCurrentTarget(newTarget);
    }

    public void removeTarget()
    {
        currentTarget = null;
        playerInteraction.setCurrentTarget(null);
    }

    public void enterInteraction()
    {
        if (!inInteraction)
        {
            GameManager.Instance.UpdateGameState(GameState.Interaction);
            // playerSettings.pausePlayer();
            InteractionTrigger interactionTrigger = currentTarget.GetComponent<InteractionTrigger>();
            interactionTrigger.interact();
            inInteraction = true;
        }
    }

    public void exitInteraction()
    {
        Debug.Log("Interaction Exited");
        if(inInteraction)
        {
            // playerSettings.resumePlayer();
            GameManager.Instance.UpdateGameState(GameState.Exploration);
            removeTarget();
            inInteraction = false;
        }
        
    }

    public void playInteraction(Interaction interaction)
    {
        currentInteraction.Clear();
        foreach (InteractionExecutable executable in interaction.executables)
        {
            currentInteraction.Enqueue(executable);
        }

        executeNextInteractionExecutable();
    }

    public void executeNextInteractionExecutable()
    {
        if (currentInteraction.Count == 0)
        {
            exitInteraction();
            return;
        }
        
        InteractionExecutable executable = currentInteraction.Dequeue();
        executable.execute();
    }
}
