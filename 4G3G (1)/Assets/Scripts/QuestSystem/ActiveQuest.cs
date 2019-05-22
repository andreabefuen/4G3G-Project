using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveQuest : MonoBehaviour
{
    public GameObject quests;
    public GameObject panelOfQuests;
    

    public Player player;
    public List<Quest> questToShow;
    List<GameObject> allTheQuest;

    // Start is called before the first frame update
    void Start()
    {
        // questToShow = player.GetActiveQuests();
        questToShow = new List<Quest>();
        
    }

    public void AddQuest(Quest newQuest)
    {
        if (questToShow.Contains(newQuest))
        {
            return;
        }
        questToShow.Add(newQuest);
        //Debug.Log("hay " + questToShow.Count);
        GameObject aux = Instantiate(panelOfQuests, quests.transform);
        aux.SetActive(true);
        aux.GetComponentsInChildren<Text>()[0].text = newQuest.title;
        aux.GetComponentsInChildren<Text>()[1].text = newQuest.description;

        allTheQuest.Add(aux);
        //allTheQuest.Add(aux);

        //questToShow.Clear();
    }

    public void DeleteThisQuest(Quest q)
    {
        questToShow.Remove(q);
        //Recorremos otra vez toda la lista para poner las que si estan
        //Eliminamos todos los gameobjects
        foreach(GameObject g in allTheQuest)
        {
            Destroy(g);
        }
        foreach(Quest qq in questToShow)
        {
            GameObject aux = Instantiate(panelOfQuests, quests.transform);
            aux.SetActive(true);
            aux.GetComponentsInChildren<Text>()[0].text = qq.title;
            aux.GetComponentsInChildren<Text>()[1].text = qq.description;

            allTheQuest.Add(aux);
        }
    }
}
