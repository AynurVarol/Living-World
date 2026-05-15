using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
// npclerin otomatik olalrak etkileþime girmesini saðlýyor bu kýsým 
public class IdleSystem : MonoBehaviour 
{
    public List<NPC> allNPCs;
    public InteractionSystem interactionSystem; //dýþarýdan game managerden veriliyor loose coupling ve modulerlik için

    public float tickInterval = 2f; //iki saniyede bir sistem calýsýyor
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime; //gecen zaman
        if(timer >= tickInterval) //süre doldu mu 
        {
            Tick(); //sistemi çalýþtýr
            timer = 0f; //durdur
        }
    }

    void Tick()
    {
        Debug.Log("TICK ÇALIÞTI");
        Debug.Log("NPC SAYISI: " + allNPCs.Count);
        foreach(var npc in allNPCs)
        {
            NPC target = npc.ChooseTarget(allNPCs); //hedef seciyor

            if (target != null) //kontrol 
            {
                //InteractionType type = interactionSystem.ChooseInteraction(npc, target);
                interactionSystem.ProcessInteraction(npc, target);
                //interactionSystem.ProcessInteractionWithType(npc, target, type);
            }
                
        }
    }

    public void Simulate(float deltaTime)
    {
        //tick system
    }
}
