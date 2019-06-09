using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResearchManager : MonoBehaviour
{
    public TextMeshProUGUI costCoal, costWind, costSolar, costGas;
    public TextMeshProUGUI levelCoal, levelWind, levelSolar, levelGas;


    //---------------------------------------------------------

    Player player;
    InventoryBuilding inventory;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        inventory = GameObject.Find("TypesOfBuildings").GetComponent<InventoryBuilding>();



        UpdateLevelResearchCoal(inventory.coalFactoryStructure.levelResearch);
        UpdateCostResearchCoal(inventory.coalFactoryStructure.costResearches[inventory.coalFactoryStructure.levelResearch]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyResearchCoal()
    {
        if (inventory.coalFactoryStructure.levelResearch >= 4)
        {

            levelCoal.text = "COMPLETED!";
            return;
        }
        if (player.totalCurrency >= inventory.coalFactoryStructure.costResearches[inventory.coalFactoryStructure.levelResearch])
        {
            Debug.Log("LEVEL COAL FACTORY:  " + inventory.coalFactoryStructure.levelResearch);
            player.DecreaseMoney(inventory.coalFactoryStructure.costResearches[inventory.coalFactoryStructure.levelResearch]);
            
            inventory.coalFactoryStructure.levelResearch++;

            UpdateLevelResearchCoal(inventory.coalFactoryStructure.levelResearch);
            UpdateCostResearchCoal(inventory.coalFactoryStructure.costResearches[inventory.coalFactoryStructure.levelResearch-1]);

        }
        else
        {
            player.NotEnoughMoneyPlay();
        }
    }

    public void BuyResearchWindmill()
    {
        if (inventory.windmillStructure.levelResearch >= 4)
        {

            levelWind.text = "COMPLETED!";
            return;
        }
        if (player.totalCurrency >= inventory.windmillStructure.costResearches[inventory.windmillStructure.levelResearch])
        {
            Debug.Log("LEVEL WINDMILL:  " + inventory.windmillStructure.levelResearch);
            player.DecreaseMoney(inventory.windmillStructure.costResearches[inventory.windmillStructure.levelResearch]);

            inventory.windmillStructure.levelResearch++;

            UpdateLevelResearchWindmill(inventory.windmillStructure.levelResearch);
            UpdateCostResearchWind(inventory.windmillStructure.costResearches[inventory.coalFactoryStructure.levelResearch - 1]);

        }
        else
        {
            player.NotEnoughMoneyPlay();
        }
    }

    void UpdateLevelResearchCoal(int level)
    {
        levelCoal.text = "Research " + level-- + "/4";
    }
    void UpdateCostResearchCoal (int cost)
    {
        costCoal.text = "Cost: " + cost;
    }

    void UpdateLevelResearchWindmill(int level)
    {
        levelWind.text = "Research " + level-- + "/4";
    }
    void UpdateCostResearchWind(int cost)
    {
        costWind.text = "Cost: " + cost;

    }
}
