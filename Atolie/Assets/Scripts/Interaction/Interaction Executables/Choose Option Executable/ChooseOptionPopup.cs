using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseOptionPopup : MonoBehaviour
{
    public static ChooseOptionPopup Instance;

    private string description;
    private string enterText;
    [SerializeField] private Runnable enter;
    private string exitText;
    [SerializeField] private Runnable exit;

    [SerializeField] private GameObject popupCanvas;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text enterButtonText;
    [SerializeField] private Text exitButtonText;

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

    public void updatePopup(string descr, string enT, string en, string exT, string ex)
    {
        description = descr;
        enterText = enT;
        enter = Runnables.GetRunnable(en);
        exitText = exT;
        exit = Runnables.GetRunnable(ex);
        updateText();
    }

    private void updateText()
    {
        descriptionText.text = description;
        enterButtonText.text = enterText;
        exitButtonText.text = exitText;
    }

    public void ShowPopup()
    {
        popupCanvas.SetActive(true);
    }

    public void HidePopup()
    {
        popupCanvas.SetActive(false);
    }

    // executes runnable tied to the left choose option popup button
    public void runEnter()
    {
        HidePopup();
        Debug.Log("Runnable: " + enter.ToString());
        enter.Run();
        InteractionExecutable.currentExecutableCompleted();
    }

    // executes runnable tied to the right choose option popup button
    public void runExit()
    {
        HidePopup();
        Debug.Log("Runnable: " + enter.ToString());
        exit.Run();
        InteractionExecutable.currentExecutableCompleted();
    }
}