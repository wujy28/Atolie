using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WirePuzzleTile : MonoBehaviour
{
    public bool used;
    [SerializeField] private int row;
    [SerializeField] private int col;
    public WirePuzzleGrid grid;

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

    public abstract void Disconnect();

    public void AssignGrid(WirePuzzleGrid puzzlegrid)
    {
        grid = puzzlegrid;
    }

    public abstract void CreateConnection(ConnectDirection direction);
}

public enum ConnectDirection
{
    Unconnected,
    Up,
    Right,
    Down,
    Left
}