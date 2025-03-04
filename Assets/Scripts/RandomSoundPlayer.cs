using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    public AudioSource audioSource; // อ้างถึง AudioSource
    public AudioClip[] colorSounds; // 4 เสียง (0 = เขียว, 1 = แดง, 2 = เหลือง, 3 = ฟ้า)

    private List<int> selectedSounds = new List<int>(); // เก็บลำดับเสียงที่สุ่มได้

    void Start()
    {
        StartCoroutine(PlayRandomSounds());
    }

    IEnumerator PlayRandomSounds()
    {
        // สุ่มเสียง 3 จาก 4
        GenerateRandomSounds();

        // เล่นทีละเสียงตามลำดับที่สุ่มได้
        foreach (int soundIndex in selectedSounds)
        {
            audioSource.PlayOneShot(colorSounds[soundIndex]);
            yield return new WaitForSeconds(1.5f);  // เว้นช่วงระหว่างเสียง (ปรับได้ตามชอบ)
        }
    }

    void GenerateRandomSounds()
    {
        selectedSounds.Clear();
        List<int> pool = new List<int>() { 0, 1, 2, 3 }; // 4 เสียง

        for (int i = 0; i < 3; i++)  // เลือกมา 3 เสียง
        {
            int randomIndex = Random.Range(0, pool.Count);
            selectedSounds.Add(pool[randomIndex]);
            pool.RemoveAt(randomIndex);  // เอาออกเพื่อไม่ให้ซ้ำ
        }

        // (Optional) เช็คได้ว่ามีลำดับไหนบ้าง
        Debug.Log("Random Sequence: " + string.Join(", ", selectedSounds));
    }
}
