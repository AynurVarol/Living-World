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
    public GameManager gameManager;
    public IdleSystem idleSystem;

    public int speedLevel = 0;
    public int speedCost = 10;

    public Text upgradeButtonText;
    public RectTransform upgradeButtonRect;

    public RectTransform coinsRect;
    int lastCoins = 0;

    public void Start()
    {
        panel.SetActive(false);
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

    public InteractionSystem interactionSystem;
    public void InteractButton()
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
    }

    private void Update()
    {
        coinsText.text = "Coins: " + FindObjectOfType<GameManager>().coins;
        //coinsText.text = "Coins: " + gameManager.coins;

        upgradeButtonText.text = "Speed Level" + speedLevel + "(" + speedCost + ")";

        //coin arttý mýÐ?
        if(gameManager.coins > lastCoins)
        {
            StartCoroutine(CoinPop());
        }
        lastCoins = gameManager.coins;



       
    }
     public void Upgrade()
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
     }
   
    System.Collections.IEnumerator CoinPop()
    {
        float duration = 0.2f;
        float timer = 0f;

        Vector3 originalScale = coinsRect.localScale;
        Vector3 targetScale = originalScale * 1.3f;

        while(timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            coinsRect.localScale = Vector3.Lerp(originalScale, targetScale, t);

            yield return null;
        }

        timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            coinsRect.localScale = Vector3.Lerp(targetScale,originalScale, t);

            yield return null;
        }

        coinsRect.localScale = originalScale;
    }
    
    System.Collections.IEnumerator ButtonPress()
    {
        float duration = 0.1f; //0.08f falanda yapabýlýýrm sonradan
        float timer = 0f;

        Vector3 originalScale = upgradeButtonRect.localScale;
        Vector3 pressedScale = originalScale * 0.85f;

        //basýlma
        while(timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;

            upgradeButtonRect.localScale = Vector3.Lerp(originalScale, pressedScale, t);

            yield return null;
        }
        timer = 0f;

        //normale don
        while(timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;

            upgradeButtonRect.localScale = Vector3.Lerp(pressedScale,originalScale, t);

            yield return null;

        }
        upgradeButtonRect.localScale = originalScale;
    }
}
