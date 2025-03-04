using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;  // จำ parent เดิมไว้ (กรณีต้องคืนที่)
        transform.SetParent(transform.root); // ให้ follow mouse ได้อิสระ
        transform.SetAsLastSibling();        // ให้อยู่บนสุด
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("DropSlot"))
        {
            transform.SetParent(eventData.pointerEnter.transform);
        }
        else
        {
            transform.SetParent(parentAfterDrag);  // ถ้าไม่โดนช่องเป้าหมาย ก็กลับที่เดิม
        }

        transform.localPosition = Vector3.zero;  // ให้อยู่ตรงกลางช่องใหม่
    }
}
