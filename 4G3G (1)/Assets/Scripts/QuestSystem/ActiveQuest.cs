using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveQuest : MonoBehaviour
{
    public GameObject quests;
    public GameObject panelOfQuests;
    

    Player player;
    List<Quest> questToShow;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        questToShow = player.GetActiveQuests();
    }

    public void PutAllTheActiveQuests()
    {
        if(questToShow !=null)
        {
            foreach (Quest q in questToShow)
            {
                if (!q.isOnTheList)
                {
                    GameObject aux = Instantiate(panelOfQuests, quests.transform);
                    aux.SetActive(true);
                    aux.GetComponentsInChildren<Text>()[0].text = q.title;
                    aux.GetComponentsInChildren<Text>()[1].text = q.description;
                    q.isOnTheList = true;
                }
               
                
            }
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
