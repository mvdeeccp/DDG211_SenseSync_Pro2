using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSequenceManager : MonoBehaviour
{
    public SpriteRenderer[] colorSlots;  // ช่องสี 4 ช่อง (ช่อง 1, 2, 3, 4)

    public AudioSource audioSource;      // ใช้เล่นเสียง
    public AudioClip[] colorSounds;      // 4 เสียง (0 = เขียว, 1 = แดง, 2 = เหลือง, 3 = ฟ้า)

    private Color[] colors = {
        Color.green, Color.red, Color.yellow, Color.blue
    };

    private List<int> colorSequence = new List<int>();   // เก็บลำดับสีที่จะแสดง
    private List<int> randomSoundSequence = new List<int>();  // เก็บลำดับเสียงแบบสุ่ม 3 อัน

    void Start()
    {
        SetAllSlotsWhite();
        StartCoroutine(RunSequence());
    }

    IEnumerator RunSequence()
    {
        yield return StartCoroutine(ShowColorSequence());  // แสดงสีตามลำดับ
        yield return new WaitForSeconds(1f);               // เว้นจังหวะก่อนสุ่มเสียงรอบแรก

        SetColorSlotsActive(false);                        // ซ่อนช่องสีขาว
        yield return StartCoroutine(PlayRandomSounds());   // เล่นเสียงสุ่มรอบแรก

        yield return new WaitForSeconds(2f);               // พัก 2 วินาที

        yield return StartCoroutine(PlayRandomSoundsRepeat());  // เล่นเสียงเดิมซ้ำรอบสอง
        SetColorSlotsActive(true);                         // แสดงช่องสีขาวกลับมา
    }

    IEnumerator PlayRandomSoundsRepeat()
    {
        // เล่นลำดับเสียงเดิมซ้ำอีกรอบ (ใช้ randomSoundSequence เดิม ไม่สุ่มใหม่)
        foreach (int soundIndex in randomSoundSequence)
        {
            audioSource.PlayOneShot(colorSounds[soundIndex]);
            yield return new WaitForSeconds(1.5f);  // เว้นระยะระหว่างเสียง
        }
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
        yield return new WaitForSeconds(1f);  // รอ 1 วิ ก่อนเริ่มแสดงสี

        // สร้างลำดับสี (กำหนดเองหรือตามโจทย์)
        GenerateColorSequence();

        for (int i = 0; i < colorSequence.Count; i++)
        {
            int colorIndex = colorSequence[i];
            yield return StartCoroutine(ShowColor(i, colorIndex));
        }

        SetAllSlotsWhite();  // หลังจากแสดงครบ กลับเป็นสีขาวทั้งหมด
    }

    void GenerateColorSequence()
    {
        colorSequence.Clear();
        colorSequence.Add(0);  // ช่อง 1 = เขียว
        colorSequence.Add(1);  // ช่อง 2 = แดง
        colorSequence.Add(2);  // ช่อง 3 = เหลือง
        colorSequence.Add(3);  // ช่อง 4 = ฟ้า
    }

    IEnumerator ShowColor(int slotIndex, int colorIndex)
    {
        colorSlots[slotIndex].color = colors[colorIndex];  // เปลี่ยนเป็นสีจริง
        audioSource.PlayOneShot(colorSounds[colorIndex]);  // เล่นเสียงตามสี
        yield return new WaitForSeconds(1f);               // โชว์สี 1 วิ
        colorSlots[slotIndex].color = Color.white;         // กลับเป็นสีขาว
        yield return new WaitForSeconds(0.5f);             // หน่วง 0.5 วิ
    }

    IEnumerator PlayRandomSounds()
    {
        GenerateRandomSoundSequence();

        foreach (int soundIndex in randomSoundSequence)
        {
            // เล่นเสียงละ 1 ครั้ง
            audioSource.PlayOneShot(colorSounds[soundIndex]);
            yield return new WaitForSeconds(1.5f);  // เว้นระยะระหว่างเสียง
        }
    }

    void GenerateRandomSoundSequence()
    {
        randomSoundSequence.Clear();

        // นับจำนวนเสียงแต่ละเสียงที่ถูกเลือกแล้ว
        Dictionary<int, int> soundCount = new Dictionary<int, int>()
    {
        { 0, 0 },  // สีเขียว
        { 1, 0 },  // สีแดง
        { 2, 0 },  // สีเหลือง
        { 3, 0 }   // สีฟ้า
    };

        for (int i = 0; i < 3; i++)
        {
            List<int> availableSounds = new List<int>();

            // เพิ่มเสียงที่ยังไม่เกิน 2 ครั้ง ลงในกลุ่มสุ่มได้
            foreach (var kvp in soundCount)
            {
                if (kvp.Value < 2)
                {
                    availableSounds.Add(kvp.Key);
                }
            }

            // สุ่มเลือก 1 เสียงจากที่เหลือ
            int randomIndex = Random.Range(0, availableSounds.Count);
            int selectedSound = availableSounds[randomIndex];

            // บันทึกเสียงที่เลือก และเพิ่มจำนวนการใช้เสียงนั้น
            randomSoundSequence.Add(selectedSound);
            soundCount[selectedSound]++;
        }

        Debug.Log("Random Sound Sequence: " + string.Join(", ", randomSoundSequence));
    }

    void SetColorSlotsActive(bool isActive)
    {
        foreach (var slot in colorSlots)
        {
            slot.gameObject.SetActive(isActive);
        }
    }

    public void CheckPlayerAnswer(ColorPiece[] playerAnswers)
    {
        bool isCorrect = true;

        for (int i = 0; i < randomSoundSequence.Count; i++)
        {
            if (playerAnswers[i].colorIndex != randomSoundSequence[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            Debug.Log("ถูกต้อง!");
        }
        else
        {
            Debug.Log("ผิด ลองใหม่!");
        }
    }



}
