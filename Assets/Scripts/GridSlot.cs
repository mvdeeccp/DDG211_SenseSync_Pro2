using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSlot : MonoBehaviour
{
    public bool isOccupied = false; // ช่องนี้ถูกใช้หรือไม่

    public bool CanPlaceObject()
    {
        return !isOccupied;
    }

    public void SetOccupied(bool state)
    {
        isOccupied = state;
    }
}

