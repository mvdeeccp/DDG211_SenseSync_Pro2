using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // ตรวจสอบว่ามี Object อยู่แล้วหรือไม่
        if (transform.childCount == 0)
        {
            GameObject droppedObject = eventData.pointerDrag;
            if (droppedObject != null)
            {
                droppedObject.transform.SetParent(transform);
                droppedObject.transform.position = transform.position;
            }
        }
    }
}






