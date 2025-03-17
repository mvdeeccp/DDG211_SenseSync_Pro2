using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.position;
        canvasGroup.blocksRaycasts = false;

        // ถ้ามี Parent (Slot) อยู่ ให้ปลดออกก่อน
        if (transform.parent != null)
        {
            transform.SetParent(transform.root);
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        // ถ้าไม่วางใน Slot ให้กลับไปตำแหน่งเดิม
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            rectTransform.position = originalPosition;
        }
    }
}
