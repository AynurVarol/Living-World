using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject gameUI;

    private void Start()
    {
        startPanel.SetActive(true);
        gameUI.SetActive(false);
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        gameUI.SetActive(true);
    }
}
