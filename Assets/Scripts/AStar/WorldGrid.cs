using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WorldGrid : Singleton<WorldGrid>
{
    protected WorldGrid() { }

    [Serializable]
    public enum CellState
    {
        OPEN,
        BLOCKED
    };

    public Vector2Int worldSize = new Vector2Int();
    public Vector2Int worldOffset;
    [SerializeField]
    public int[,] cells;

    public int[] cellsX;
    public int[] cellsY;

    public void Init()
    {
        worldOffset = new Vector2Int(worldSize.x / 2, worldSize.y / 2);
        cells = new int[worldSize.x, worldSize.y];
    }

    public void Set(int x, int y, CellState state)
    {
        if(cells == null)
        {
            Init();
        }
        x += worldOffset.x;
        y += worldOffset.y;
        if (x < cells.GetUpperBound(0) || y < cells.GetUpperBound(1))
        {
            cells[x, y] = (int)state;
        }
        else
        {
            Debug.Log("Tried to set WorldGrid cells which were out of bounds");
        }
    }

    public bool Check(int x, int y, out int cellState)
    {
        if (cells == null)
        {
            Init();
        }
        x += worldOffset.x;
        y += worldOffset.y;
        if(x >= cells.GetUpperBound(0) || y >= cells.GetUpperBound(1))
        {
            cellState = (int)CellState.BLOCKED;
            return false;
        }
        cellState = cells[x, y];
        return true;
    }

    void GetCells(ref int[,] cellsArray)
    {
        cellsArray = cells;
    }
}
