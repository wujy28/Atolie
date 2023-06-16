using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableContainer : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IInteractable
{
    [SerializeField] private int maxCapacity;
    [SerializeField] private int currentCapacity;

    [SerializeField] Text text;
    [SerializeField] Slider slider;

    private Vector2 spawnPoint;
    private RectTransform container;

    private void Awake()
    {
        container = GetComponent<RectTransform>();
        spawnPoint = container.anchoredPosition;
        WateringCansPuzzleTracker.OnStageChanged += WateringCansPuzzleTracker_OnStageChanged;
    }

    private void OnDestroy()
    {
        WateringCansPuzzleTracker.OnStageChanged -= WateringCansPuzzleTracker_OnStageChanged;
    }

    private void WateringCansPuzzleTracker_OnStageChanged(WateringCansPuzzleTracker.Stage stage)
    {
        Empty();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PointerDown: " + transform.name);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag: " + transform.name);
        MoveSpriteToFront();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag: " + transform.name);
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.enabled = false;
        Collider2D hit = Physics2D.OverlapBox(collider.bounds.center, collider.bounds.size, 0f); 
        if (hit != null)
        {
            Debug.Log("Hit: " + hit.name);
            if (hit.TryGetComponent<IInteractable>(out IInteractable interaction))
            {
                interaction.Interact(transform);
            }
        }
        collider.enabled = true;
        SetDefaultSortingOrder();
        container.anchoredPosition = spawnPoint;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        Gizmos.DrawCube(collider.bounds.center, collider.bounds.size);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag: " + transform.name);
        container.anchoredPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void MoveSpriteToFront()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 15;
        GetComponentInChildren<Canvas>().sortingOrder = 14;
    }

    private void SetDefaultSortingOrder()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 10;
        GetComponentInChildren<Canvas>().sortingOrder = 5;
    }

    public static void TransferBetweenContainers(DraggableContainer fromContainer, DraggableContainer toContainer)
    {
        int availableSpaceInToContainer = toContainer.maxCapacity - toContainer.currentCapacity;
        toContainer.AddVolume(fromContainer.currentCapacity);
        fromContainer.RemoveVolume(availableSpaceInToContainer);
    }

    public void Interact(Transform initiator)
    {
        if (initiator.TryGetComponent<DraggableContainer>(out DraggableContainer fromContainer))
        {
            DraggableContainer.TransferBetweenContainers(fromContainer, this);
        }
    }

    private void updateDisplay()
    {
        slider.value = currentCapacity;
        text.text = currentCapacity.ToString();
    }

    public void Empty()
    {
        currentCapacity = 0;
        updateDisplay();
    }

    public void Fill()
    {
        currentCapacity = maxCapacity;
        updateDisplay();
    }

    private void AddVolume(int volume)
    {
        currentCapacity += volume;
        if (currentCapacity > maxCapacity)
        {
            currentCapacity = maxCapacity;
        }
        updateDisplay();
    } 

    private void RemoveVolume(int volume)
    {
        currentCapacity -= volume;
        if (currentCapacity < 0)
        {
            currentCapacity = 0;
        }
        updateDisplay();
    }

    public int TransferAll()
    {
        int outCapacity = currentCapacity;
        Empty();
        return outCapacity;
    }
}
