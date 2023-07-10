using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private List<InteractableData> allInteractableData;
    [SerializeField] private List<InteractableListener> allInteractableListener;

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
            Debug.Log("On Destroy");
        }
    }

    private void OnDisable()
    {
        if (Instance == this)
        {
            UnsubscribeAllListeners();
            ResetAllInteractableData();
            Debug.Log("On Disable");
        }
    }

    private void SubscribeAllListeners()
    {
        foreach (InteractableListener listener in allInteractableListener)
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
    }

    private void ResetAllInteractableData()
    {
        foreach (InteractableData interactable in allInteractableData)
        {
            interactable.ResetData();
        }
    }
}
