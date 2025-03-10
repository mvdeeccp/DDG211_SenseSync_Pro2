using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSequenceManager : MonoBehaviour
{
    public SpriteRenderer[] colorSlots;  // ลำดับของช่อง (ช่อง 1, ช่อง 2, ช่อง 3, ช่อง 4)

    public AudioSource audioSource;      // เล่นเสียง
    public AudioClip[] colorSounds;      // เสียงของแต่ละสี

    private Color[] colors = {
        Color.green, Color.red, Color.yellow, Color.blue
    };

    private List<int> colorSequence = new List<int>();  // ลำดับสีที่จะแสดง

    void Start()
    {
        SetAllSlotsWhite();
        StartCoroutine(ShowColorSequence());
    }

    void SetAllSlotsWhite()
    {
        foreach (var slot in colorSlots)
        {
            slot.color = Color.white;
        }
    }

    IEnumerator ShowColorSequence()
    {
        yield return new WaitForSeconds(1f);  // หน่วงก่อนเริ่ม
        GenerateSequence();

        for (int i = 0; i < colorSequence.Count; i++)
        {
            int colorIndex = colorSequence[i];
            yield return StartCoroutine(ShowColor(i, colorIndex));  // แสดงสีทีละช่องตามลำดับ
        }

        SetAllSlotsWhite();  // เมื่อจบ กลับเป็นขาวทั้งหมด
    }

    void GenerateSequence()
    {
        // ตัวอย่างลำดับที่กำหนดเอง
        colorSequence.Clear();
        colorSequence.Add(0);  // ช่องที่ 1 = เขียว
        colorSequence.Add(1);  // ช่องที่ 2 = แดง
        colorSequence.Add(2);  // ช่องที่ 3 = เหลือง
        colorSequence.Add(3);  // ช่องที่ 4 = ฟ้า
    }

    IEnumerator ShowColor(int slotIndex, int colorIndex)
    {
        colorSlots[slotIndex].color = colors[colorIndex];  // เปลี่ยนสีช่องตามลำดับ
        audioSource.PlayOneShot(colorSounds[colorIndex]);  // เล่นเสียง
        yield return new WaitForSeconds(1f);  // แสดง 1 วินาที
        colorSlots[slotIndex].color = Color.white;  // กลับเป็นสีขาว
        yield return new WaitForSeconds(0.5f);  // เว้นช่วงก่อนช่องต่อไป
    }
}
