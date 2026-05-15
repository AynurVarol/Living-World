using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public GameObject tutorialPanel;

    
    void Start()
    {
        //ilk açýlýþta göster
        tutorialPanel.SetActive(true);

        //oyun dursun
        Time.timeScale = 0f;
        
    }

   public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
        Time.timeScale = 1f;

    }
}
