using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmButton : MonoBehaviour
{
    public Button confirmButton;

    void Start()
    {
        confirmButton.onClick.AddListener(ConfirmBlockPlacement);
    }

    void ConfirmBlockPlacement()
    {
        DragBlock[] blocks = FindObjectsOfType<DragBlock>();
        foreach (var block in blocks)
        {
            block.ConfirmPlacement();
        }
    }
}
