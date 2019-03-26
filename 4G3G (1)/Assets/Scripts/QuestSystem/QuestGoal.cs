using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    //Requeriments to complete the quest
    public int requiredWindmills;
    public int currentWindmills;


    public bool IsReached()
    {
        return (currentWindmills >= requiredWindmills);
    }
   //
   // public void BuildWindmillGoal()
   //{
   //    if(goalType == GoalType.Expand)
   //    {
   //        currentWindmills++;
   //    }
   //}

}

public enum GoalType
{
    Happiness,
    Expand,
    Money
}
