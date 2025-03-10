using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundColor
{
    Green,
    Red,
    Yellow,
    Blue
}

[System.Serializable]
public class SoundClip
{
    public string soundName;
    public AudioClip clip;
    public SoundColor color;
}

