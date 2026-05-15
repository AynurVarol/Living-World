using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text nameText;
    public Text relationshipText;

    private NPC selectedNPC;

    public GameObject panel;
    public Text coinsText;

    [Header("Systems")]
    public InteractionSystem interactionSystem;
    public GameManager gameManager;
    public IdleSystem idleSystem;

    public int speedLevel = 0;
    public int speedCost = 10;

    public Text upgradeButtonText;
    public RectTransform upgradeButtonRect;

    public RectTransform coinsRect;
    int lastCoins = 0;
    [Header("Win UI")]
    public GameObject winPanel;

    [Header("Combo UI")]
    public Text comboText;
    public RectTransform comboRect;
    private SaveManager saveManager;


    private Coroutine comboRoutine;

    public Text goalText;
    public void Start()
    {
        panel.SetActive(false);
        winPanel.SetActive(false);
        saveManager = FindObjectOfType<SaveManager>();
    }
    private void Update()
    {
        UpdateCoinsUI();
        UpdateUpgradeUI();
        CheckCoinAnimation();



    }

    public void SelectNPC(NPC npc)
    {
        selectedNPC = npc;
        panel.SetActive(true);
        nameText.text = "NPC " + npc.Id;
        UpdateRelationships();
    }

    public void UpdateRelationships()
    {
        if (selectedNPC == null) return;
        string text = "";
        foreach(var rel in selectedNPC.Relationships)
        {
            text += "NPC " + rel.Key + ": " + rel.Value.Trust.ToString("F1") + "\n";

        }
        relationshipText.text = text;
    }

    //*****PLAYER INTERACTION*******
    public void InteractTalk() => Interact(InteractionType.Talk);
    public void InteractHelp() => Interact(InteractionType.Help);
    public void InteractInsult() => Interact(InteractionType.Insult);

    void Interact(InteractionType type)
    {
        if (selectedNPC == null || gameManager == null) return;
        // NPC[] allNPCs = FindObjectsOfType<NPC>();
        if (gameManager.allNPCs == null || gameManager.allNPCs.Count <= 1) return;
        //ayný npc secilmesin diye daha saðlam
        NPC target;
        do
        {
            target = gameManager.allNPCs[Random.Range(0, gameManager.allNPCs.Count)];
        }
        while (target == selectedNPC);

       /* var allNpc = gameManager.allNPCs;
        if (allNpc == null || allNpc.Count <= 1) return;

         NPC target = gameManager.allNPCs[Random.Range(0, gameManager.allNPCs.Count)];*/

       

        //NPC target = allNPCs[Random.Range(0, allNPCs.Length)];

        if (target != selectedNPC)
        {
            interactionSystem.ProcessInteractionWithType(selectedNPC, target, type);
            UpdateRelationships();
        }

    }



   /* public void InteractButton()
    {
        if (selectedNPC == null) return;

        //random target sec
        var allNPCs = FindObjectsOfType<NPC>();

        NPC target = allNPCs[Random.Range(0, allNPCs.Length)];

        if(target != selectedNPC)
        {
            interactionSystem.ProcessInteraction(selectedNPC, target);
            UpdateRelationships();
        }
       //Debug.Log("Manual interaction trigger");
    }*/


    //************COINS**************
    private void UpdateCoinsUI()
    {
        //coinsText.text = "Coins: " + FindObjectOfType<GameManager>().coins;
        coinsText.text = "Coins: " + gameManager.coins;

        //upgradeButtonText.text = "Speed Level" + speedLevel + "(" + speedCost + ")";

       
    }
    void CheckCoinAnimation()
    {
        //coin arttý mýÐ?
        if (gameManager.coins > lastCoins)
        {
            StartCoroutine(CoinPop());
        }
        lastCoins = gameManager.coins;
    }

    /*public void Upgrade()
     {
       StartCoroutine(ButtonPress()); //nutton buyume kuculme fonksyonu caðýr
       GameManager gm = FindObjectOfType<GameManager>();

       if(gm.coins < speedCost)
       {

           Debug.Log("YETERSÝZ COÝN");
           return;

       }
       //coin düþür
       gm.coins -= speedCost;

       //level arttýr
       speedLevel++;

       //sistemý hýzlandýrmak için
       FindObjectOfType<IdleSystem>().tickInterval *= 0.85f;

       //cost arttýr
       speedCost = Mathf.RoundToInt(speedCost * 1.5f);

       Debug.Log("Upgarde Level: " + speedLevel);

       FindObjectOfType<SaveManager>().Save(); //KAYDET 
    }*/

    IEnumerator CoinPop()
    {
        float duration = 0.15f;
        float timer = 0f;

        Vector3 original = Vector3.one;
        Vector3 target= Vector3.one * 1.15f;

        coinsRect.localScale = original;

        while(timer < duration)
        {
            timer += Time.deltaTime;

           // float t = timer / duration;
            coinsRect.localScale = Vector3.Lerp(original, target, timer / duration);

            yield return null;
        }

        timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            //float t = timer / duration;
            coinsRect.localScale = Vector3.Lerp(target,original, timer / duration);

            yield return null;
        }

        coinsRect.localScale = original;
  }
    //*****UPGRADE*****
    void UpdateUpgradeUI()
    {
        upgradeButtonText.text = "Speed lv." + speedLevel + "(" + speedCost + ")";
    }
     public void Upgrade()
    {
        StartCoroutine(ButtonPress()); //nutton buyume kuculme fonksyonu caðýr
        if(AudioManager.Instance != null)

        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.buttonSound);
        }
        //GameManager gm = FindObjectOfType<GameManager>();

        if (gameManager == null) return;
       /* {

            Debug.Log("YETERSÝZ COÝN");
            return;

        }*/
        //coin düþür
        gameManager.coins -= speedCost;
        //level arttýr
        speedLevel++;

        //sistemý hýzlandýrmak için
         idleSystem.tickInterval *= 0.85f;

        //cost arttýr
        speedCost = Mathf.RoundToInt(speedCost * 1.5f);

        Debug.Log("Upgarde Level: " + speedLevel);
        if(saveManager != null)
        saveManager.Save(); //KAYDET 
    }



    System.Collections.IEnumerator ButtonPress()
    {
        float duration = 0.1f; //0.08f falanda yapabýlýýrm sonradan
        float timer = 0f;

        Vector3 original = upgradeButtonRect.localScale;
        Vector3 pressed = original * 0.85f;

        //basýlma
        while(timer < duration)
        {
            timer += Time.deltaTime;
            //float t = timer / duration;

            upgradeButtonRect.localScale = Vector3.Lerp(original, pressed, timer / duration);

            yield return null;
        }
        timer = 0f;

        //normale don
        while(timer < duration)
        {
            timer += Time.deltaTime;
            //float t = timer / duration;

            upgradeButtonRect.localScale = Vector3.Lerp(pressed,original, timer / duration);

            yield return null;

        }
        upgradeButtonRect.localScale = original;
    }
    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }

    public void ShowCombo(int combo)
    {
        if(combo <= 1)
        {
            comboText.gameObject.SetActive(false);
            return;
        }

        comboText.gameObject.SetActive(true);
        comboText.text = "COMBO x" + combo;

        // StopAllCoroutines();
        if (comboRoutine != null)
            StopCoroutine(comboRoutine);

        comboRoutine = StartCoroutine(ComboPop());
        //StartCoroutine(ComboPop());
    }


    IEnumerator ComboPop() //animasyon
    {
        float duration = 0.15f;
        float timer = 0f;

        // Vector3 original = comboRect.localScale; burda çok büyüyor
        // Vector3 target = original * 1.4f;

        //her zaman sabit scale olsun 
        Vector3 original = Vector3.one;

        //maks büyüklük
        Vector3 target = Vector3.one * 1.15f;
        comboRect.localScale = original;

        while(timer < duration)
        {
            timer += Time.deltaTime;
            comboRect.localScale = Vector3.Lerp(original, target, timer / duration);
            yield return null;
        }

        timer = 0f;
        while(timer < duration)
        {
            timer += Time.deltaTime;
            comboRect.localScale = Vector3.Lerp(target, original, timer / duration);
            yield return null;

        }
        comboRect.localScale = original;
    }

    public void UpdateGoalUI(Goal goal)
    {
        goalText.text = goal.Type + ": " + goal.CurrentValue + "/" + goal.TargetValue;
    }

    public void ShowGoalComplete()
    {
        Debug.Log("Goal Completed");

        if (goalText != null)
            goalText.text = "Goal Completed!";

        //ekstra efekt
        StartCoroutine(GoalPopEffect());
    }
    IEnumerator GoalPopEffect()
    {
        Vector3 original = goalText.rectTransform.localScale;
        Vector3 target = original * 1.5f;

        float duration = 0.25f;
        float timer = 0f;

        while(timer < duration)
        {
            timer += Time.deltaTime;
            goalText.rectTransform.localScale = Vector3.Lerp(original, target, timer / duration);
            yield return null;
        }

        timer = 0f;
        while(timer < duration)
        {
            timer = Time.deltaTime;
            goalText.rectTransform.localScale = Vector3.Lerp(target, original, timer / duration);
            yield return null;
        }
        goalText.rectTransform.localScale = original;

    }

    public void ResetWorld()
    {
        if (gameManager == null) return;

        foreach(NPC npc in gameManager.allNPCs)
        {
            npc.Relationships.Clear();
        }
        gameManager.combo = 0;
        gameManager.coins = 0;

        Debug.Log("WORLD RESET");
    }
    public void QuitGame()
    {
        Debug.Log("Oyun kapandý");
        Application.Quit();
    }
}
