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

    int contActiveQuest = 0;

    private void Start()
    {
        /*buildManager = BuildManager.instance;
        if (!GameControl.control.loaded)
        {
            OpenQuestWindow();

        }*/

        activeQuest = activeQuestPanel.GetComponent<ActiveQuest>();

        Invoke("ReloadActiveQuest", 1f);
        

    }

    public void OpenQuestWindow()
    {
        if(contActiveQuest< quest.Count)
        {
            if(quest[contActiveQuest].done == true)
            {
                contActiveQuest++;
                OpenQuestWindow();
            }
            if (quest[contActiveQuest].levelCity <= player.levelCity)
            {
                questWindow.SetActive(true);
                titleText.text = quest[contActiveQuest].title;
                descriptionText.text = quest[contActiveQuest].description;
                rewardText.text = "Reward: " + quest[contActiveQuest].moneyReward.ToString() + "$";
            }

        }

    }

    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        quest[contActiveQuest].isActive = true;
        //give to the player the quest assign the quest
        //player.quest = quest; (create a list of quest)
        player.activeQuest.Add(quest[contActiveQuest]);
        
        activeQuest.AddQuest(quest[contActiveQuest]);
        if (contActiveQuest < quest.Count)
        {
            contActiveQuest++;
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

            player.cantCompletedQuest++;
        }
    }

    public void ReloadActiveQuest()
    {
        if(player.cantCompletedQuest == 0)
        {
            Debug.Log("Not quest activated");
            return;
        }
        for (int i = 0; i < player.cantCompletedQuest; i++)
        {
            quest[i].done = true;
            
        }
        Debug.Log("Player had done " + player.cantCompletedQuest + " quest");
    }
}
