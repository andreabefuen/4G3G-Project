using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    //Requeriments to complete the quest
    public int requiredElements;
    public int currentElements;


    public bool IsReached()
    {
        return (currentElements >= requiredElements);
    }
    
    public void BuildWindmillGoal()
    {
        if (goalType == GoalType.BuildWindmill)
        {
            currentElements++;
        }
    }
    
      
    

}

public enum GoalType
{
    Happiness,
    Expand,
    Money,
    BuildWindmill,
    BuildSolarpanel
}
