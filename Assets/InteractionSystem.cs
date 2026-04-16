using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    private Dictionary<InteractionType, InteractionData> interactionConfigs;
    public static System.Action<NPC, NPC> OnInteractionEvent;
    
    private void Awake()
    {
        InitializeConfigs();
    }

    void InitializeConfigs()
    {
        interactionConfigs = new Dictionary<InteractionType, InteractionData>()
        {
            { InteractionType.Talk, new InteractionData(InteractionType.Talk, 5f, 2f) },
            { InteractionType.Help, new InteractionData(InteractionType.Help, 10f, 3f) },
            { InteractionType.Insult, new InteractionData(InteractionType.Insult, -15f, 5f) },
            { InteractionType.Gossip, new InteractionData(InteractionType.Gossip, -5f, 4f) }

        };



    }

    public void ProcessInteraction(NPC source, NPC target)
    {
        OnInteractionEvent?.Invoke(source, target);
        Debug.Log($"Interaction: {source.Id} -> {target.Id}");
        source.transform.localScale = Vector3.one * 1.2f;
        Invoke(nameof(ResetScale), 0.2f);

        void ResetScale()
        {
            source.transform.localScale = Vector3.one;
        }

        //npcler konuþtu iliþki degðisti ödül alýndý
        if (source == null || target == null || source == target) return;

        InteractionType type = ChooseInteraction(source, target);
        InteractionData data = interactionConfigs[type];

        float impact = CalculateImpact(data);

        ApplyRelationshipChange(source, target, impact);
        FindObjectOfType<GameManager>().AddCoins(1);
        CreateMemory(target, source, type, impact);

        GameEvents.OnInteraction?.Invoke(source, target);
        GameEvents.OnRelationshipChanged?.Invoke(source, target, impact);

        FindObjectOfType<SaveManager>().Save();

        //random event 
        //update relationship
        //trigger memory
    }

    InteractionType ChooseInteraction(NPC source, NPC target)
    {
        float trust = GetTrust(source, target);
        if (trust > 50) return InteractionType.Help;
        if (trust < -30) return InteractionType.Insult;

        int rand = Random.Range(0, 100);

        if (rand < 50) return InteractionType.Talk;
        return InteractionType.Gossip;
    }

    float CalculateImpact(InteractionData data)
    {
        float variance = Random.Range(-data.RandomVariance, data.RandomVariance);
        return data.BaseImpact + variance;
    }

    void ApplyRelationshipChange(NPC source, NPC target,float impact)
    {
        RelationshipData rel = GetOrCreateRelationship(source, target);

        rel.Trust += impact;
        rel.Trust = Mathf.Clamp(rel.Trust, -100f, 100f);
    }

    void CreateMemory(NPC receiver, NPC actor, InteractionType type, float impact)
    {
        RelationshipData rel = GetOrCreateRelationship(receiver, actor);
        Memory memory = new Memory()
        {
            Type = type.ToString(),
            Impact = impact,
            TimeStamp = Time.time
        };
        rel.Memories.Add(memory);
    }
    float GetTrust(NPC a, NPC b)
    {
        if (a.Relationships.TryGetValue(b.Id, out var rel))
            return rel.Trust;

        return 0f;
    }

    RelationshipData GetOrCreateRelationship(NPC a, NPC b)
    {
        if(!a.Relationships.ContainsKey(b.Id))
        {
            a.Relationships[b.Id] = new RelationshipData()
            {
                Trust = 0,
                Memories = new List<Memory>()
            };
        }
        return a.Relationships[b.Id];
    }

    void SpreadGossip(NPC source, NPC target, NPC thirdParty)
    {
        float impact = -3f;

        RelationshipData rel = GetOrCreateRelationship(thirdParty, target);
        rel.Trust += impact;

        GameEvents.OnRelationshipChanged?.Invoke(thirdParty, target, impact);
    }
}
