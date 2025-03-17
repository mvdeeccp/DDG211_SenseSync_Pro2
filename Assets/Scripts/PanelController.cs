using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject selectionPanel;

    public void OpenPanel()
    {
        selectionPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        selectionPanel.SetActive(false);
    }
}

