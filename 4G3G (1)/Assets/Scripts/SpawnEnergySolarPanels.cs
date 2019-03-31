using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnEnergySolarPanels : MonoBehaviour
{

    public GameObject energyCanvas;

    InventoryBuilding inventory;
    TextMeshProUGUI moneyText;

    float timer;
    float spawnTimer;
    int energy;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("TypesOfBuildings").GetComponent<InventoryBuilding>();
        timer = 0f;
        moneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        spawnTimer = inventory.solarPanelStructure.timeEnergy;
        energy = inventory.solarPanelStructure.energyPerTap;
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
        int total = int.Parse(moneyText.text);

        total += energy;
        moneyText.text = total.ToString();
        timer = 0f;
    }
}
