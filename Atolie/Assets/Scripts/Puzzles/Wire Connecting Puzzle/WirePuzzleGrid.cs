using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirePuzzleGrid : MonoBehaviour
{
    [SerializeField] private WirePuzzleTracker.Stage level;
    [SerializeField] private WirePuzzleTracker puzzleManager;

    [SerializeField] private int gridSize;
    [SerializeField] private int numOfTerminals;
    private int numOfTiles;
    private int numOfUsedTiles;
    private int numOfConnectedTerminals;
    private int[] tileCountInConnections;

    public int activeTerminal;
    public WirePuzzleTerminalTile startingTerminal;
    private Stack<WirePuzzleTile> activeConnection;

    private void Awake()
    {
        numOfTiles = gridSize * gridSize;
        ResetGrid();
        SetUpTiles();
    }

    private void ResetGrid()
    {
        numOfUsedTiles = 0;
        numOfConnectedTerminals = 0;
        tileCountInConnections = new int[numOfTerminals + 1];
        activeTerminal = 0;
        startingTerminal = null;
        activeConnection = new Stack<WirePuzzleTile>();
    }

    private void OnEnable()
    {
        Debug.Log(Enum.GetName(typeof(WirePuzzleTracker.Stage), level) + " Reset");
        ResetGrid();
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

    private void CheckForCompletion()
    {
        Debug.Log("NumberOfUsedTiles: " + numOfUsedTiles.ToString());
        Debug.Log("NumberOfConnectedTerminals: " + numOfConnectedTerminals.ToString());
        if (numOfUsedTiles == numOfTiles && numOfTerminals == numOfConnectedTerminals)
        {
            Debug.Log("STAGE COMPLETE!");
            puzzleManager.CompleteStage(level);
        }
    }

    private void RegisterNewlyConnectedTerminal(int terminalID, int tileCount)
    {
        tileCountInConnections[terminalID] = tileCount;
        tileCountInConnections[0] += tileCount;
        numOfConnectedTerminals += 1;
        numOfUsedTiles += tileCount;
        CheckForCompletion();
    }

    private void UnregisterTerminalConnection(int terminalID)
    {
        int tileCount = tileCountInConnections[terminalID];
        if (tileCount != 0)
        {
            tileCountInConnections[terminalID] = 0;
            tileCountInConnections[0] -= tileCount;
            numOfConnectedTerminals -= 1;
            numOfUsedTiles -= tileCount;
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
        Debug.Log("Connection Terminated");
    }

    public void TryCompleteConnection(WirePuzzleTerminalTile terminal)
    {
        if (terminal != startingTerminal && terminal.terminalID == activeTerminal)
        {
            WirePuzzleTile previousTile = activeConnection.Peek();
            ConnectTiles(previousTile, terminal);
            activeConnection.Push(terminal);
            int numOfTilesInConnection = activeConnection.Count;
            RegisterNewlyConnectedTerminal(terminal.terminalID, numOfTilesInConnection);
         } else
        {
            ResetActiveConnection();
        }
        TerminateConnection();
    }

    public WirePuzzleTile SecondLastTileInActiveConnection()
    {
        WirePuzzleTile lastTile = activeConnection.Pop();
        WirePuzzleTile secondLastTile = activeConnection.Peek();
        activeConnection.Push(lastTile);
        return secondLastTile;
    }

    public void ResetConnection(WirePuzzleTerminalTile terminal)
    {
        WirePuzzleTile nextTile = terminal;
        while (nextTile != null)
        {
            WirePuzzleTile currentTile = nextTile;
            nextTile = currentTile.outTile;
            currentTile.Disconnect();
        }
        UnregisterTerminalConnection(terminal.terminalID);
    }

    /*
    public void ResetActiveConnection()
    {
        if (activeTerminal != 0)
        {
            ResetConnection(startingTerminal);
        }
        TerminateConnection();
    }
    */

    public void ResetActiveConnection()
    {
        if (activeTerminal != 0)
        {
            while (activeConnection.Count > 0)
            {
                WirePuzzleTile tile = activeConnection.Pop();
                tile.Disconnect();
            }
        }
        TerminateConnection();
    }

    public void AddTileToActiveConnection(WirePuzzleTile tile)
    {
        WirePuzzleTile fromTile = activeConnection.Peek();
        ConnectTiles(fromTile, tile);
        activeConnection.Push(tile);
    }

    public void RemovePreviousTileFromActiveConnection()
    {
        WirePuzzleTile previousTile = activeConnection.Pop();
        previousTile.Disconnect();
    }

    private void ConnectTiles(WirePuzzleTile fromTile, WirePuzzleTile toTile)
    {
        int relativePosition = fromTile.CompareTo(toTile);
        switch (relativePosition)
        {
            case 0:
                // do nothing if same tile
                break;
            case 1:
                // toTile is above fromTile
                fromTile.CreateConnection(ConnectDirection.Up, toTile);
                toTile.CreateConnection(ConnectDirection.Down, fromTile);
                break;
            case 2:
                // toTile is to the right of fromTile
                fromTile.CreateConnection(ConnectDirection.Right, toTile);
                toTile.CreateConnection(ConnectDirection.Left, fromTile);
                break;
            case 3:
                // toTile is below fromTile
                fromTile.CreateConnection(ConnectDirection.Down, toTile);
                toTile.CreateConnection(ConnectDirection.Up, fromTile);
                break;
            case 4:
                // toTile is to the left of fromTile
                fromTile.CreateConnection(ConnectDirection.Left, toTile);
                toTile.CreateConnection(ConnectDirection.Right, fromTile);
                break;
            case -1:
                // terminate connection if diagonal
                TerminateConnection();
                break;
        }
    }

    public bool IsAdjacentToPreviousTile(WirePuzzleTile tile)
    {
        WirePuzzleTile previousTile = activeConnection.Peek();
        return tile.IsAdjacent(previousTile);
    }
}
