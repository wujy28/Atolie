using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private InteractableData interactableData;
    [SerializeField] private Coloring coloring;
    [SerializeField] private InteractionTrigger interactionTrigger;

    private void Awake()
    {
        LoadInteractable();
    }

    private void OnDestroy()
    {
        SaveInteractableData();
    }

    public void LoadInteractable()
    {
        bool active = interactableData.GetActiveStatus();
        gameObject.SetActive(active);
        if (active)
        {
            if (TryGetComponent(out Coloring coloring))
            {
                bool coloredIn = interactableData.GetColoredInStatus();
                if (coloredIn)
                {
                    coloring.colorInQuietly();
                }
            }
            Interaction currentInteraction = interactableData.GetCurrentInteraction();
            interactionTrigger.setCurrentInteraction(currentInteraction);
        }
        Debug.Log(gameObject.name + " loaded.");
    }

    public void UpdateInteraction()
    {
        Interaction currentInteraction = interactableData.GetCurrentInteraction();
        interactionTrigger.setCurrentInteraction(currentInteraction);
    }

    public void SaveInteractableData()
    {
        bool active = gameObject.activeSelf;
        interactableData.SetActiveStatus(active);
        if (active)
        {
            if (TryGetComponent(out Coloring coloring))
            {
                bool coloredIn = coloring.coloredIn;
                interactableData.SetColoredInStatus(coloredIn);
            }
        }
    }
}
