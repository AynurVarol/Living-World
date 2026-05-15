using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public int Id;
    public Dictionary<int, RelationshipData> Relationships = new Dictionary<int, RelationshipData>(); //býr npcnin diðerleriyle iliþkisini tutuyor hýzlý eriþebilmek icin d,ctionary kullandýk

    public void Interact(NPC target)
    {
        GameEvents.OnInteraction?.Invoke(this, target); //bu kýsým ben bir etkileþim baslatýyorum dmek icin  event sistem kullanma sebebim loose coupling, moduler sistem ve geniþletilebilir olmasý
    }

    public NPC ChooseTarget(List<NPC> allNPCs) //npc kiminle konusacak?
    {
        if (allNPCs.Count <= 1) return null;

        NPC target = null;
        int attempts = 5;

        while(attempts-- > 0) //birkac kez dene //sonsuz loopu önlemek için attempts kulllandým
        {
            var random = allNPCs[UnityEngine.Random.Range(0, allNPCs.Count)]; //rastgele npc secimi

            if(random != this) //npc kendisiyle konusmasýn
            {
                target = random;
                break;

            }

        }
        return target;
    }
    private void Update() //npcler hareket etsin cunku yasýyor hissi
    {
        transform.position += new Vector3(  //ýd ekleme sebebimiz herkesin ayný hareketi yapmasýný engellemek
            Mathf.Sin(Time.time + Id) * 0.001f,
            Mathf.Cos(Time.time + Id) * 0.001f,
            0
            );
    }

    private void OnMouseDown() //mobil için
    {
        FindObjectOfType<UIManager>().SelectNPC(this);
    }
}//bu scriptte data tutuldu event kullandým aý davranýþý verdim ve gorsel hareket de ekledim
