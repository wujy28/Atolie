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
    private PlayerSettings playerSettings;

    [SerializeField] private bool inInteraction;
    [SerializeField] private Transform currentTarget;
    [SerializeField] private Queue<InteractionExecutable> currentInteraction;

    public static InteractionManager Instance { get; private set; }

    public static Action InteractWith;

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
        playerSettings = player.GetComponent<PlayerSettings>();
        currentTarget = null;
        inInteraction = false;
        currentInteraction = new Queue<InteractionExecutable>();
    }

    private void Update()
    {
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
            playerSettings.pausePlayer();
            InteractionTrigger interactionTrigger = currentTarget.GetComponent<InteractionTrigger>();
            interactionTrigger.interact();
            inInteraction = true;
        }
    }

    public void exitInteraction()
    {
        if(inInteraction)
        {
            playerSettings.resumePlayer();
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
