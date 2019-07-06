using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    public List<Tut> tutorialInstructions;

    public GameObject[] allArrows;

    public GameObject tutorialWindow;

    List<GameObject> windows;

    int cont = 0;
    int contWin = 0;

    public void GoToNextIndicator(int num)
    {
        
        if (num < allArrows.Length)
        {
            allArrows[num].SetActive(false);

            num++;
            cont++;

            OpenTutorialWindow();
            if(num < allArrows.Length)
            {
                allArrows[num].SetActive(true);

            }
        }
        if(num == allArrows.Length)
        {
            GameControl.control.tutorial = false;
            Destroy(this.gameObject);
        }


    }
    public void FirtsIndicator()
    {
        allArrows[cont].SetActive(true);
        OpenTutorialWindow();

    }

    // Start is called before the first frame update
    void Start()
    {
       if(GameControl.control.tutorial == false)
        {
            Destroy(this.gameObject);
        }
        else
        {
            windows = new List<GameObject>(tutorialInstructions.Count);
            Invoke("OpenTutorialWindow", 1.5f);
           // WelcomeTutorial();
        }
       
    }

    void OpenTutorialWindow()
    {
        if (contWin < tutorialInstructions.Count)
        {
            GameObject aux = Instantiate(tutorialWindow, this.transform);
            aux.GetComponent<RectTransform>().localPosition = tutorialInstructions[contWin].position;

            aux.GetComponentsInChildren<Text>()[0].text = tutorialInstructions[contWin].title;
            aux.GetComponentsInChildren<Text>()[1].text = tutorialInstructions[contWin].description;
            aux.SetActive(true);

            windows.Add(aux);

            Invoke("AcceptTutorialWindow", tutorialInstructions[contWin].seconds);
        }

    }
    public void AcceptTutorialWindow()
    {

       // Debug.Log("Coss dentro: " + windows.Count);
        if(windows.Count > 0)
        {
            if(windows[0].activeInHierarchy == true)
            {
                // windows[0].SetActive(false);
                Destroy(windows[0]);
                windows.Clear();
                contWin++;
                //OpenTutorialWindow();
            }

        }



    }

    void WelcomeTutorial()
    {
        GameObject aux = Instantiate(tutorialWindow, this.transform);


        aux.GetComponentsInChildren<Text>()[0].text = "Welcome to your island!";
        aux.GetComponentsInChildren<Text>()[1].text = "To start the game you need to build your town hall! You can do that tapping in the blue square, try it!";
        aux.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Tut 
{
    public string title;
    public string description;

    public float seconds;

    public Vector2 position;
}
