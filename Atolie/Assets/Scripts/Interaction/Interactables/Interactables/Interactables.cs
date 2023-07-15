using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactables : MonoBehaviour
{
    public InteractablesData interactableData;

    [SerializeField] private List<Transform> interactables;
    public CurrentScene currentScene;

    private void Awake()
    {
        interactables = new List<Transform>();
        foreach (Transform child in transform)
        {
            interactables.Add(child);
        }
        RegisterAllInteractables();
        SyncStatusOfAllInteractables();
    }

    private void OnDestroy()
    {
        UpdateStatusesOfAllInteractables();
    }

    private void SyncStatusOfAllInteractables()
    {
        foreach (Transform interactable in interactables)
        {
            string name = interactable.name;
            bool active = interactableData.GetActiveStatus(name);
            interactable.gameObject.SetActive(active);
            if (active)
            {
                if (interactable.TryGetComponent<Coloring>(out Coloring coloring))
                {
                    bool coloredIn = interactableData.GetColoredStatus(name);
                    if (coloredIn)
                    {
                        coloring.colorIn(coloring.correctColor);
                    }
                }
                Interaction currentInteraction = interactableData.GetInteraction(name);
                InteractionTrigger interactionTrigger = interactable.GetComponent<InteractionTrigger>();
                interactionTrigger.setCurrentInteraction(currentInteraction);
            }
        }
    }

    private void UpdateStatusesOfAllInteractables()
    {
        foreach (Transform interactable in interactables)
        {
            string name = interactable.name;
            Scene scene = SceneManager.GetActiveScene();
            bool isActive = interactable.gameObject.activeSelf;
            bool coloredIn = false;
            Interaction interaction = null;
            if (isActive)
            {
                if (interactable.TryGetComponent<Coloring>(out Coloring coloring))
                {
                    coloredIn = coloring.coloredIn;
                }
                interaction = interactable.GetComponent<InteractionTrigger>().GetCurrentInteraction();
            }
            interactableData.UpdateStatus(name, scene, isActive, coloredIn, interaction);
        }
    }

    public void RegisterAllInteractables()
    {
        foreach (Transform interactable in interactables)
        {
            string name = interactable.name;
            Scene scene = SceneManager.GetActiveScene();
            bool isActive = interactable.gameObject.activeSelf;
            bool coloredIn = false;
            Interaction interaction = null;
            if (isActive)
            {
                if (interactable.TryGetComponent<Coloring>(out Coloring coloring))
                {
                    coloredIn = coloring.coloredIn;
                }
                interaction = interactable.GetComponent<InteractionTrigger>().GetCurrentInteraction();
            }
            interactableData.AddInteractable(name, scene, isActive, coloredIn, interaction);
        }
    }

    public enum CurrentScene
    {
        Arcade,
        CyberpunkCity,
        MushroomGarden
    }
}
