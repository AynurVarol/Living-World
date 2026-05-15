using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoalType
{
    ReachTrust,
    TotalTrust,
    TriggerInteraction,
    Combo
}


[System.Serializable]
public class Goal
{
    public GoalType Type;
    public float TargetValue;
    public float CurrentValue;
    public bool IsCompleted;

}
