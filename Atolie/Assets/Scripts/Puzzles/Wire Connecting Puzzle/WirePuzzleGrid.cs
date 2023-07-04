using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirePuzzleGrid : MonoBehaviour
{
    [SerializeField] private int gridSize;
    [SerializeField] private int numOfTerminals;
    private int numOfTiles;
    private int numOfUsedTiles;
    private int numOfConnectedTerminals;

    public int activeTerminal;
    private WirePuzzleTerminalTile startingTerminal;
    private Stack<WirePuzzleTile> activeConnection;

    private void Awake()
    {
        numOfTiles = gridSize * gridSize;
        numOfUsedTiles = 0;
        numOfConnectedTerminals = 0;
        activeTerminal = 0;
        activeConnection = new Stack<WirePuzzleTile>();
        SetUpTiles();
    }

    private void SetUpTiles()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out WirePuzzleTile tile))
            {
                tile.AssignGrid(this);
            }
        }
    }

    public void StartConnection(WirePuzzleTerminalTile terminal)
    {
        activeConnection.Push(terminal);
        activeTerminal = terminal.terminalID;
        startingTerminal = terminal;
    }

    public void TerminateConnection()
    {
        activeConnection.Clear();
        activeTerminal = 0;
        startingTerminal = null;
    }

    public bool TryCompleteConnection(WirePuzzleTerminalTile terminal)
    {
        if (terminal != startingTerminal)
        {
            WirePuzzleTile previousTile = activeConnection.Peek();
            ConnectTiles(previousTile, terminal);
            //TODO: implement
            return true;
        }
        else
        {
            TerminateConnection();
            return false;
        }
    }

    public void ConnectTiles(WirePuzzleTile fromTile, WirePuzzleTile toTile)
    {
        int relativePosition = fromTile.CompareTo(toTile);
        switch (relativePosition)
        {
            case 0:
                // do nothing if same tile
                break;
            case 1:
                // toTile is above fromTile
                fromTile.CreateConnection(ConnectDirection.Up);
                toTile.CreateConnection(ConnectDirection.Down);
                break;
            case 2:
                // toTile is to the right of fromTile
                fromTile.CreateConnection(ConnectDirection.Right);
                toTile.CreateConnection(ConnectDirection.Left);
                break;
            case 3:
                // toTile is below fromTile
                fromTile.CreateConnection(ConnectDirection.Down);
                toTile.CreateConnection(ConnectDirection.Up);
                break;
            case 4:
                // toTile is to the left of fromTile
                fromTile.CreateConnection(ConnectDirection.Left);
                toTile.CreateConnection(ConnectDirection.Right);
                break;
            case -1:
                // terminate connection if diagonal
                TerminateConnection();
                break;
        }
    }
}
