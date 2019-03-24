using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnergyWindmills : MonoBehaviour
{
    public GameObject energyCanvas;

    InventoryBuilding inventory;

    float timer;
    float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("TypesOfBuildings").GetComponent<InventoryBuilding>();
        timer = 0f;
        spawnTimer = inventory.windmillStructure.timeEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > spawnTimer)
        {
            energyCanvas.SetActive(true);
        }
    }

    public void CollectEnergy()
    {
        Debug.Log("Collected the energy");
        energyCanvas.SetActive(false);
        timer = 0f;
    }
}
