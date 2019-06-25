using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabsManager : MonoBehaviour
{
    //public Image lockImagen;
    //COAL>GAS>WIND>SOLAR
    [Header("ResearchTab")]
    public GameObject coalBuyTab, windBuyTab, gasBuyTab, solarBuyTab;
    
    public GameObject coalResearch, windResearch, gasResearch, solarResearch;
    [Header("Cost of research text")]
    public TextMeshProUGUI costCoal;
    public TextMeshProUGUI costWind;
    public TextMeshProUGUI costSolar;
    public TextMeshProUGUI costGas;

    [Header("Buy buttons scroll")]
    public GameObject coalBuy;
    public GameObject windBuy;
    public GameObject gasBuy;
    public GameObject solarBuy;

    [Header("Level of Research")]
    public TextMeshProUGUI levelCoal;
    public TextMeshProUGUI levelWind;
    public TextMeshProUGUI levelSolar;
    public TextMeshProUGUI levelGas;

    [Header("EnergyBuildings")]
    public TextMeshProUGUI costBuildCoal;
    public TextMeshProUGUI costBuildGas;
    public TextMeshProUGUI costBuildWind;
    public TextMeshProUGUI costBuildSolar;


    [Header("Monuments and other buildings")]
    public TextMeshProUGUI costMonumentLiberty;
    public TextMeshProUGUI costPark;
    public TextMeshProUGUI costEiffle;
    public TextMeshProUGUI costWheel;
    public TextMeshProUGUI costPisa;


    [Header("Tabs")]
    public Toggle panel1;
    public Toggle panel2;
    public Toggle panel3;
    //---------------------------------------------------------

    Player player;
    InventoryBuilding inventory;


    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        inventory = InventoryBuilding.inventory;

        
        //LockTabs();

        if (CheckCoalSuccess())
        {
            UnlockCoalFactory();

        }
        if (CheckGasSuccess())
        {
        
            UnlockGasFactory();
        }
        if (CheckSolarSuccess())
        {
            UnlockSolar();
        }
        if (CheckWindSuccess())
        {
            UnlockWind();
        }
        else
        {
            UpdateLevelResearchCoal(inventory.coalFactoryStructure.levelResearch);
            Debug.Log("Level of the coal research: " + inventory.coalFactoryStructure.levelResearch);
            if (!CheckCoalSuccess())
            {
                UpdateCostResearchCoal(inventory.coalFactoryStructure.costResearches[inventory.coalFactoryStructure.levelResearch]);
            }
            

                UpdateTextCostBuildCoal(inventory.coalFactoryStructure.cost);
                UpdateTextCostBuildGas(inventory.gasExtractorStructure.cost);
                UpdateTextCostBuildWind(inventory.windmillStructure.cost);
                UpdateTextCostBuildSolar(inventory.solarPanelStructure.cost);

                UpdateTextCostLiberty(inventory.statueOfLiberty.cost);
                UpdateTextCostPark(inventory.park.cost);

                UpdateTextCostPisa(inventory.pisaTower.cost);
                UpdateTextCostWheel(inventory.ferrisWheel.cost);
                UpdateTextCostEiffle(inventory.eiffleTower.cost);
        }


        
        




    }

    public void UpdateLockInBuildings()
    {
        if (Player.instance.unlockCoal)
        {
            UnlockCoalFactory();

        }
        if (Player.instance.unlockGas)
        {
            UnlockGasFactory();
        }

        if (Player.instance.unlockSolar)
        {
            UnlockSolar();
        }
        if (Player.instance.unlockWind)
        {
            UnlockWind();
        }

    }

    public void UpdateAllTheCost()
    {


        UpdateLevelResearchCoal(inventory.coalFactoryStructure.levelResearch);
        UpdateCostResearchCoal(inventory.coalFactoryStructure.costResearches[inventory.coalFactoryStructure.levelResearch]);

        UpdateLevelResearchGas(inventory.gasExtractorStructure.levelResearch);
        UpdateCostResearchGas(inventory.gasExtractorStructure.costResearches[inventory.gasExtractorStructure.levelResearch]);

        UpdateLevelResearchWindmill(inventory.windmillStructure.levelResearch);
        UpdateCostResearchWind(inventory.windmillStructure.costResearches[inventory.windmillStructure.levelResearch]);

        UpdateLevelResearchSolar(inventory.solarPanelStructure.levelResearch);
        UpdateCostResearchSolar(inventory.solarPanelStructure.costResearches[inventory.solarPanelStructure.levelResearch ]);



        
        /*if (CheckCoalSuccess())
        {
            UnlockCoalFactory();

        }
        if (CheckGasSuccess())
        {

            UnlockGasFactory();
        }
        if (CheckSolarSuccess())
        {
            UnlockSolar();
        }
        if (CheckWindSuccess())
        {
            UnlockWind();
        }
        */
        UpdateTextCostBuildCoal(inventory.coalFactoryStructure.cost);
        UpdateTextCostBuildGas(inventory.gasExtractorStructure.cost);
        UpdateTextCostBuildWind(inventory.windmillStructure.cost);
        UpdateTextCostBuildSolar(inventory.solarPanelStructure.cost);

        UpdateTextCostLiberty(inventory.statueOfLiberty.cost);
        UpdateTextCostPark(inventory.park.cost);

        UpdateTextCostPisa(inventory.pisaTower.cost);
        UpdateTextCostWheel(inventory.ferrisWheel.cost);
        UpdateTextCostEiffle(inventory.eiffleTower.cost);

        Debug.Log("LOADING THE COST OF THE BUILDINGS");

        //this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {


       
    }

    public void BuyResearchCoal()
    {
        if (CheckCoalSuccess())
        {

            levelCoal.text = "COMPLETED!";
            //Call the method for unlock the other panels
            //UnlockTabs();
            UnlockCoalFactory();
            Player.instance.unlockCoal = true;
            
            return;
        }
        if (player.totalCurrency >= inventory.coalFactoryStructure.costResearches[inventory.coalFactoryStructure.levelResearch])
        {
            //Debug.Log("LEVEL COAL FACTORY:  " + inventory.coalFactoryStructure.levelResearch);
            player.DecreaseMoney(inventory.coalFactoryStructure.costResearches[inventory.coalFactoryStructure.levelResearch]);
            
            inventory.coalFactoryStructure.levelResearch++;

            UpdateLevelResearchCoal(inventory.coalFactoryStructure.levelResearch);
            UpdateCostResearchCoal(inventory.coalFactoryStructure.costResearches[inventory.coalFactoryStructure.levelResearch]);

        }
        else
        {
            player.NotEnoughMoneyPlay();
        }
    }

    public void BuyResearchGas()
    {
        if (CheckGasSuccess())
        {
            levelGas.text = "COMPLETED!";
            UnlockGasFactory();
            Player.instance.unlockGas = true;
            return;
        }
        if (player.totalCurrency >= inventory.gasExtractorStructure.costResearches[inventory.gasExtractorStructure.levelResearch])
        {
            //Debug.Log("LEVEL COAL FACTORY:  " + inventory.coalFactoryStructure.levelResearch);
            player.DecreaseMoney(inventory.gasExtractorStructure.costResearches[inventory.gasExtractorStructure.levelResearch]);

            inventory.gasExtractorStructure.levelResearch++;

            UpdateLevelResearchGas(inventory.gasExtractorStructure.levelResearch);
            UpdateCostResearchGas(inventory.gasExtractorStructure.costResearches[inventory.gasExtractorStructure.levelResearch]);

        }
        else
        {
            player.NotEnoughMoneyPlay();
        
        }
    }

    private void UpdateCostResearchGas(int cost)
    {
        costGas.text = "Cost: " + cost;
    }

    private void UpdateLevelResearchGas(int level)
    {
        levelGas.text = "Research " + level++ + "/4";
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
    void UpdateTextCostPark (int cost)
    {
        costPark.text = "Cost: " + cost;
    }

    void UpdateTextCostEiffle(int cost)
    {
        costEiffle.text = "Cost: " + cost;
    }

    void UpdateTextCostWheel(int cost)
    {
        costWheel.text = "Cost: " + cost;
    }

    void UpdateTextCostPisa(int cost)
    {
        costPisa.text = "Cost: " + cost;
    }

    public void BuyResearchWindmill()
    {
        if (CheckWindSuccess())
        {

            levelWind.text = "COMPLETED!";
            UnlockWind();
            Player.instance.unlockWind = true;
            return;
        }
        if (player.totalCurrency >= inventory.windmillStructure.costResearches[inventory.windmillStructure.levelResearch])
        {
            Debug.Log("LEVEL WINDMILL:  " + inventory.windmillStructure.levelResearch);
            player.DecreaseMoney(inventory.windmillStructure.costResearches[inventory.windmillStructure.levelResearch]);

            inventory.windmillStructure.levelResearch++;

            UpdateLevelResearchWindmill(inventory.windmillStructure.levelResearch);
            UpdateCostResearchWind(inventory.windmillStructure.costResearches[inventory.windmillStructure.levelResearch]);

        }
        else
        {
            player.NotEnoughMoneyPlay();
        }
    }
    public void BuyResearchSolar()
    {
        if (CheckSolarSuccess())
        {
            levelSolar.text = "COMPLETED!";
            UnlockSolar();
            Player.instance.unlockSolar = true;
            return;
        }
        if (player.totalCurrency >= inventory.solarPanelStructure.costResearches[inventory.solarPanelStructure.levelResearch])
        {
            Debug.Log("LEVEL WINDMILL:  " + inventory.solarPanelStructure.levelResearch);
            player.DecreaseMoney(inventory.solarPanelStructure.costResearches[inventory.solarPanelStructure.levelResearch]);

            inventory.solarPanelStructure.levelResearch++;

            UpdateLevelResearchSolar(inventory.solarPanelStructure.levelResearch);
            UpdateCostResearchSolar(inventory.solarPanelStructure.costResearches[inventory.solarPanelStructure.levelResearch]);

        }
        else
        {
            player.NotEnoughMoneyPlay();
        }
    }

    private void UpdateCostResearchSolar(int cost)
    {
        costSolar.text = "Cost: " + cost; ;
    }

    void UnlockCoalFactory()
    {
        levelCoal.text = "COMPLETED!";
        costCoal.text = "";
        coalBuy.transform.Find("Lock").gameObject.SetActive(false);
        coalBuy.GetComponent<Button>().interactable = true;
        coalBuyTab.transform.Find("Lock").gameObject.SetActive(false);

        //Unlock the next energy
        gasResearch.transform.Find("Lock").gameObject.SetActive(false);

        UpdateLevelResearchGas(inventory.gasExtractorStructure.levelResearch);
        UpdateCostResearchGas(inventory.gasExtractorStructure.costResearches[inventory.gasExtractorStructure.levelResearch]);




    }

    void UnlockGasFactory()
    {
        levelGas.text = "COMPLETED!";
        costGas.text = "";
        gasBuy.transform.Find("Lock").gameObject.SetActive(false);
        gasBuy.GetComponent<Button>().interactable = true;
        gasBuyTab.transform.Find("Lock").gameObject.SetActive(false);

        //Unlock the next energy
        windResearch.transform.Find("Lock").gameObject.SetActive(false);
  
        Debug.Log("Wind unlock");
    }

    void UnlockSolar()
    {
        levelSolar.text = "COMPLETED!";
        costSolar.text = "";
        solarBuy.transform.Find("Lock").gameObject.SetActive(false);
        solarBuy.GetComponent<Button>().interactable = true;
        solarBuyTab.transform.Find("Lock").gameObject.SetActive(false);



        //Unlock the next energy
        // windResearch.transform.Find("Lock").gameObject.SetActive(false);
    }

    void UnlockWind()
    {
        levelWind.text = "COMPLETED!";
        costWind.text = "";
        windBuy.transform.Find("Lock").gameObject.SetActive(false);
        windBuy.GetComponent<Button>().interactable = true;
        windBuyTab.transform.Find("Lock").gameObject.SetActive(false);

        solarResearch.transform.Find("Lock").gameObject.SetActive(false);


    }

    bool CheckCoalSuccess()
    {
        return inventory.coalFactoryStructure.levelResearch >= 3;
    }
    bool CheckGasSuccess()
    {
        return inventory.gasExtractorStructure.levelResearch >= 3;
    }
    bool CheckWindSuccess()
    {
        return inventory.windmillStructure.levelResearch >= 3;
    }
    bool CheckSolarSuccess()
    {
        return inventory.solarPanelStructure.levelResearch >= 3;
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
        levelCoal.text = "Research " + level++ + "/4";
    }
    void UpdateCostResearchCoal (int cost)
    {
        costCoal.text = "Cost: " + cost;
    }
    
    void UpdateLevelResearchSolar(int level)
    {
        levelSolar.text = "Research " + level++ + "/4";
    }

    void UpdateCosstResearchSolar ( int cost)
    {
        costSolar.text = "Cost: " + cost;
    }


    void UpdateLevelResearchWindmill(int level)
    {
        levelWind.text = "Research " + level++ + "/4";
    }
    void UpdateCostResearchWind(int cost)
    {
        costWind.text = "Cost: " + cost;

    }
}
