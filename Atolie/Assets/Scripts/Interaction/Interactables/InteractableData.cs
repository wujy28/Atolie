using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Interactable Data", menuName = "Interactable Data")]
public class InteractableData : ScriptableObject
{
    [SerializeField] private string interactableName;

    // Initial state
    [SerializeField] private bool initiallyActive;
    [SerializeField] private bool initiallyColoredIn;
    [SerializeField] private int initialInteractionIndex;

    // Current state
    private bool isCurrentlyActive;
    private bool isColoredIn;
    private int currentInteractionIndex;

    [SerializeField] private List<Interaction> interactionsList;

    public bool GetActiveStatus()
    {
        return isCurrentlyActive;
    }

    public bool GetColoredInStatus()
    {
        return isColoredIn;
    }

    public Interaction GetCurrentInteraction()
    {
        return interactionsList[currentInteractionIndex];
    }

    public void SetActiveStatus(bool isActive)
    {
        isCurrentlyActive = isActive;
    }

    public void SetColoredInStatus(bool coloredIn)
    {
        isColoredIn = coloredIn;
    }

    public void SetCurrentInteractionIndex(int index)
    {
        currentInteractionIndex = index;
    }

    public void ResetData()
    {
        isCurrentlyActive = initiallyActive;
        isColoredIn = initiallyColoredIn;
        currentInteractionIndex = initialInteractionIndex;
    }
}
