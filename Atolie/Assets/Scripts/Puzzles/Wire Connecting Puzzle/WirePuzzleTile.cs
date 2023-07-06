using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WirePuzzleTile : MonoBehaviour
{
    [SerializeField] private int row;
    [SerializeField] private int col;
    public WirePuzzleGrid grid;

    public WirePuzzleTile inTile;
    public WirePuzzleTile outTile;

    public int CompareTo(WirePuzzleTile tile)
    {
        if (tile.col == this.col)
        {
            if (tile.row < this.row)
            {
                // tile is above this
                return 1;
            }
            else if (tile.row > this.row)
            {
                // tile is below this
                return 3;
            }
            else
            {
                // tile is the same
                return 0;
            }
        } else if (tile.row == this.row)
        {
            if (tile.col > this.col)
            {
                // tile is to the right of this
                return 2;
            }
            else if (tile.col < this.col)
            {
                // tile is to the left of this
                return 4;
            }
            else
            {
                // tile is the same
                return 0;
            }
        }
        return -1;
    }

    public bool IsAdjacent(WirePuzzleTile tile)
    {
        if (Mathf.Abs(tile.row - this.row) == 1 && tile.col == this.col)
        {
            return true;
        }
        else if (Mathf.Abs(tile.col - this.col) == 1 && tile.row == this.row)
        {
            return true;
        }
        return false;
    }

    public abstract void Disconnect();

    public void AssignGrid(WirePuzzleGrid puzzlegrid)
    {
        grid = puzzlegrid;
    }

    public abstract void CreateConnection(ConnectDirection direction, WirePuzzleTile tile);
}

public enum ConnectDirection
{
    Unconnected,
    Up,
    Right,
    Down,
    Left
}