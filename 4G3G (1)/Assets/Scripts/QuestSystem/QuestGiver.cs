using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    

    public Quest quest;

    public Player player;


    public GameObject questWindow;
    public Text titleText;
    public Text descriptionText;
    

    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
    }

    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        quest.isActive = true;
        //give to the player the quest assign the quest
        //player.quest = quest; (create a list of quest)
        player.activeQuest.Add(quest);
    }
}
