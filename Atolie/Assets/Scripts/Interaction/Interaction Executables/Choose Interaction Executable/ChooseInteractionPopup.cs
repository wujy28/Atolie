using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseInteractionPopup : MonoBehaviour
{
    public static ChooseInteractionPopup Instance;

    [SerializeField] private int selectedOption;

    private List<string> optionDescriptions;
    private List<Interaction> interactionOptions;


    [SerializeField] private GameObject popupCanvas;
    [SerializeField] private Dropdown dropdown;

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
    }

    public void updatePopup(List<string> descriptions, List<Interaction> interactions)
    {
        optionDescriptions = descriptions;
        interactionOptions = interactions;
        LoadOptions();
    }

    private void LoadOptions()
    {
        foreach (string description in optionDescriptions)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = description });
        }
        dropdown.options.Add(new Dropdown.OptionData() { text = "Nothing" });
    }

    public void ShowPopup()
    {
        popupCanvas.SetActive(true);
    }

    public void HidePopup()
    {
        popupCanvas.SetActive(false);
    }

    public void HandleInput(int input)
    {
        selectedOption = input;
    }

    public void ConfirmChoice()
    {
        if (selectedOption >= interactionOptions.Count)
        {
            HidePopup();
            InteractionExecutable.currentExecutableCompleted();
            return;
        }
        Interaction interaction = interactionOptions[selectedOption];
        HidePopup();
        InteractionManager.Instance.forceExitInteraction();
        InteractionManager.Instance.playInteraction(interaction);
    }
}