using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WirePuzzleConnectorTile : WirePuzzleTile, IPointerEnterHandler, IPointerExitHandler
{
    private int connectedTerminal;
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
        connectedTerminal = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        int terminal = grid.activeTerminal;
        if (terminal != 0)
        {
            connectedTerminal = terminal;

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public override void CreateConnection(ConnectDirection direction)
    {
        // TODO: implement
        if (inDirection == ConnectDirection.Unconnected)
        {
        }
    }
}
