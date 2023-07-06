using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WirePuzzleTerminalTile : WirePuzzleTile, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public int terminalID;
    [SerializeField] private Animator animator;
    private WirePuzzleTerminalTile startingTerminal;

    private ConnectDirection connectedDirection;

    private void Awake()
    {
        connectedDirection = ConnectDirection.Unconnected;
        animator = GetComponent<Animator>();
        startingTerminal = null;
        transform.Find("Canvas").GetComponentInChildren<Text>().text = terminalID.ToString();
    }

    public override void Disconnect()
    {
        connectedDirection = ConnectDirection.Unconnected;
        animator.SetInteger("ConnectDirection", 0);
        startingTerminal = null;
        this.inTile = null;
        this.outTile = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (startingTerminal != null)
        {
            grid.ResetConnection(startingTerminal);
        }
        startingTerminal = this;
        grid.StartConnection(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        grid.ResetActiveConnection();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("On Drop");
        startingTerminal = grid.startingTerminal;
        grid.TryCompleteConnection(this);
    }

    public override void CreateConnection(ConnectDirection direction, WirePuzzleTile tile)
    {
        connectedDirection = direction;
        animator.SetInteger("ConnectDirection", (int)direction);
        if (grid.startingTerminal == this)
        {
            this.outTile = tile;
        } else
        {
            this.inTile = tile;
        }
    }
}
