using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnergy : MonoBehaviour
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
        inventory = InventoryBuilding.inventory;
        timer = 0f;
        if(this.gameObject.tag == "Factory")
        {
            spawnTimer = inventory.coalFactoryStructure.timeMoney;
            money = inventory.coalFactoryStructure.moneyPerTap;
        }
        else if(this.gameObject.tag == "Gas")
        {
            spawnTimer = inventory.gasExtractorStructure.timeMoney;
            money = inventory.gasExtractorStructure.moneyPerTap;
        }
        else if (this.gameObject.tag == "Windmill")
        {
            spawnTimer = inventory.windmillStructure.timeMoney;
            money = inventory.windmillStructure.moneyPerTap;
        }
        else if (this.gameObject.tag == "Solar")
        {
            spawnTimer = inventory.solarPanelStructure.timeMoney;
            money = inventory.solarPanelStructure.moneyPerTap;
        }

        // energy = inventory.coalFactoryStructure.energyPerTap;

        player = GameObject.Find("Player").GetComponent<Player>();

        Destroy(GetComponent<BoxCollider>(), 0.5f);
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
        ReloadSpawnMoney();
        
    }

    public void ReloadSpawnMoney()
    {
        if (this.gameObject.tag == "Factory")
        {
            spawnTimer = inventory.coalFactoryStructure.timeMoney;
            money = inventory.coalFactoryStructure.moneyPerTap;
        }
        else if (this.gameObject.tag == "Gas")
        {
            spawnTimer = inventory.gasExtractorStructure.timeMoney;
            money = inventory.gasExtractorStructure.moneyPerTap;
        }
        else if (this.gameObject.tag == "Windmill")
        {
            spawnTimer = inventory.windmillStructure.timeMoney;
            money = inventory.windmillStructure.moneyPerTap;
        }
        else if (this.gameObject.tag == "Solar")
        {
            spawnTimer = inventory.solarPanelStructure.timeMoney;
            money = inventory.solarPanelStructure.moneyPerTap;
        }

    }
}

