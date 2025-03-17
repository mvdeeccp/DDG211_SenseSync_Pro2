using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDrop : MonoBehaviour, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector3 originalPosition; // จำตำแหน่งเดิม

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition; // บันทึกตำแหน่งก่อนลาก
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // ตรวจสอบว่ามี GridCell ใกล้เคียงไหม
        GameObject closestCell = FindClosestGridCell();
        if (closestCell != null)
        {
            // Snap ไปที่ GridCell
            rectTransform.position = closestCell.transform.position;
        }
        else
        {
            // ถ้าไม่มี GridCell ให้กลับตำแหน่งเดิม
            rectTransform.anchoredPosition = originalPosition;
        }
    }

    GameObject FindClosestGridCell()
    {
        float minDistance = float.MaxValue;
        GameObject closestCell = null;
        GameObject[] gridCells = GameObject.FindGameObjectsWithTag("GridCell");

        foreach (GameObject cell in gridCells)
        {
            float distance = Vector3.Distance(transform.position, cell.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestCell = cell;
            }
        }

        return (minDistance < 50f) ? closestCell : null; // ถ้าห่างจาก Grid น้อยกว่า 50 ให้ Snap
    }
}
