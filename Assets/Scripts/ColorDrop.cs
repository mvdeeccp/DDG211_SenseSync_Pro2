using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorDrop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)  // ถ้ามีของอยู่แล้วไม่รับเพิ่ม
        {
            GameObject dropped = eventData.pointerDrag;
            dropped.transform.SetParent(transform);
            dropped.transform.localPosition = Vector3.zero;
        }
    }
}
