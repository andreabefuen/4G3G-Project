using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    public int levelCity;
    public bool isActive;
    public bool isOnTheList;
    public bool done = false;
    public string title;
    public string description;

    [Header("Rewards")]
    public int moneyReward;
    //public int energyReward;
    //public int happinessRewward;

    public QuestGoal goal;

    public void CompleteQuest()
    {
        isActive = false;
        done = true;
        Debug.Log(title + " was completed!");
    }

}
