using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Interactables Data", menuName = "Interactables Data")]
public class InteractableData : ScriptableObject
{
    [SerializedDictionary("Interactable Name", "Interactable Status")]
    public SerializedDictionary<string, InteractableStatus> interactables;

    [System.Serializable]
    public class InteractableStatus
    {
        public string interactableName;
        public Scene scene;
        public bool isActive;
        public bool coloredIn;
        public Interaction interaction;

        public InteractableStatus(string name, Scene scene, bool active, bool colored, Interaction interaction)
        {
            this.interactableName = name;
            this.scene = scene;
            this.isActive = active;
            this.coloredIn = colored;
            this.interaction = interaction;
        }
    }

    public void ResetData()
    {
        interactables = new SerializedDictionary<string, InteractableStatus>();
    }


    public void UpdateStatus(string interactableName, Scene scene, bool isActive, bool coloredIn, Interaction interaction)
    {
        InteractableStatus status = new InteractableStatus(interactableName, scene, isActive, coloredIn, interaction);
        if (!interactables.ContainsKey(interactableName))
        {
            Debug.Log("Cannot update status: " + interactableName + " does not exist.");
            return;
        }
        else
        {
            interactables.Remove(interactableName);
            interactables.Add(interactableName, status);
        }
    }

    public void AddInteractable(string interactableName, Scene scene, bool isActive, bool coloredIn, Interaction interaction)
    {
        InteractableStatus status = new InteractableStatus(interactableName, scene, isActive, coloredIn, interaction);
        interactables.TryAdd(interactableName, status);
    }

    public void UpdateActiveStatus(string interactableName, bool isActive)
    {
        bool exists = interactables.TryGetValue(interactableName, out InteractableStatus status);
        if (exists)
        {
            status.isActive = isActive;
        }
        else
        {
            Debug.Log("Cannot update isActive status: " + interactableName + " not found.");
        }
    }

    public void UpdateColoredInStatus(string interactableName, bool coloredIn)
    {
        bool exists = interactables.TryGetValue(interactableName, out InteractableStatus status);
        if (exists)
        {
            status.coloredIn = coloredIn;
        }
        else
        {
            Debug.Log("Cannot update coloredIn status: " + interactableName + " not found.");
        }
    }

    public void UpdateInteraction(string interactableName, Interaction interaction)
    {
        bool exists = interactables.TryGetValue(interactableName, out InteractableStatus status);
        if (exists)
        {
            status.interaction = interaction;
        }
        else
        {
            Debug.Log("Cannot update interaction: " + interactableName + " not found.");
        }
    }

    public bool GetActiveStatus(string interactableName)
    {
        bool exists = interactables.TryGetValue(interactableName, out InteractableStatus status);
        if (exists)
        {
            return status.isActive;
        }
        else
        {
            Debug.Log("Cannot get isActive status: " + interactableName + " not found.");
            return false;
        }
    }

    public bool GetColoredStatus(string interactableName)
    {
        bool exists = interactables.TryGetValue(interactableName, out InteractableStatus status);
        if (exists)
        {
            return status.coloredIn;
        }
        else
        {
            Debug.Log("Cannot get coloredIn status: " + interactableName + " not found.");
            return false;
        }
    }

    public Interaction GetInteraction(string interactableName)
    {
        bool exists = interactables.TryGetValue(interactableName, out InteractableStatus status);
        if (exists)
        {
            return status.interaction;
        }
        else
        {
            Debug.Log("Cannot get interaction: " + interactableName + " not found.");
            return null;
        }
    }
}
