using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WaterSupply : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IInteractable
{
    [SerializeField] private CursorController cursorController;

    public void Interact(Transform initiator)
    {
        if (initiator.TryGetComponent<DraggableContainer>(out DraggableContainer fromContainer))
        {
            fromContainer.Empty();
        }
        else if (initiator.TryGetComponent<WateringCan>(out WateringCan wateringCan))
        {
            wateringCan.Empty();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        cursorController.setWateringCansPuzzle_waterCursor();
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        cursorController.setDefaultCursor();
        Collider2D hit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (hit != null)
        {
            if (hit.TryGetComponent<DraggableContainer>(out DraggableContainer container))
            {
                Debug.Log("Fill: " + hit.name);
                container.Fill();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
