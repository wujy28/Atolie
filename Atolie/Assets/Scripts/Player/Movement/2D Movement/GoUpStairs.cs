using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoUpStairs : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private MovementController2D movementController2D;
    [SerializeField] private CursorController cursorController;
    [SerializeField] 

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    private void OnDisable()
    {
        if (!PaintBucketMode.paintBucketModeOn)
        {
            cursorController.setDefaultCursor();
        }
    }

    private void GameManager_OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Exploration:
                break;
            case GameState.Interaction:
                cursorController.setDefaultCursor();
                break;
            case GameState.PaintBucketMode:
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        movementController2D.goToUpperLevel();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (InteractionTrigger.interactionsAllowed)
        {
            cursorController.setStairsUpCursor();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!PaintBucketMode.paintBucketModeOn)
        {
            cursorController.setDefaultCursor();
        }
    }
}
