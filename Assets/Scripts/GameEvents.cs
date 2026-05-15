using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameEvents 
{
    public static Action<NPC, NPC> OnInteraction;
    public static Action<NPC, NPC, float> OnRelationshipChanged;
}
