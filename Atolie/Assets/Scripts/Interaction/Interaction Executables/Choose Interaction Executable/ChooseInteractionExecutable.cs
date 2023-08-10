using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Choose Interaction Executable", menuName = "Interaction Executable/Choose Interaction Executable")]
public class ChooseInteractionExecutable : InteractionExecutable
{
    [SerializeField] private List<InteractionOption> allInteractions;

    public override void execute()
    {
        ChooseInteractionPopup popup = ChooseInteractionPopup.Instance;
        List<InteractionOption> activeOptions = FindActiveOptions();
        List<string> allActiveDescriptions = CollectAllDescriptions(activeOptions);
        List<Interaction> allActiveInteractions = CollectAllInteractions(activeOptions);
        popup.ShowPopup();
        popup.updatePopup(allActiveDescriptions, allActiveInteractions);
    }

    private List<InteractionOption> FindActiveOptions()
    {
        List<InteractionOption> activeOptions = new List<InteractionOption>();
        foreach (InteractionOption option in allInteractions)
        {
            if (option.isActive)
            {
                activeOptions.Add(option);
            }
        }
        return activeOptions;
    }

    private List<string> CollectAllDescriptions(List<InteractionOption> options)
    {
        List<string> allDescriptions = new List<string>();
        foreach (InteractionOption option in options)
        {
            allDescriptions.Add(option.description);
        }
        return allDescriptions;
    }

    private List<Interaction> CollectAllInteractions(List<InteractionOption> options)
    {
        List<Interaction> allInteractions = new List<Interaction>();
        foreach (InteractionOption option in options)
        {
            allInteractions.Add(option.GetCurrentInteraction());
        }
        return allInteractions;
    }

    public void GoToStep(int optionIndex, int step)
    {
        InteractionOption option = allInteractions[optionIndex];
        option.currentInteractionIndex = step;
    }

    public void AddToOptions(int optionIndex)
    {
        InteractionOption option = allInteractions[optionIndex];
        option.isActive = true;
    }

    public void RemoveFromOptions(int optionIndex)
    {
        InteractionOption option = allInteractions[optionIndex];
        option.isActive = false;
    }

    public void ResetAllOptions()
    {
        foreach (InteractionOption option in allInteractions)
        {
            option.Reset();
        }
    }

    // nested class encapsulating each option of the CI dropdown list
    [System.Serializable]
    private class InteractionOption
    {
        public bool isActive;
        public string description;
        public List<Interaction> allInteractionsForOption;
        public int currentInteractionIndex;

        public bool isInitiallyActive;
        public int initialInteractionIndex;

        public Interaction GetCurrentInteraction()
        {
            return allInteractionsForOption[currentInteractionIndex];
        }

        public void Reset()
        {
            isActive = isInitiallyActive;
            currentInteractionIndex = initialInteractionIndex;
        }
    }
}
