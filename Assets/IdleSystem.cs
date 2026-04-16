using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSystem : MonoBehaviour 
{
    public List<NPC> allNPCs;
    public InteractionSystem interactionSystem;

    public float tickInterval = 2f;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= tickInterval)
        {
            Tick();
            timer = 0f;
        }
    }

    void Tick()
    {
        Debug.Log("TICK ÇALIÞTI");
        Debug.Log("NPC SAYISI: " + allNPCs.Count);
        foreach(var npc in allNPCs)
        {
            NPC target = npc.ChooseTarget(allNPCs);
            
            if(target != null)
            {
                interactionSystem.ProcessInteraction(npc, target);
            }
        }
    }

    public void Simulate(float deltaTime)
    {
        //tick system
    }
}
