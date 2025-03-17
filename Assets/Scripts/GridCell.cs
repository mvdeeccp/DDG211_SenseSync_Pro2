using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour
{
    public bool isOccupied = false; // เช็คว่าช่องนี้มีบล็อกอยู่ไหม
    public Vector2Int gridPos; // ตำแหน่งของช่องใน Grid

    public void SetOccupied(bool state)
    {
        isOccupied = state;
    }
}

