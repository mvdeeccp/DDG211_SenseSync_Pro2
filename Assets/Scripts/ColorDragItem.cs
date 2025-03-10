using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorDragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public SoundColor color;

    private Transform parentAfterDrag;
    public RectTransform rectTransform;
    private GameObject cloneItem;  // สำเนาที่จะสร้างเมื่อเริ่มลาก
    private bool isClone = false; // เช็คว่าเป็นสำเนาหรือไม่

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // เฉพาะต้นฉบับเท่านั้นที่สร้างสำเนาได้
        if (!isClone)
        {
            cloneItem = Instantiate(gameObject, transform.parent);
            cloneItem.transform.SetSiblingIndex(transform.GetSiblingIndex());
            cloneItem.GetComponent<ColorDragItem>().color = this.color;

            // บอกว่าสำเนานี้เป็น clone เพื่อไม่ให้สร้างเพิ่มอีก
            cloneItem.GetComponent<ColorDragItem>().isClone = true;
        }

        // ตั้งค่าการลาก
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
