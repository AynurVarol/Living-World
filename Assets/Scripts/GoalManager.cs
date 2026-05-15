using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public List<Goal> goals = new List<Goal>();
    //ublic UIManager ui;
    public GameManager gameManager;
    public UIManager uiManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        uiManager = FindObjectOfType<UIManager>();
    }
    public void AddProgress(GoalType type, float amount)
    {if (goals == null || goals.Count == 0) return;

        /* foreach(var goal in goals)
         {
             if (goal.Type != type || goal.IsCompleted) continue;

             goal.CurrentValue += amount;

             if(goal.CurrentValue >= goal.TargetValue && !goal.IsCompleted)
             {
                 goal.IsCompleted = true;
                 Debug.Log("GOAL COMPLETED: " + goal.Type);
                 CompleteGoal(goal);

                 uiManager.ShowGoalComplete();
             }
         }*/
        for (int i = 0; i < goals.Count; i++)
        {
            Goal goal = goals[i];

            if (goal.Type == type && !goal.IsCompleted)
            {
                goal.CurrentValue += amount;

                if (goal.CurrentValue >= goal.TargetValue)
                {
                    CompleteGoal(goal);
                }

                if (uiManager != null)
                {
                    uiManager.UpdateGoalUI(goal);
                }
            }
        }


    }

    void CompleteGoal(Goal goal)
    {
        goal.IsCompleted = true;

        //ödül
        if(gameManager != null)
        gameManager.AddCoins(20);

        if (uiManager != null) uiManager.ShowGoalComplete();

        ReplaceGoal(goal);
        
    }

    void ReplaceGoal(Goal oldGoal)
    {
        int index = goals.IndexOf(oldGoal);

        if(index >= 0)
        {
            goals[index] = GenerateRandomGoal();
        }
    }

    Goal GenerateRandomGoal()
    {
        GoalType type = (GoalType)Random.Range(0, System.Enum.GetValues(typeof(GoalType)).Length);
        Goal newGoal = new Goal();
        newGoal.Type = type;
        newGoal.CurrentValue = 0;
        newGoal.IsCompleted = false;

        switch(type)
        {
            case GoalType.TriggerInteraction:
                newGoal.TargetValue = Random.Range(5, 15);
                break;
            case GoalType.TotalTrust:
                newGoal.TargetValue = Random.Range(50, 150);
                break;
            case GoalType.ReachTrust:
                newGoal.TargetValue = Random.Range(20, 80);
                break;
            case GoalType.Combo:
                newGoal.TargetValue = Random.Range(3, 8);
                break;

        }
        return newGoal;

    }
    
}
