using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
        public IdleSystem idleSystem;
        public InteractionSystem interactionSystem;
        public int coins = 0;

        private void Start()
        {
          List<NPC> npcs = FindAllNPCs();
        Debug.Log("Bulunan NPC: " + npcs.Count);

            idleSystem.allNPCs = npcs;
            idleSystem.interactionSystem = interactionSystem;

          foreach (var npc in npcs)
          {
            var visualizer = npc.GetComponent<RelationshipVisualizer>();

            if(visualizer != null)
            {
                visualizer.npc = npc;
                visualizer.allNPCs = npcs;
            }

          }

         
        }

   List<NPC> FindAllNPCs()
    {
        NPC[] npcs = FindObjectsOfType<NPC>();
        return new List<NPC>(npcs);
    }

    void AssignIDs(List<NPC> npcs)
    {
        for (int i = 0; i < npcs.Count; i++)
        {
            npcs[i].Id = i;
        }
    }
   
    public void AddCoins(int amount)
    {
        coins += amount;
        Debug.Log("Coins: " + coins);

    }
}
