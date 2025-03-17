using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    private GridCell[,] gridCells;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // โหลด GridCell ที่มีอยู่ใน Grid Layout Group
        GridCell[] cells = GetComponentsInChildren<GridCell>();
        gridCells = new GridCell[3, 3];

        for (int i = 0; i < cells.Length; i++)
        {
            int x = i % 3;
            int y = i / 3;
            cells[i].gridPos = new Vector2Int(x, y);
            gridCells[x, y] = cells[i];
        }
    }

    public bool CanPlaceBlock(Vector2Int gridPos)
    {
        if (gridPos.x < 0 || gridPos.x >= 3 || gridPos.y < 0 || gridPos.y >= 3)
        {
            return false;
        }
        return !gridCells[gridPos.x, gridPos.y].isOccupied;
    }

    public void PlaceBlock(Vector2Int gridPos)
    {
        if (CanPlaceBlock(gridPos))
        {
            gridCells[gridPos.x, gridPos.y].SetOccupied(true);
        }
    }
}


