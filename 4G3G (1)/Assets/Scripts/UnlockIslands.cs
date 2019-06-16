using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockIslands : MonoBehaviour
{
    public static UnlockIslands instance;

    public bool unlockCoal, unlockGas, unlockWind, unlockSolar;

    public GameObject lockCoal, lockGas, lockWind, lockSolar;

    // Start is called before the first frame update
    void Start()
    {
        //Inicio
        GameControl.control.LoadUnlockIslandInfo();
        unlockCoal = GameControl.control.unlockIslandCoal;
        unlockGas = GameControl.control.unlockIslandGas;
        unlockSolar = GameControl.control.unlockIslandSolar;
        unlockWind = GameControl.control.unlockIslandWind;
        ReloadUnlockIslands();

    }
    public void ReloadUnlockIslands()
    {
        if (unlockGas)
        {
            lockGas.GetComponent<Button>().interactable = true;
            lockGas.GetComponentsInChildren<Image>()[1].gameObject.SetActive(false);

        }
        if (unlockCoal)
        {

            lockCoal.GetComponent<Button>().interactable = true;
            lockCoal.GetComponentsInChildren<Image>()[1].gameObject.SetActive(false);

        }
        if (unlockSolar)
        {

            lockSolar.GetComponent<Button>().interactable = true;
            lockSolar.GetComponentsInChildren<Image>()[1].gameObject.SetActive(false);
        }
        if (unlockWind)
        {

            lockWind.GetComponent<Button>().interactable = true;
            lockWind.GetComponentsInChildren<Image>()[1].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ReloadUnlockIslands();

    }
}
