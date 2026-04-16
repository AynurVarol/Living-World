using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionData 
{
    public InteractionType Type;
    public float BaseImpact;
    public float RandomVariance;

    public InteractionData(InteractionType type, float baseImpact, float variance)
    {
        Type = type;
        BaseImpact = baseImpact;
        RandomVariance = variance;
    }
}
