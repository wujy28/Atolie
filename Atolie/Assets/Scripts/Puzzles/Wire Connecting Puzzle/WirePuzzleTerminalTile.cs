using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WirePuzzleTerminalTile : WirePuzzleTile, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public int terminalID;
    [SerializeField] private Animator animator;

    private ConnectDirection connectedDirection;

    private void Awake()
    {
        connectedDirection = ConnectDirection.Unconnected;
        animator = GetComponent<Animator>();
    }

    public override void Disconnect()
    {
        connectedDirection = ConnectDirection.Unconnected;
        animator.SetInteger("ConnectDirection", 0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        grid.StartConnection(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        grid.TerminateConnection();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("On Drop");
    }

    public override void CreateConnection(ConnectDirection direction)
    {
        connectedDirection = direction;
        animator.SetInteger("ConnectDirection", (int)direction);
    }
}
