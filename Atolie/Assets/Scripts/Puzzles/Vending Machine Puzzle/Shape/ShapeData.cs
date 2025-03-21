using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class ShapeData : ScriptableObject
{
    [System.Serializable]
    public class Row
    {
        public bool[] column;
        private int size = 0;

        public Row() { }
        public Row(int size)
        {
            CreateRow(size);
        }

        public void CreateRow(int size)
        {
            this.size = size;
            column = new bool[size];
            ClearRow();
        }

        public void ClearRow()
        {
            for (int i = 0; i < size; i++)
            {
                column[i] = false;
            }
        }
    }

    public int columns = 0;
    public int rows = 0;
    public Row[] board;

    public void Clear()
    {
        for (var i = 0; i < rows; i++)
        {
            board[i].ClearRow();
        }
    }

    public void CreateNewBoard()
    {
        board = new Row[rows];

        for (var i = 0; i < rows; i++)
        {
            board[i] = new Row(columns);
        }
    }
}
