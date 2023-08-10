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

    // controls logic of entering an interaction tied to the current target
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

    // controls logic of entering an interaction independent of current target
    private void enterNewInteraction()
    {
        if (!inInteraction)
        {
            GameManager.Instance.UpdateGameState(GameState.Interaction);
            // playerSettings.pausePlayer();
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

    // method called by other classes to immediately exit interaction that is playing currently
    public void forceExitInteraction()
    {
        currentInteraction.Clear();
        exitInteraction();
    }

    // called to play an interaction after completing a puzzle
    public void playPostPuzzleInteraction(Interaction interaction)
    {
        playInteraction(interaction);
        enterNewInteraction();
    }

    // called to play an additional interaction independent of current target
    public void playAdditionalInteraction(Interaction interaction)
    {
        playInteraction(interaction);
        enterNewInteraction();
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

    // not used by current iteration of the game (used by old quest manager)
    public void LoadInteraction(string InteractableName, Interaction interaction)
    {
        Transform interactable = transform.Find("Interactables").Find(InteractableName);
        if (interactable != null)
        {
            interactable.GetComponent<InteractionTrigger>().setCurrentInteraction(interaction);
        }
    }

    // removes a collectible if it is present on screen as a sprite after being obtained
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
