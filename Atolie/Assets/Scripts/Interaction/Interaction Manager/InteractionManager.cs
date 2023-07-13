using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractionManager : MonoBehaviour
{
    enum Scene
    {
        Arcade,
        CyberpunkCity,
        MushroomGarden
    }

    private Transform player;
    private PlayerInteraction playerInteraction;
    // private PlayerSettings playerSettings;

    [SerializeField] private GameObject interactables;
    [SerializeField] private bool inInteraction;
    [SerializeField] private Transform currentTarget;
    [SerializeField] private Queue<InteractionExecutable> currentInteraction;

    public static InteractionManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        Debug.Log("Awake");
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
        // QuestManager.Instance.LoadActiveQuests();
    }

    public void forceExitInteraction()
    {
        currentInteraction.Clear();
        exitInteraction();
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

    public void LoadInteraction(string InteractableName, Interaction interaction)
    {
        Transform interactable = transform.Find("Interactables").Find(InteractableName);
        if (interactable != null)
        {
            interactable.GetComponent<InteractionTrigger>().setCurrentInteraction(interaction);
        }
    }

    public void RemoveFromScene(Item item)
    {
        string name = item.interactableName;
        Debug.Log(name);
        if (name != null && name != "")
        {
            Transform interactable = interactables.transform.Find(name);
            if (interactable != null)
            {
                Debug.Log(interactable.name);
                interactable.gameObject.SetActive(false);
            }
        }
    }
}
