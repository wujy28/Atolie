using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class InteractionTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Interaction currentInteraction;

    CursorController cursorController;

    public void interact()
    {
        if (currentInteraction != null)
        {
            InteractionManager.Instance.playInteraction(currentInteraction);
        } else
        {
            Debug.Log("No Interaction Assigned");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        setTarget();
    }

    public abstract void OnPointerEnter(PointerEventData eventData);

    public void OnPointerExit(PointerEventData eventData)
    {
        cursorController.setDefaultCursor();
    }

    public void setTarget()
    {
        InteractionManager.Instance.setCurrentTarget(transform);
    }

    public CursorController getCursorController()
    {
        return cursorController;
    }

    public void setCurrentInteraction(Interaction newInteraction)
    {
        currentInteraction = newInteraction;
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
