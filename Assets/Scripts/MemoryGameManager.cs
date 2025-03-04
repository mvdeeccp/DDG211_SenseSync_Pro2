using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour
{
    public Transform[] dropSlots; // ช่องวางสี 3 ช่อง (ลำดับ 1-3)
    public List<int> correctSequence = new List<int>(); // รับจาก ColorSequenceManager

    public void SetCorrectSequence(List<int> sequence)
    {
        correctSequence = new List<int>(sequence);
    }

    public void CheckPlayerAnswer()
    {
        bool isCorrect = true;

        for (int i = 0; i < 3; i++)
        {
            Transform slot = dropSlots[i].transform;

            if (slot.childCount == 0)
            {
                isCorrect = false;
                break;
            }

            ColorPiece piece = slot.GetChild(0).GetComponent<ColorPiece>();

            if (piece.colorIndex != correctSequence[i])
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
