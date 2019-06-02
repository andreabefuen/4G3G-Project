using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBuilding : MonoBehaviour
{
    public GameObject notEnergyImagen;
    public GameObject energyOkayImagen;
    // Start is called before the first frame update
    void Start()
    {
        notEnergyImagen.SetActive(true);
        energyOkayImagen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NotEnergy()
    {
        notEnergyImagen.SetActive(true);
        energyOkayImagen.SetActive(false);
    }
    public void EnoughtEnergy()
    {
        notEnergyImagen.SetActive(false);
        energyOkayImagen.SetActive(true);

        
    }
}
