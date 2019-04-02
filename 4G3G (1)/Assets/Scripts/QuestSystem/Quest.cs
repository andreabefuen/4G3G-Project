using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    public bool isActive;

    public bool isOnTheList = false;

    public string title;
    public string description;

    public int energyReward;

    public QuestGoal goal;

    public void CompleteQuest()
    {
        isActive = false;
        Debug.Log(title + "was completed!");
    }

}
