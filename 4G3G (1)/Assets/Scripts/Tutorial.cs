using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject[] allArrows;

    int cont = 0;

    public void GoToNextIndicator()
    {
        
        if (cont < allArrows.Length)
        {
            allArrows[cont].SetActive(false);

        
            cont++;
            if(cont < allArrows.Length)
            {
                allArrows[cont].SetActive(true);

            }
        }
        if(cont == allArrows.Length)
        {
            GameControl.control.tutorial = false;
            Destroy(this.gameObject);
        }


    }
    public void FirtsIndicator()
    {
        allArrows[cont].SetActive(true);

    }

    // Start is called before the first frame update
    void Start()
    {
       if(GameControl.control.tutorial == false)
        {
            Destroy(this.gameObject);
        } 
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
