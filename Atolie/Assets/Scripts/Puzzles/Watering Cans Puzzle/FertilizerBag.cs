using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FertilizerBag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private CursorController cursorController;

    public void OnBeginDrag(PointerEventData eventData)
    {
        cursorController.setWateringCansPuzzle_fertilizerCursor() ;
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
            if (hit.TryGetComponent<WateringCan>(out WateringCan wateringCan))
            {
                wateringCan.Interact(transform);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }
}
