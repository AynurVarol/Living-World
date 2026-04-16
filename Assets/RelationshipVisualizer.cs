using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationshipVisualizer : MonoBehaviour
{
    public NPC npc;
    public List<NPC> allNPCs;

    private Dictionary<int, LineRenderer> lines = new Dictionary<int, LineRenderer>();

    public float minTrustToShow = 10f;

    private void Start()
    {
        InteractionSystem.OnInteractionEvent += HighlightConnection;
    }

    private void OnDestroy()
    {
        InteractionSystem.OnInteractionEvent -= HighlightConnection;
    }


    private void Update()
    {
        if (npc == null || allNPCs == null) return;

        foreach(var target in allNPCs)
        {
            if (target == npc) continue;

            float trust = 0;

            if (npc.Relationships.ContainsKey(target.Id))
                trust = npc.Relationships[target.Id].Trust;
            //duduk trust gizle
            if(Mathf.Abs(trust) < minTrustToShow)
            {
                if(lines.ContainsKey(target.Id))
                {
                    lines[target.Id].enabled = false;
                }
                continue;
            }

            UpdateLine(target, trust);
        }
    }
     void UpdateLine(NPC target, float trust)
     {
        if(!lines.ContainsKey(target.Id))
        {
            GameObject lineObj = new GameObject("Line_" + target.Id);
            lineObj.transform.parent = transform;

            LineRenderer lr = lineObj.AddComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.material = new Material(Shader.Find("Sprites/Default"));

            lines[target.Id] = lr;
        }

        LineRenderer line = lines[target.Id];
        line.enabled = true;

        //pozisyonlar
        line.SetPosition(0, npc.transform.position);
        line.SetPosition(1, target.transform.position);

        //renk ayarý(gradient mantýðý)
        //
        //Color color = Color.white;
        Color color = Color.Lerp(Color.red, Color.green, (trust + 100f) / 200f);
        

        line.startColor = color;
        line.endColor = color;


        /*if (trust > 0)
            color = Color.green;
        else if (trust < 0)
            color = Color.red;*/
        //KALINLIK
        float width = Mathf.Clamp(Mathf.Abs(trust) / 100f, 0.02f, 0.2f);
        line.startWidth = width;
        line.endWidth = width;

    }

    void HighlightConnection(NPC source, NPC target)
    {
        if (source != npc && target != npc) return;
        NPC other = source == npc ? target : source;

        if (!lines.ContainsKey(other.Id)) return;
        LineRenderer line = lines[other.Id];

        StartCoroutine(FlashLine(line));

    }
     IEnumerator FlashLine(LineRenderer line)
    {
        float timer = 0f;
        float baseWidht = line.startWidth;

        while(timer < 0.3f)
        {
            timer += Time.deltaTime;

            float t = Mathf.PingPong(timer * 10f, 1f);
            float width = Mathf.Lerp(0.05f, 0.25f, t);

            line.startWidth = width;
            line.endWidth = width;

            yield return null;
        }

        //eski haline don
        line.startWidth = baseWidht;
        line.endWidth = baseWidht;
    }
}
