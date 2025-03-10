using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotButton : MonoBehaviour
{
    public int slotIndex; // ตำแหน่งของช่อง (0,1,2)
    private SoundColor currentColor;
    private Image buttonImage;

    public List<Color> colorOptions; // กำหนดสีของปุ่ม (เชื่อมใน Inspector)
    private int colorIndex = 0;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        ChangeColor(0); // เริ่มต้นที่สีแรก
    }

    public void OnClickChangeColor()
    {
        colorIndex = (colorIndex + 1) % colorOptions.Count;
        ChangeColor(colorIndex);
    }

    private void ChangeColor(int index)
    {
        buttonImage.color = colorOptions[index];
        currentColor = (SoundColor)index;

        // บันทึกสีที่เลือกไว้ใน SoundGameManager
        SoundGameManager.Instance.SetPlayerColorChoice(slotIndex, currentColor);
    }
}
