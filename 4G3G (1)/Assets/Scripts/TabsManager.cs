using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabsManager : MonoBehaviour
{
    [Header("ResearchTab")]
    public TextMeshProUGUI costCoal, costWind, costSolar, costGas;
    public TextMeshProUGUI levelCoal, levelWind, levelSolar, levelGas;

    [Header("EnergyBuildings")]
    public TextMeshProUGUI costBuildCoal;
    public TextMeshProUGUI costBuildGas;
    public TextMeshProUGUI costBuildWind;
    public TextMeshProUGUI costBuildSolar;


    [Header("Monuments")]
    public TextMeshProUGUI costMonumentLiberty;


    [Header("Tabs")]
    public Toggle panel1;
    public Toggle panel2;
    public Toggle panel3;
    //---------------------------------------------------------

    Player player;
    InventoryBuilding inventory;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        inventory = GameObject.Find("TypesOfBuildings").GetComponent<InventoryBuilding>();

        LockTabs();

        UpdateLevelResearchCoal(inventory.coalFactoryStructure.levelResearch);
        UpdateCostResearchCoal(inventory.coalFactoryStructure.costResearches[inventory.coalFactoryStructure.levelResearch]);

        UpdateTextCostBuildCoal(inventory.coalFactoryStructure.cost);
        UpdateTextCostBuildGas(inventory.gasExtractorStructure.cost);
        UpdateTextCostBuildWind(inventory.windmillStructure.cost);
        UpdateTextCostBuildSolar(inventory.solarPanelStructure.cost);

        UpdateTextCostLiberty(inventory.statueOfLiberty.cost);
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
            //Call the method for unlock the other panels
            UnlockTabs();
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

    void UpdateTextCostBuildCoal(int cost  )
    {
        costBuildCoal.text = "Cost: " + cost;
    }
    void UpdateTextCostBuildWind(int cost)
    {
        costBuildWind.text = "Cost: " + cost;
    }
    void UpdateTextCostBuildGas(int cost)
    {
        costBuildGas.text = "Cost: " + cost;
    }
    void UpdateTextCostBuildSolar(int cost)
    {
        costBuildSolar.text = "Cost: " + cost;
    }

    void UpdateTextCostLiberty (int cost)
    {
        costMonumentLiberty.text = "Cost: " + cost;
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
    void LockTabs()
    {
        panel1.GetComponent<Toggle>().interactable = false;
        panel2.interactable = false;
    }
    void UnlockTabs()
    {
        panel1.GetComponent<Toggle>().interactable = true;
        panel2.interactable = true;
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
