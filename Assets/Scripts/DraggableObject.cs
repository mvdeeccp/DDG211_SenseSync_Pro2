using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform parentAfterDrag;

    private void Start()
    {
        gameObject.SetActive(true);
        parentAfterDrag = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        transform.SetParent(transform.root); // แยกออกจาก Parent เพื่อให้ลากได้
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 10f));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        if (hitCollider != null)
        {
            Debug.Log("ชนกับ: " + hitCollider.name);
            if (hitCollider.CompareTag("GridSlot"))
            {
                Debug.Log("เป็น GridSlot");
                GridSlot slot = hitCollider.GetComponent<GridSlot>();
                if (slot.CanPlaceObject())
                {
                    Debug.Log("ช่องว่าง! วางวัตถุได้");
                    transform.position = slot.transform.position;
                    slot.SetOccupied(true);
                    return;
                }
                else
                {
                    Debug.Log("ช่องนี้ถูกใช้แล้ว!");
                }
            }
        }
        else
        {
            Debug.Log("ไม่ได้ชนกับอะไรเลย!");
        }

        // ถ้าวางไม่ได้ ให้กลับไปตำแหน่งเดิม
        transform.position = startPosition;
        transform.SetParent(parentAfterDrag);
    }

}
