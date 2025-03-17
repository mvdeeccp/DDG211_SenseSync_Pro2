using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableBlock : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;  // ตำแหน่งเริ่มต้นของ Block
    private Transform parentTransform;  // ตำแหน่งพ่อแม่ของ Block

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        parentTransform = transform.parent;
        transform.SetParent(transform.root);  // ป้องกัน Block ซ้อนกัน
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition; // ทำให้ Object เคลื่อนที่ตามเมาส์
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject closestCell = FindClosestGridCell();

        if (closestCell != null) // ถ้าพบช่องที่ใกล้ที่สุดและยังว่างอยู่
        {
            transform.position = closestCell.transform.position; // วาง Block ลงไป
            transform.SetParent(closestCell.transform); // กำหนดให้ Block เป็นลูกของช่องนั้น
        }
        else
        {
            transform.position = startPosition; // ถ้าไม่มีช่องว่าง กลับไปที่เดิม
        }
    }


    private GameObject FindClosestGridCell()
    {
        float minDistance = Mathf.Infinity;
        GameObject closestCell = null;

        // ค้นหาทุก GridCell ที่มี Tag "GridCell"
        foreach (GameObject cell in GameObject.FindGameObjectsWithTag("GridCell"))
        {
            float distance = Vector3.Distance(transform.position, cell.transform.position);

            // ตรวจสอบว่า cell นั้นว่าง (ไม่มี Block วางอยู่)
            if (distance < minDistance && cell.transform.childCount == 0)
            {
                minDistance = distance;
                closestCell = cell;
            }
        }
        return closestCell;
    }

}

