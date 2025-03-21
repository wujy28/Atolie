using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class InteractionTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Interaction currentInteraction;

    private CursorController cursorController;

    [SerializeField] public static bool interactionsAllowed = true;

    public static event Action<Transform> OnInteractedWith;

    public void interact()
    {
        if (currentInteraction != null)
        {
            InteractionManager.Instance.playInteraction(currentInteraction);
            OnInteractedWith?.Invoke(transform);
        }
        else
        {
            Debug.Log("No Interaction Assigned");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (interactionsAllowed)
        {
            setTarget();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (interactionsAllowed)
        {
            changeCursor();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (interactionsAllowed)
        {
            cursorController.setDefaultCursor();
            // Debug.Log("Pointer Exit: " + transform.name);
            // Debug.Log("InteractionsAllowed: " + interactionsAllowed.ToString());
        }
    }


    // Alternatively, make this class not abstract, remove all subclasses and
    // use a switch statement in OnPointerEnter instead (if no
    // new methods are added to differentiate)
    public abstract void changeCursor();

    public void setTarget()
    {
        if (interactionsAllowed)
        {
            InteractionManager.Instance.setCurrentTarget(transform);
            if (TryGetComponent(out Interactable interactable))
            {
                interactable.UpdateInteraction();
            }
        }
    }

    public CursorController getCursorController()
    {
        return cursorController;
    }

    public void setCurrentInteraction(Interaction newInteraction)
    {
        currentInteraction = newInteraction;
    }

    public Interaction GetCurrentInteraction()
    {
        return currentInteraction;
    }

    private void Start()
    {
        // interactionsAllowed = true;
    }

    // Start is called before the first frame update
    void Awake()
    {
        cursorController = GameObject.Find("CursorController").GetComponent<CursorController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
