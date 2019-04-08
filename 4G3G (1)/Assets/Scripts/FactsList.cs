using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactsList : MonoBehaviour
{
    public GameObject factWindow;
    public Text descriptionText;

    public List<Facts> allTheFacts;

    int cont = 0;




    public void ShowFact()
    {
        factWindow.SetActive(true);
        descriptionText.text = allTheFacts[cont].description;
    }

    public void OkButton()
    {
        factWindow.SetActive(false);
        if (cont + 1 < allTheFacts.Count)
        {
            cont++;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
