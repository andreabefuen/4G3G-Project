using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnergyCoalFactory : MonoBehaviour
{
    public GameObject energyCanvas;

    InventoryBuilding inventory;


    Player player;
    float timer;
    float spawnTimer;
    int money, energy;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("TypesOfBuildings").GetComponent<InventoryBuilding>();
        timer = 0f;
        spawnTimer = inventory.coalFactoryStructure.timeMoney;
        money = inventory.coalFactoryStructure.moneyPerTap;
       // energy = inventory.coalFactoryStructure.energyPerTap;

        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnTimer)
        {
            energyCanvas.SetActive(true);
        }
    }

    public void CollectEnergy()
    {
        Debug.Log("Collected the energy");
        energyCanvas.SetActive(false);

        player.IncreaseMoney(money);
       // player.IncreaseEnergy(energy);

        timer = 0f;
    }
}

