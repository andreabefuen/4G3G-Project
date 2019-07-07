using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum idBuildings
{
    windmill = 1,
    solarpanel = 2,
    coalfactory = 3,
    gasextractor = 4,
    houses = 5,
    river = 6,
    cityhall = 7,
    statueOfLiberty = 8,
    shop = 9,
    park = 10,
    wheel = 11,
    eiffle = 12,
    pisaTower = 13,
    policeStation = 14,
    fireStation = 15,
    diner = 16
};

public class InventoryBuilding : MonoBehaviour
{

    public static InventoryBuilding inventory;

    public List<StructureBlueprint> structures;

    public StructureBlueprint windmillStructure;
    public StructureBlueprint solarPanelStructure;

    public StructureBlueprint coalFactoryStructure;
    public StructureBlueprint gasExtractorStructure;

    public StructureBlueprint statueOfLiberty;


    public StructureBlueprint riverPart;
    public StructureBlueprint shop;

    public StructureBlueprint park;

    public StructureBlueprint ferrisWheel;
    public StructureBlueprint eiffleTower;
    public StructureBlueprint pisaTower;

    public StructureBlueprint policeStation;
    public StructureBlueprint fireStation;

    public StructureBlueprint diner;

    //List<StructureBlueprint> listOfBlueprints = new List<StructureBlueprint>();



