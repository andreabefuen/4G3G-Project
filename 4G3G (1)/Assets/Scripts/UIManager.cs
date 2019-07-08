using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject infoPanel;
    public Image icon;
    public TextMeshProUGUI titleText;
    public Text descriptionText;
    public TextMeshProUGUI moneyText, happinessText, energyText, pollutionText;
    public Text costText;

    [Header("Level of building")]
    public TextMeshProUGUI textLevel;
    public TextMeshProUGUI textPriceUpdate;


    public GameObject loadScreenObject;


    [Header("New Level Reach")]
    public GameObject newLevelWindow;
    public TextMeshProUGUI levelRewardText;

    public Button motherlodeButton;

    BuildManager buildManager;

    StructureBlueprint structureBuy;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than a one buildmanager in the scene");
            return;
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;

        SetActiveCheatsMode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectBuilding(string s)
    {
        switch (s)
        {
            case "solar":
                ShowInfo(InventoryBuilding.inventory.solarPanelStructure);
                break;
            case "wind":
                ShowInfo(InventoryBuilding.inventory.windmillStructure);
                break;

            case "coal":
                ShowInfo(InventoryBuilding.inventory.coalFactoryStructure);
                break;

            case "gas":
                ShowInfo(InventoryBuilding.inventory.gasExtractorStructure);
                break;

            case "liberty":
                ShowInfo(InventoryBuilding.inventory.statueOfLiberty);
                break;
            case "shop":
                ShowInfo(InventoryBuilding.inventory.shop);
                break;
            case "park":
                ShowInfo(InventoryBuilding.inventory.park);
                break;
            case "eiffle":
                ShowInfo(InventoryBuilding.inventory.eiffleTower);
                break;
            case "wheel":
                ShowInfo(InventoryBuilding.inventory.ferrisWheel);
                break;
            case "pisa":
                ShowInfo(InventoryBuilding.inventory.pisaTower);
                break;
            case "fire":
                ShowInfo(InventoryBuilding.inventory.fireStation);
                break;
            case "police":
                ShowInfo(InventoryBuilding.inventory.policeStation);
                break;
            case "diner":
                ShowInfo(InventoryBuilding.inventory.diner);
                break;
            default:
                break;

        }

    }

     public void LoadScreenAnim()
    {
        loadScreenObject.SetActive(true);
    }

    public void ShowInfo(StructureBlueprint structure)
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

        Debug.Log(" ASFOIJASDJGDSAKBJHODSAJGJODSJSDAOJDSOJFDSAJOSOJO ");

        infoPanel.SetActive(true);

        infoPanel.transform.Find("BuyButton").gameObject.SetActive(true);

        structureBuy = structure;
    }

    public void ShowInfoWithOutBuyButton(StructureBlueprint structure)
    {
       // buildManager.HideConstructionPanel();

        icon.sprite = structure.icon;
        titleText.text = structure.title;
        descriptionText.text = structure.description;
        moneyText.text = structure.moneyPerTap / 1000 + "K";
        happinessText.text = "+" + structure.amountOfHappiness;
        energyText.text = "+" + structure.amountOfEnergy;
        pollutionText.text = "+" + structure.amountOfPollution;
        costText.text = structure.cost.ToString();

        infoPanel.SetActive(true);

        infoPanel.transform.Find("BuyButton").gameObject.SetActive(false);
    }

    public void UpdateLevel(StructureBlueprint structure)
    {
        textLevel.text = "Level: " + structure.levelBuilding;
        UpdatePriceUpdateBuilding(structure);
    }
    public void UpdatePriceUpdateBuilding(StructureBlueprint structure)
    {
        textPriceUpdate.text = structure.costUpgrades[structure.levelBuilding-1].ToString();
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
        SoundManager.soundManager.TapSound();
    }

    public void ShowNewLevelWindow()
    {
        newLevelWindow.SetActive(true);
        levelRewardText.text = "REWARD: " + Player.instance.levelCity * 500;
    }
    public void CloseNewLevelWindow()
    {
        newLevelWindow.SetActive(false);
        Player.instance.IncreaseMoney(Player.instance.levelCity * 500);
    }

    void SetActiveCheatsMode()
    {
        motherlodeButton.gameObject.SetActive(GameControl.control.cheatsOn); ;
        /*if (GameControl.control.cheatsOn)
        {
            //motherlodeButton.gameObject.SetActive(true);
            Debug.Log("Cheats On");
        }
        else
        {
            //motherlodeButton.gameObject.SetActive(true);
            Debug.Log("Cheats OFF");
        }*/
    }

    public void Motherlode()
    {
        Player.instance.IncreaseMoney(50000);
    }

}
