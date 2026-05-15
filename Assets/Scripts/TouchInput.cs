using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    private UIManager uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }
    void Update()
    {
        if (Input.touchCount == 0) return;
        {
            Touch touch = Input.GetTouch(0);
           
            if(touch.phase == TouchPhase.Began)
            {
                Handheld.Vibrate();
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit))
                {
                    NPC npc = hit.collider.GetComponent<NPC>();

                    if(npc != null)
                    {
                        uiManager.SelectNPC(npc);

                    }
                }
            }
        }
        
    }

    
}
