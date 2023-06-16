using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WateringCan : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IInteractable
{
    private static int targetCapacity;
    private int currentCapacity;
    private bool fertilizerAdded;

    [SerializeField] private Image WaterFill;
    [SerializeField] private Image SerumFill;

    private Vector2 spawnPoint;
    private RectTransform can;

    private void Awake()
    {
        targetCapacity = 0;
        currentCapacity = 0;
        fertilizerAdded = false;
        can = GetComponent<RectTransform>();
        spawnPoint = can.anchoredPosition;
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

    public void Interact(Transform initiator)
    {
        if (initiator.TryGetComponent<DraggableContainer>(out DraggableContainer fromContainer))
        {
            AddVolume(fromContainer.TransferAll());
        } else if (initiator.TryGetComponent<FertilizerBag>(out FertilizerBag fertilizerBag))
        {
            AddFertilizer();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        can.anchoredPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
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
        can.anchoredPosition = spawnPoint;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void Empty()
    {
        currentCapacity = 0;
        fertilizerAdded = false;
        updateDisplay();
    }

    private void AddVolume(int volume)
    {
        currentCapacity = volume;
        fertilizerAdded = false;
        updateDisplay();
    }

    private void AddFertilizer()
    {
        fertilizerAdded = true;
        updateDisplay();
        if (currentCapacity == targetCapacity)
        {
            WateringCansPuzzleTracker.Instance.CompleteCurrentStage();
        }
    }

    private void updateDisplay()
    {
        if (currentCapacity == 0)
        {
            WaterFill.enabled = false;
            SerumFill.enabled = false;
        } else if (!fertilizerAdded)
        {
            WaterFill.enabled = true;
            SerumFill.enabled = false;
        } else
        {
            WaterFill.enabled = false;
            SerumFill.enabled = true;
        }
    }

    public static void setTargetCapacity(int goal)
    {
        targetCapacity = goal;
    }
}
