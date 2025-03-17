using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridManager1 : MonoBehaviour
    {
    public GameObject gridCellPrefab; // Prefab ของช่อง
    public int gridSize = 3; // ขนาดของ Grid (3x3)

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int i = 0; i < gridSize * gridSize; i++)
        {
            Instantiate(gridCellPrefab, transform);
        }
    }
}

