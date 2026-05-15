using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
        public IdleSystem idleSystem; // daha performanslý ve daha temiz yoksa findojectoftype da okeydi
        public InteractionSystem interactionSystem;
        public int coins = 0;
    public NPC playerNPC;
    public float winScore = 400f;
    private bool hasWon = false;

    public int combo = 0;
    public float comboTimer = 0f;
    public List<NPC>allNPCs ;
    private UIManager ui;
    

        public void Start()
        {
         allNPCs = FindAllNPCs(); //npcleri bul //tüm npcleri toplar cunku sistemin calýþmasý ýcýn npc listesi gerkli
         Debug.Log("Bulunan NPC: " + allNPCs.Count);

        //idle sisteme npc listesi ve interaction sistemnini verdim cunlu ýdle sistem tek basýna calýþamaz veriye ihtiyac duyar

            idleSystem.allNPCs = allNPCs;
            idleSystem.interactionSystem = interactionSystem;

          AssignIDs(allNPCs); //bu yoksa relationshipsistemi bozulur dictionary sistemi de calýsmaz
          foreach (var npc in allNPCs)
          {
            var visualizer = npc.GetComponent<RelationshipVisualizer>();

            if(visualizer != null)
            {
                visualizer.npc = npc;
                visualizer.allNPCs = allNPCs;
            }

          }
        
         
        }
    public void Awake()
    {
        ui = FindObjectOfType<UIManager>();
    }

    public void Update()
    {
        comboTimer -= Time.deltaTime;
        if(comboTimer <= 0)
        {
            combo = 0;
        }
    }

   


    List<NPC> FindAllNPCs()
    {
        NPC[] npcs = FindObjectsOfType<NPC>(); //sahnedeki tüm npcleri bul
        return new List<NPC>(npcs); //listeye dondur cunku list daha esnek bir sistem ekleme silme v gibi konularda
    }

    void AssignIDs(List<NPC> npcs)
    {
        for (int i = 0; i < npcs.Count; i++)
        {
            npcs[i].Id = i; //her npcye özel bir ýd vermek index verdim cunku hýzlý ve basit index yerine guýd yada random id de olurdu
        }
    }
   
    public void AddCoins(int amount)
    {
        coins += amount;
        Debug.Log("Coins: " + coins);

    }

    //npcleri bul
    //ýd ver 
    //idlesistem e bagla
    //visuþaizere bagla

    public void CheckWin()
    { if (hasWon) return;

        float total = 0;
        foreach (var npc in allNPCs)
        {
            if (npc == playerNPC) continue;

            if (playerNPC.Relationships.ContainsKey(npc.Id))
            {
                total += playerNPC.Relationships[npc.Id].Trust;
            }

        }
        Debug.Log("Toplam Trust: " + total);
        if(total >= winScore)
        {
            hasWon = true;
            Debug.Log("KAZANDIN");

            //FindObjectOfType<UIManager>().ShowWinPanel();
            if (ui != null)
                ui.ShowWinPanel();
        }
    }
}