    void Awake()
    {


        if (inventory == null)
        {
            inventory = this;
        }
        else if (inventory != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
       
        /*
        windmillStructure.id = idBuildings.windmill;
        solarPanelStructure.id = idBuildings.solarpanel; 

        coalFactoryStructure.id = idBuildings.coalfactory; 
        gasExtractorStructure.id = idBuildings.gasextractor;


        statueOfLiberty.id = idBuildings.statueOfLiberty;
        shop.id = idBuildings.shop;

        riverPart.id = idBuildings.river;

        park.id = idBuildings.park;

        ferrisWheel.id = idBuildings.wheel;
        eiffleTower.id = idBuildings.eiffle;
        pisaTower.id = idBuildings.pisaTower;

        */

        //listOfBlueprints.Add(windmillStructure);
    }

  /*  public void SelectShowStatueOfLiberty()
    {
        buildManager.HideConstructionPanel();

        icon.sprite = statueOfLiberty.icon;
        titleText.text = statueOfLiberty.title;
        descriptionText.text = statueOfLiberty.description;
        moneyText.text = statueOfLiberty.moneyPerTap / 1000 + "K";
        happinessText.text = "+" + statueOfLiberty.amountOfHappiness;
        energyText.text = "+" + statueOfLiberty.amountOfEnergy;
        pollutionText.text = "+" + statueOfLiberty.amountOfPollution;
        costText.text = statueOfLiberty.cost.ToString();

        //windmillStructure.informationPanel.SetActive(true);
        infoPanel.SetActive(true);

        structureBuy = statueOfLiberty;
    }*/
    /*public void SelectBuilding(string s)
    {
        switch (s)
        {
            case "solar":
                ShowInfo(solarPanelStructure);
                break;
            case "wind":
                ShowInfo(windmillStructure);
                break;

            case "coal":
                ShowInfo(coalFactoryStructure);
                break;

            case "gas":
                ShowInfo(gasExtractorStructure);
                break;

            case "liberty":
                ShowInfo(statueOfLiberty);
                break;
            case "shop":
                ShowInfo(shop);
                break;
            default:
                break;

        }
       
    }*/

   /* public void ShowInfo(StructureBlueprint structure)
    {
        buildManager.HideConstructionPanel();

        icon.sprite = structure.icon;
        titleText.text = structure.title;
        descriptionText.text = structure.description;
        moneyText.text = structure.moneyPerTap / 1000 + "K";
        happinessText.text = "+" + structure.amountOfHappiness;
        energyText.text = "+" + structure.amountOfEnergy;
        pollutionText.text = "+" + structure.amountOfPollution;
        costText.text = structure.cost.ToString();

        //windmillStructure.informationPanel.SetActive(true);
        infoPanel.SetActive(true);

        structureBuy = structure;
    }
    */
    /*
    public void SelectShowInfoWindmill()
    {
        buildManager.HideConstructionPanel();

        icon.sprite = windmillStructure.icon;
        titleText.text = windmillStructure.title;
        descriptionText.text = windmillStructure.description;
        moneyText.text = windmillStructure.moneyPerTap / 1000 + "K";
        happinessText.text = "+" + windmillStructure.amountOfHappiness;
        energyText.text = "+" + windmillStructure.amountOfEnergy;
        pollutionText.text = "+" + windmillStructure.amountOfPollution;
        costText.text = windmillStructure.cost.ToString();

        //windmillStructure.informationPanel.SetActive(true);
        infoPanel.SetActive(true);

        structureBuy = windmillStructure;
    }
    /*public void HideInfoWindmill()
    {
        windmillStructure.informationPanel.SetActive(false);

    }
    public void SelectShowInfoCoalFactory()
    {
        buildManager.HideConstructionPanel();

        icon.sprite = coalFactoryStructure.icon;
        titleText.text = coalFactoryStructure.title;
        descriptionText.text = coalFactoryStructure.description;
        moneyText.text = coalFactoryStructure.moneyPerTap / 1000 + "K";
        happinessText.text = "+" + coalFactoryStructure.amountOfHappiness;
        energyText.text = "+" + coalFactoryStructure.amountOfEnergy;
        pollutionText.text = "+" + coalFactoryStructure.amountOfPollution;
        costText.text = coalFactoryStructure.cost.ToString();

       // coalFactoryStructure.informationPanel.SetActive(true);

        infoPanel.SetActive(true);

        structureBuy = coalFactoryStructure;

    }

    public void SelectShowInfoSolarpanel()
    {
        buildManager.HideConstructionPanel();


        icon.sprite = solarPanelStructure.icon;
        titleText.text = solarPanelStructure.title;
        descriptionText.text = solarPanelStructure.description;
        moneyText.text = solarPanelStructure.moneyPerTap / 1000 + "K";
        happinessText.text = "+" + solarPanelStructure.amountOfHappiness;
        energyText.text = "+" + solarPanelStructure.amountOfEnergy;
        pollutionText.text = "+" + solarPanelStructure.amountOfPollution;
        costText.text = solarPanelStructure.cost.ToString();

        infoPanel.SetActive(true);

        structureBuy = solarPanelStructure;

        //solarPanelStructure.informationPanel.SetActive(true);
    }
    public void SelectShowInfoGas()
    {
        buildManager.HideConstructionPanel();



        icon.sprite = gasExtractorStructure.icon;
        titleText.text = gasExtractorStructure.title;
        descriptionText.text = gasExtractorStructure.description;
        moneyText.text = gasExtractorStructure.moneyPerTap / 1000 + "K";
        happinessText.text = "+" + gasExtractorStructure.amountOfHappiness;
        energyText.text = "+" + gasExtractorStructure.amountOfEnergy;
        pollutionText.text = "+" + gasExtractorStructure.amountOfPollution;
        costText.text = gasExtractorStructure.cost.ToString();

        infoPanel.SetActive(true);

        structureBuy = gasExtractorStructure;

       // gasExtractorStructure.informationPanel.SetActive(true);
    }
    public void SelectStatueOfLiberty()
    {
        buildManager.SelectStructureToBuild(gasExtractorStructure);
        Debug.Log("Selected monument statue of liberty");
    }

    public void SelectGasExtractorStructure()
    {
        buildManager.SelectStructureToBuild(gasExtractorStructure);
        Debug.Log("Selected gas extractor");
    }

    public void SelectWindmillStructure()
    {
        buildManager.SelectStructureToBuild(windmillStructure);
        Debug.Log("Selected windmill");
    }

    public void SelectSolarPanelStructure()
    {
        buildManager.SelectStructureToBuild(solarPanelStructure);
        Debug.Log("Selected solar panel");
    }

    public void SelectCoalFactoryStructure()
    {
        buildManager.SelectStructureToBuild(coalFactoryStructure);
        Debug.Log("Selected coal factory");
    }

    public void SelectRiverStructure()
    {
        buildManager.SelectStructureToBuild(riverPart);
        Debug.Log("Selected river");
    }
    */

   /* public void ShowInfoRiver()
    {
        buildManager.HideConstructionPanel();
      //  riverPart.informationPanel.SetActive(true);
    }*/


    // Update is called once per frame
    void Update()
    {
        
    }
}
