using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    private Dictionary<InteractionType, InteractionData> interactionConfigs; //etkileþim ayarlarýný tutan sistem dictionary olmasýnýn sebebi hýzlý eriþim ve düzenli bir yapý olmasý
    public static System.Action<NPC, NPC> OnInteractionEvent; //baska sýstemler dinlesin observer pattern

    private GameManager gameManager;
    private SaveManager saveManager;
    GoalManager goalManager;
    CameraShake shake;
    UIManager ui;
    private void Awake()
    {
        InitializeConfigs();
        gameManager = FindObjectOfType<GameManager>();
        saveManager = FindObjectOfType<SaveManager>();
        shake = FindObjectOfType<CameraShake>();
        ui = FindObjectOfType<UIManager>();
        goalManager = FindObjectOfType<GoalManager>();
        if (goalManager == null) Debug.LogWarning("GoalManager yok");
        if (shake == null) Debug.LogWarning("Camera Shake yok");
        if (ui == null) Debug.LogWarning("UI Manager yok");
    }

    void InitializeConfigs()
    { //data-driven sistem kurdum 
        interactionConfigs = new Dictionary<InteractionType, InteractionData>()
        {
            { InteractionType.Talk, new InteractionData(InteractionType.Talk, 5f, 2f) },
            { InteractionType.Help, new InteractionData(InteractionType.Help, 10f, 3f) },
            { InteractionType.Insult, new InteractionData(InteractionType.Insult, -15f, 5f) },
            { InteractionType.Gossip, new InteractionData(InteractionType.Gossip, -5f, 4f) }

        };



    }

    public void ProcessInteractionWithType(NPC source, NPC target, InteractionType type)
    {
        if (source == null || target == null || source == target) return; //crhas onluyor
        ExecuteInteraction(source, target, type);
        /* OnInteractionEvent?.Invoke(source, target); //etkileþim oldu
         Debug.Log($"Interaction: {source.Id} -> {target.Id}");
         source.transform.localScale = Vector3.one * 1.2f; //npc buyusun
         Invoke(nameof(ResetScale), 0.2f);

         void ResetScale()
         {
             source.transform.localScale = Vector3.one;
         }

         //npcler konuþtu iliþki degðisti ödül alýndý
         /*if (source == null || target == null || source == target) return; //crhas onluyor

         InteractionType type = ChooseInteraction(source, target); //etileþim tipi secme iliþkiye göre davrnýþ degiþiyor 
         InteractionData data = interactionConfigs[type];

         float impact = CalculateImpact(data);

         ApplyRelationshipChange(source, target, impact);
         FindObjectOfType<GameManager>().AddCoins(1);
         CreateMemory(target, source, type, impact);

         GameEvents.OnInteraction?.Invoke(source, target);
         GameEvents.OnRelationshipChanged?.Invoke(source, target, impact);

         FindObjectOfType<SaveManager>().Save();*/

        //random event 
        //update relationship
        //trigger memory
    }

    //Ortak logýc
    public void ExecuteInteraction(NPC source, NPC target, InteractionType type)
    {
        if (gameManager == null) return;
        OnInteractionEvent?.Invoke(source, target); //etkileþim oldu

        if (goalManager != null) goalManager.AddProgress(GoalType.TriggerInteraction, 1);
        Debug.Log($"Interaction: {type} ({source.Id} -> {target.Id})");

        if(AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.interactionSound);
        }

        //var shake = FindObjectOfType<CameraShake>();
       /* if(shake != null)
        {
            shake.StartCoroutine(shake.Shake(0.1f, 0.1f));
        }*/

        //CAMERA TÝTREME EFEKTÝ
        if (shake != null)
            shake.StartCoroutine(shake.Shake(0.1f, 0.1f));

        //animasyon(buyume efekti)
        StartCoroutine(ScaleEffect(source));

        InteractionData data = interactionConfigs[type];

        float impact = CalculateImpact(data);

        //realtionship
        ApplyRelationshipChange(source, target, impact); //COMBO SÝSTEM

        //goal: trust
        if (goalManager != null) goalManager.AddProgress(GoalType.ReachTrust, Mathf.Abs(impact)); //abs yaptýk cunku negatif interactionda progress sayýlýr daha dengeli sistem
        //gameManager.AddCoins(1);

        //COMBO+COIN
        gameManager.combo++;
        gameManager.comboTimer = 2f;

        //goal: combo
        if (goalManager != null) goalManager.AddProgress(GoalType.Combo, gameManager.combo);
        int reward = 1 + gameManager.combo;
        gameManager.AddCoins(reward);
        //UI cache kullan
        if (ui != null)
            ui.ShowCombo(gameManager.combo);

        //combo sesi
        if(AudioManager.Instance != null && gameManager.combo == 3)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.comboSound);
        }


       // FindObjectOfType<UIManager>().ShowCombo(gameManager.combo);

        //MEMORY
        CreateMemory(target, source, type, impact);

        //EVENTS
        GameEvents.OnInteraction?.Invoke(source, target);
        GameEvents.OnRelationshipChanged?.Invoke(source, target, impact);
        //WIN CHECK
        gameManager.CheckWin();
        //SAVE
        //saveManager.Save();
        if(saveManager != null)
        {
            saveManager.Save();
        }
        //FLOATÝNG TEXT
        if (FloatingTextSpawner.Instance != null)
            FloatingTextSpawner.Instance.Spawn(reward, target.transform.position);
        //FloatingTextSpawner.Instance.Spawn(impact, target.transform.position);

    }

    public void ProcessInteraction(NPC source, NPC target)
    {
        if (source == null || target == null || source == target) return;
        InteractionType type = ChooseInteraction(source, target);
        ExecuteInteraction(source, target, type);
    }
    IEnumerator ScaleEffect(NPC npc)
    {
        Vector3 original = Vector3.one;
        Vector3 target = Vector3.one * 1.2f;

        float duration = 0.1f;
        float timer = 0;

        while(timer < duration)
        {
            timer += Time.deltaTime;
            npc.transform.localScale = Vector3.Lerp(original, target, timer / duration);
            yield return null;
        }
        timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            npc.transform.localScale = Vector3.Lerp(target, original, timer / duration);
            yield return null;
        }
        npc.transform.localScale = original;

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

    void CreateMemory(NPC receiver, NPC actor, InteractionType type, float impact) //npc gecmiþi hatýrlasýn
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
        if (a.Relationships.TryGetValue(b.Id, out var rel)) //guvenli veri okuma trygetvalue crash önlüyor
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
