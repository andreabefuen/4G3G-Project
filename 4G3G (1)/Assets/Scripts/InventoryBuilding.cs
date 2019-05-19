using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBuilding : MonoBehaviour
{

    public enum idBuildings
    {
        windmill = 1,
        solarpanel = 2,
        coalfactory = 3,
        gasextractor = 4,
        houses = 5,
        river = 6,
        cityhall = 7
    };


    public StructureBlueprint windmillStructure;
    public StructureBlueprint solarPanelStructure;

    public StructureBlueprint coalFactoryStructure;
    public StructureBlueprint gasExtractorStructure;


    public StructureBlueprint riverPart;

    List<StructureBlueprint> listOfBlueprints = new List<StructureBlueprint>();


    public GameObject infoPanel;
    public Image icon;
    public TextMeshProUGUI titleText;
    public Text descriptionText;
    public TextMeshProUGUI moneyText, happinessText, energyText, pollutionText;
    public Text costText;


    BuildManager buildManager;

    StructureBlueprint structureBuy;

    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;

        windmillStructure.id = (int) idBuildings.windmill;
        solarPanelStructure.id = (int)idBuildings.solarpanel; 

        coalFactoryStructure.id = (int)idBuildings.coalfactory; 
        gasExtractorStructure.id = (int)idBuildings.gasextractor; 


        riverPart.id = (int)idBuildings.river; ;

        listOfBlueprints.Add(windmillStructure);
    }

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

    }*/
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


    public void ShowInfoRiver()
    {
        buildManager.HideConstructionPanel();
      //  riverPart.informationPanel.SetActive(true);
    }

    public void HideInfoPanel()
    {
        infoPanel.SetActive(false);
        structureBuy = null;
    }

    public void BuyButton()
    {
        buildManager.SelectStructureToBuild(structureBuy);
        HideInfoPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
