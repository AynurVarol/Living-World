using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public int Id;
    public Dictionary<int, RelationshipData> Relationships = new Dictionary<int, RelationshipData>();

    public void Interact(NPC target)
    {
        GameEvents.OnInteraction?.Invoke(this, target);
    }

    public NPC ChooseTarget(List<NPC> allNPCs)
    {
        if (allNPCs.Count <= 1) return null;

        NPC target = null;
        int attempts = 5;

        while(attempts-- > 0)
        {
            var random = allNPCs[UnityEngine.Random.Range(0, allNPCs.Count)];

            if(random != this)
            {
                target = random;
                break;

            }

        }
        return target;
    }
    private void Update()
    {
        transform.position += new Vector3(
            Mathf.Sin(Time.time + Id) * 0.001f,
            Mathf.Cos(Time.time + Id) * 0.001f,
            0
            );
    }
}
