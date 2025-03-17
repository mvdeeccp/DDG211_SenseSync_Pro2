using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockSpawner : MonoBehaviour
{
    public GameObject normalBlockPrefab; // Prefab ของ Block ปกติ
    public GameObject lBlockPrefab; // Prefab ของ Block ทรง L
    public Transform spawnParent; // ที่วาง Block (Canvas/Panel)

    void Start()
    {
        SpawnBlocks();
    }

    void SpawnBlocks()
    {
        if (normalBlockPrefab != null && lBlockPrefab != null && spawnParent != null)
        {
            // สร้าง Block ปกติ
            GameObject normalBlock = Instantiate(normalBlockPrefab, spawnParent);
            normalBlock.transform.localPosition = new Vector3(-100, 0, 0); // ตำแหน่งทางซ้าย

            // สร้าง L-Block
            GameObject lBlock = Instantiate(lBlockPrefab, spawnParent);
            lBlock.transform.localPosition = new Vector3(100, 0, 0); // ตำแหน่งทางขวา
        }
        else
        {
            Debug.LogError("Prefab หรือ SpawnParent ไม่ได้ถูกตั้งค่า!");
        }
    }
}

