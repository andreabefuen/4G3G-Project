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
    public List<GameObject> allTheQuest;

    // Start is called before the first frame update
    void Start()
    {
        // questToShow = player.GetActiveQuests();
        questToShow = new List<Quest>();
        allTheQuest = new List<GameObject>();

        this.gameObject.SetActive(false);
        
    }

    public void AddQuest(Quest newQuest)
    {
        /*if (questToShow.Contains(newQuest))
        {
            return;
        }*/
        questToShow.Add(newQuest);
        //Debug.Log("hay " + questToShow.Count);
        GameObject aux = Instantiate(panelOfQuests, quests.transform);
        
        aux.GetComponentsInChildren<Text>()[0].text = newQuest.title;
        aux.GetComponentsInChildren<Text>()[1].text = newQuest.description;
        aux.SetActive(true);
        
        allTheQuest.Add(aux);
        //if (allTheQuest[0] != null)
        //{
        //    allTheQuest[0].GetComponent<Image>().color = Color.green;
        //
        //}
        //allTheQuest.Add(aux);
        //allTheQuest.Add(aux);

        //questToShow.Clear();
    }

    public void DeleteThisQuest(Quest q)
    {
        Debug.Log("Delete this quest: " + q.title);
       
        //Destroy(allTheQuest[0].gameObject);
       // Destroy(allTheQuest[0]);
        allTheQuest[0].gameObject.SetActive(false);
        allTheQuest.RemoveAt(0);
        questToShow.RemoveAt(0);
        //Recorremos otra vez toda la lista para poner las que si estan
        //Eliminamos todos los gameobjects
        // foreach(GameObject g in allTheQuest)
        // {
        //     Destroy(g);
        // }

        /*/
                foreach (Quest qq in questToShow)
                {
                    GameObject aux = Instantiate(panelOfQuests, quests.transform);
                    aux.SetActive(true);
                    aux.GetComponentsInChildren<Text>()[0].text = qq.title;
                    aux.GetComponentsInChildren<Text>()[1].text = qq.description;

                    allTheQuest.Add(aux);
                }*/
        //allTheQuest[0].gameObject.GetComponent<Image>().color = Color.green;
        // if (allTheQuest[0] != null)
        // {
        //     allTheQuest[0].GetComponent<Image>().color = Color.green;
        //
        // }


    }
}
