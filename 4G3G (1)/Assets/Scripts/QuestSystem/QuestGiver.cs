using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    

    public List<Quest> quest;

    public Player player;


    public GameObject questWindow;
    public Text titleText;
    public Text descriptionText;
    public Text rewardText;


    public GameObject activeQuestPanel;

    BuildManager buildManager;
    ActiveQuest activeQuest;

    int cont = 0;

    private void Start()
    {
        /*buildManager = BuildManager.instance;
        if (!GameControl.control.loaded)
        {
            OpenQuestWindow();

        }*/

        activeQuest = activeQuestPanel.GetComponent<ActiveQuest>();

    }

    public void OpenQuestWindow()
    {
        if(cont< quest.Count)
        {
            if(quest[cont].done == true)
            {
                cont++;
            }
            if (quest[cont].levelCity <= player.levelCity)
            {
                questWindow.SetActive(true);
                titleText.text = quest[cont].title;
                descriptionText.text = quest[cont].description;
                rewardText.text = "Reward: " + quest[cont].moneyReward.ToString() + "$";
            }

        }

    }

    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        quest[cont].isActive = true;
        //give to the player the quest assign the quest
        //player.quest = quest; (create a list of quest)
        player.activeQuest.Add(quest[cont]);
        
        activeQuest.AddQuest(quest[cont]);
        if (cont < quest.Count)
        {
            cont++;
            //To open all the quest
            //OpenQuestWindow();
        }
       
        
        
    }

    public void ClaimReward()
    {
        if (player.activeQuest[0].goal.IsReached() && player.activeQuest[0].isActive)
        {
            Debug.Log("Yes reward");
            activeQuest.DeleteThisQuest(player.activeQuest[0]);
            player.IncreaseMoney(player.activeQuest[0].moneyReward);
            player.activeQuest[0].CompleteQuest();
            player.activeQuest.Remove(player.activeQuest[0]);
            
            
        }
    }
}
