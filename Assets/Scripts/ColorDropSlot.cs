using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorDropSlot : MonoBehaviour, IDropHandler
{
    public int slotIndex;

    public void OnDrop(PointerEventData eventData)
    {
        ColorDragItem dragItem = eventData.pointerDrag.GetComponent<ColorDragItem>();
        if (dragItem != null)
        {
            dragItem.transform.SetParent(transform);
            dragItem.rectTransform.anchoredPosition = Vector2.zero;

            SoundGameManager.Instance.SetPlayerColorChoice(slotIndex, dragItem.color);
        }
    }
}
