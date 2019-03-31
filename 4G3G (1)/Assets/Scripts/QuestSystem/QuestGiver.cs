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

    BuildManager buildManager;

    int cont = 0;

    private void Start()
    {
        buildManager = BuildManager.instance;
        OpenQuestWindow();

    }

    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = quest[cont].title;
        descriptionText.text = quest[cont].description;
    }

    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        quest[cont].isActive = true;
        //give to the player the quest assign the quest
        //player.quest = quest; (create a list of quest)
        player.activeQuest.Add(quest[cont]);
        if(cont+1 < quest.Count)
        {
            cont++;
            //To open all the quest
            //OpenQuestWindow();
        }
        
    }
}
