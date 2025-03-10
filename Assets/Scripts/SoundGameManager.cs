using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGameManager : MonoBehaviour
{
    public static SoundGameManager Instance;

    public List<SoundClip> allSounds = new List<SoundClip>();  // ใส่ใน Inspector
    private List<SoundClip> currentRoundSounds = new List<SoundClip>();

    public AudioSource audioSource;  // ใส่ AudioSource ตรงนี้

    private SoundColor[] playerChoices = new SoundColor[3];  // 3 ช่องให้ลากสีใส่

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartNewRound();
    }

    public void StartNewRound()
    {
        currentRoundSounds.Clear();
        List<SoundClip> shuffled = new List<SoundClip>(allSounds);
        shuffled.Shuffle();

        if (allSounds.Count < 3)
        {
            Debug.LogWarning("Not enough sounds in allSounds list! Add at least 3.");
        }

        int countToTake = Mathf.Min(3, shuffled.Count);
        currentRoundSounds.AddRange(shuffled.GetRange(0, countToTake));

        // Debug ลำดับเสียงที่สุ่มได้
        Debug.Log("New Round Sounds Order:");
        for (int i = 0; i < currentRoundSounds.Count; i++)
        {
            Debug.Log($"Slot {i + 1}: {currentRoundSounds[i].soundName} - Color: {currentRoundSounds[i].color}");
        }

        StartCoroutine(PlaySoundsInOrder());
    }


    IEnumerator PlaySoundsInOrder()
    {
        foreach (var sound in currentRoundSounds)
        {
            audioSource.clip = sound.clip;
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length + 0.2f);
        }

        Debug.Log("Waiting for player to arrange colors...");
    }

    public void SetPlayerColorChoice(int slotIndex, SoundColor color)
    {
        if (slotIndex >= 0 && slotIndex < playerChoices.Length)
        {
            playerChoices[slotIndex] = color;
        }
    }

    public void CheckAnswers()
    {
        bool isCorrect = true;
        Debug.Log("Checking Player Choices:");

        for (int i = 0; i < currentRoundSounds.Count; i++)
        {
            SoundColor correctColor = currentRoundSounds[i].color;
            SoundColor playerColor = playerChoices[i];

            if (correctColor != playerColor)
            {
                isCorrect = false;
            }

            Debug.Log($"Slot {i + 1}: Expected {correctColor}, Player Chose {playerColor}");
        }

        if (isCorrect)
        {
            Debug.Log("Correct Order!");
        }
        else
        {
            Debug.Log("Wrong Order!");
        }
    }


    public void OnSubmitButtonPressed()
    {
        CheckAnswers();
        StartNewRound();
    }
}
