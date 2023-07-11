using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private List<InteractableData> allInteractableData;
    [SerializeField] private List<InteractableListener> allInteractableListener;
    [SerializeField] private List<ChooseInteractionExecutable> allChooseInteractionExecutables;
    [SerializeField] private List<ChooseInteractionListener> allChooseInteractionListeners;

    public static DataManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
            SubscribeAllListeners();
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            UnsubscribeAllListeners();
            ResetAllInteractableData();
            ResetAllChooseInteractionExecutables();
            Debug.Log("On Destroy");
        }
    }

    private void OnDisable()
    {
        if (Instance == this)
        {
            UnsubscribeAllListeners();
            ResetAllInteractableData();
            ResetAllChooseInteractionExecutables();
            Debug.Log("On Disable");
        }
    }

    private void SubscribeAllListeners()
    {
        foreach (InteractableListener listener in allInteractableListener)
        {
            listener.SubscribeToAllEvents();
        }

        foreach (ChooseInteractionListener listener in allChooseInteractionListeners)
        {
            listener.SubscribeToAllEvents();
        }
    }

    private void UnsubscribeAllListeners()
    {
        foreach (InteractableListener listener in allInteractableListener)
        {
            listener.UnsubscribeFromAllEvents();
        }

        foreach (ChooseInteractionListener listener in allChooseInteractionListeners)
        {
            listener.UnsubscribeFromAllEvents();
        }
    }

    private void ResetAllInteractableData()
    {
        foreach (InteractableData interactable in allInteractableData)
        {
            interactable.ResetData();
        }
    }

    private void ResetAllChooseInteractionExecutables()
    {
        foreach (ChooseInteractionExecutable executable in  allChooseInteractionExecutables)
        {
            executable.ResetAllOptions();
        }
    }
}
