using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WirePuzzleConnectorTile : WirePuzzleTile, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int connectedTerminalID;
    private WirePuzzleTerminalTile connectedTerminal;
    [SerializeField] private Animator animator;

    private ConnectDirection inDirection;
    private ConnectDirection outDirection;

    private void Awake()
    {
        inDirection = ConnectDirection.Unconnected;
        outDirection = ConnectDirection.Unconnected;
        animator = GetComponent<Animator>();
    }

    public override void Disconnect()
    {
        inDirection = ConnectDirection.Unconnected;
        outDirection = ConnectDirection.Unconnected;
        animator.SetInteger("OutDirection", 0);
        animator.SetInteger("InDirection", 0);
        connectedTerminalID = 0;
        connectedTerminal = null;
        this.inTile = null;
        this.outTile = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        int terminalID = grid.activeTerminal;
        if (terminalID != 0)
        {
            if (connectedTerminalID == 0)
            {
                if (grid.IsAdjacentToPreviousTile(this))
                {
                    connectedTerminalID = terminalID;
                    connectedTerminal = grid.startingTerminal;
                    grid.AddTileToActiveConnection(this);
                }
                else
                {
                    grid.ResetActiveConnection();
                }
            } else if (connectedTerminalID == terminalID)
            {
                if (this == grid.SecondLastTileInActiveConnection())
                {
                    grid.RemovePreviousTileFromActiveConnection();
                    grid.RemovePreviousTileFromActiveConnection();
                    grid.AddTileToActiveConnection(this);
                }
                else
                {
                    grid.ResetActiveConnection();
                }
            } else
            {
                grid.ResetActiveConnection();
            }

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public override void CreateConnection(ConnectDirection direction, WirePuzzleTile tile)
    {
        // TODO: implement
        if (inDirection == ConnectDirection.Unconnected)
        {
            inDirection = direction;
            animator.SetInteger("InDirection", (int)direction);
            this.inTile = tile;
        } else if (outDirection == ConnectDirection.Unconnected)
        {
            outDirection = direction;
            animator.SetInteger("OutDirection", (int)direction);
            this.outTile = tile;
        }
    }
}
