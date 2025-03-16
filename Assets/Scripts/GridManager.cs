using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 3;  // จำนวนแถว
    public int cols = 3;  // จำนวนคอลัมน์
    public float cellSize = 1.1f; // ขนาดของแต่ละช่อง
    public GameObject gridSlotPrefab; // Prefab ของ GridSlot

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        if (gridSlotPrefab == null)
        {
            Debug.LogError("GridSlot Prefab ไม่ถูกกำหนด!");
            return;
        }

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Vector3 spawnPosition = new Vector3(col * cellSize, -row * cellSize, 0);
                GameObject newSlot = Instantiate(gridSlotPrefab, spawnPosition, Quaternion.identity);
                newSlot.transform.SetParent(transform); // ทำให้เป็นลูกของ GridManager

                // ตั้งชื่อให้ช่อง (เช่น GridSlot_0_0, GridSlot_1_2)
                newSlot.name = $"GridSlot_{row}_{col}";
            }
        }
    }
}

