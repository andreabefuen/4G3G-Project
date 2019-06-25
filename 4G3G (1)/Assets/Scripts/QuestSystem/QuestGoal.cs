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
    
    public void BuildGoal(StructureBlueprint structure)
    {
        if (goalType == GoalType.BuildWindmill && structure.id == idBuildings.windmill)
        {
            currentElements++;
        }
        else if (goalType == GoalType.BuildSolarpanel && structure.id == idBuildings.solarpanel)
        {
            currentElements++;
        }
        else
        {
            Debug.Log("No solar or wind energy goal");
        }

    }
  
    public void IncreaseHappiness(int earnHappiness)
    {
        if(goalType == GoalType.Happiness)
        {
            currentElements += earnHappiness;
        }
        else
        {
            Debug.Log("The goal is not happiness");
        }
    }

    public void IncreaseTheGoalMoney(int earnMoney)
    {
        if(goalType == GoalType.Money)
        {
            currentElements += earnMoney;

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
