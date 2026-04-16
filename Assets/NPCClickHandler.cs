using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCClickHandler : MonoBehaviour
{
    public UIManager uýManager;
    private NPC npc;

    private void Start()
    {
        npc = GetComponent<NPC>();
    }

    private void OnMouseDown()
    {
        uýManager.SelectNPC(npc);
    }
}
