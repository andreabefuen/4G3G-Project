using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;


    public GameObject Grid;


    [Header("Panels and UI")]
    public GameObject cityHall;
    public GameObject uiAllQuests;
    public GameObject buildPanel;

    public GameObject infoPanel;
    public GameObject destroyPanelSelection;

    public bool destroyActivate = false;

    private StructureBlueprint structureToBuild;
    private NodeTouch selectedNode;

    public NodeUI nodeUI;

    public bool haveCityHall;

    private Player player;
    private InventoryBuilding inventoryBuilding;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than a one buildmanager in the scene");
            return;
        }
        instance = this;

        player = GameObject.Find("Player").GetComponent<Player>();
        inventoryBuilding = GameObject.Find("TypesOfBuildings").GetComponent<InventoryBuilding>();
    }

    public void BuildStructureOn(NodeTouch node)
    {
        if(structureToBuild == null)
        {
            return;
        }
        if (!HasEnoughMoney() && GameControl.control.loaded == false)
        {
            Debug.Log("Not enough money!");
            player.NotEnoughMoneyPlay();
            return;
        }
        if(node.haveWater && !structureToBuild.needWater)
        {
            Debug.Log("The structure can't build in water");
            return;
        }

        player.DecreaseMoney(structureToBuild.cost);
        player.IncreaseHappiness(structureToBuild.amountOfHappiness);
        player.IncreasePollution(structureToBuild.amountOfPollution);
        player.IncreaseEnergy(structureToBuild.amountOfEnergy);

        GameObject structure = (GameObject)Instantiate(structureToBuild.prefab, node.GetBuildPosition(), structureToBuild.prefab.transform.rotation);
        
        node.nodeInfo.idBuilding = structureToBuild.id;

        
       

        Debug.Log("CONSTRUIDO");
        //Hide the node
        if (!structureToBuild.isWater)
        {
            node.gameObject.GetComponent<MeshRenderer>().enabled = false;
            node.buildingThere = structure;
        }
        else
        {
            node.haveWater = true;
        }
        structureToBuild.nodeAsociate = node;
        structureToBuild = null;

    }

    bool HasEnoughMoney()
    {
        return structureToBuild.cost <= player.GetTotalMoney();
    }
    public void BuildCityHall(NodeTouch node)
    {
        GameObject structure = Instantiate(cityHall, node.GetBuildPosition(), cityHall.transform.rotation);

        node.buildingThere = structure;

        

        Debug.Log("City hall did it");
        node.gameObject.GetComponent<MeshRenderer>().enabled = false;

        player.IncreaseMoney(35000);
        node.nodeInfo.idBuilding = (int) InventoryBuilding.idBuildings.cityhall;

        structureToBuild = null;
        haveCityHall = true;
        DeselectNode();
    }
    public void HideUIQuest()
    {
        uiAllQuests.SetActive(false);
    }
    public void SelectNode(NodeTouch node)
    {
        if (node.tag == "CityPlace" && haveCityHall == false)
        {
            BuildCityHall(node);
            
            
            return;
        }
        if (haveCityHall)
        {
               if (selectedNode == node )
               {
                   //node.gameObject.GetComponent<MeshRenderer>().enabled = false;
                   DeselectNode();
                   return;
               }
                if(node.isUnlock == false)
                {
                     DeselectNode();
                     return;
                }

            /*
                if (structureToBuild == null && node.buildingThere == null)
                {
                    buildPanel.SetActive(true);
                    HideInfoPanel();
                    selectedNode = node;
                    return;
                }

            */
                if (structureToBuild != null && node.buildingThere == null)
                {
                    BuildStructureOn(node);
                    //buildPanel.SetActive(false);
                    DeselectNode();
                    structureToBuild = null;
                    return;
                }
                //If in the node are something
                if(node.buildingThere != null && node.buildingThere.tag != "House" && node.buildingThere.tag != "CityHall")
                {
                    selectedNode = node;
                    infoPanel.SetActive(true);
                    if (destroyActivate){
                    //destroyPanelSelection.SetActive(true);
                         DestroyThisBuilding();
                    }
                    //HideConstructionPanel();
                    HideEverything();
                    return;
                }

               //if (node.buildingThere.tag == "Factory")
               //{
               //    DeselectNode();
               //    selectedNode = node;
               //    structureToBuild = null;
               //    nodeUI.SetTargetReplaceFactory(node);
               //    return;
               //}
                if (node.buildingThere.tag == "CityHall")
                {
                    Debug.Log("Menu city hall");
                    DeselectNode();
                    uiAllQuests.SetActive(true);
                    return;
                }
               //else //House
               //{
               //    DeselectNode();
               //    Debug.Log("Is not a factory");
               //    selectedNode = node;
               //    structureToBuild = null;
               //    //nodeUI.SetTargetHouses(node);
               //}
            }
            
           
       
       
    }
    public void SelectThisBuilding( int id)
    {
        if(id == (int)InventoryBuilding.idBuildings.coalfactory)
        {
            structureToBuild = inventoryBuilding.coalFactoryStructure;

        }
        else if (id == (int)InventoryBuilding.idBuildings.windmill)
        {
            structureToBuild = inventoryBuilding.windmillStructure;
    
        }
        else if (id == (int)InventoryBuilding.idBuildings.gasextractor)
        {
            structureToBuild = inventoryBuilding.gasExtractorStructure;
  
        }
        else if (id == (int)InventoryBuilding.idBuildings.solarpanel)
        {
            structureToBuild = inventoryBuilding.solarPanelStructure;

        }
        else if (id == (int)InventoryBuilding.idBuildings.river)
        {
            structureToBuild = inventoryBuilding.riverPart;

        }

    }

    public void HideEverything()
    {
        infoPanel.SetActive(false);
        destroyPanelSelection.SetActive(false);
        buildPanel.SetActive(false);
        uiAllQuests.SetActive(false);
        inventoryBuilding.HideInfoPanel(); 
        
    }
    
    public void BuildEnergyButton()
    {
     
        //HideInfoPanel();
        HideEverything();
        buildPanel.SetActive(true);
        Grid.SetActive(true);
    }

    public void DestroyButton()
    {
        HideEverything();
        if (destroyActivate)
        {
            destroyActivate = false;
            destroyPanelSelection.SetActive(false);
            Grid.SetActive(false);
        }
        else
        {
            destroyActivate = true;
            destroyPanelSelection.SetActive(true);
            Grid.SetActive(true);
            HideInfoPanel();
        }
        
        


    }

    public void DestroyThisBuilding()
    {
        if(selectedNode != null)
        {

            if(selectedNode.buildingThere.tag == "Factory")
            {
                if(player.GetTotalMoney() < inventoryBuilding.coalFactoryStructure.costOfDemolition)
                {
                    Debug.Log("You can't demolish this building");
                    //HideInfoPanel();
                    HideEverything();
                    NotDestroyThisBuilding();
                    player.NotEnoughMoneyPlay();
                    return;
                }
                Debug.Log("Demolish factory");
                player.IncreaseEnergy(-inventoryBuilding.coalFactoryStructure.amountOfEnergy);
                player.DecreaseMoney(inventoryBuilding.coalFactoryStructure.costOfDemolition);
               // player.IncreasePollution(-inventoryBuilding.coalFactoryStructure.amountOfPollution);
            }
            else if(selectedNode.buildingThere.tag == "Windmill")
            {
                if (player.GetTotalMoney() < inventoryBuilding.windmillStructure.costOfDemolition)
                {
                    Debug.Log("You can't demolish this building");
                    //HideInfoPanel();
                    HideEverything();
                    NotDestroyThisBuilding();
                    return;

                }
                player.IncreaseEnergy(-inventoryBuilding.windmillStructure.amountOfEnergy);
                player.DecreaseMoney(inventoryBuilding.windmillStructure.costOfDemolition);

                // player.IncreasePollution(-inventoryBuilding.windmillStructure.amountOfPollution);

            }
            else if(selectedNode.buildingThere.tag == "Solar")
            {
                if (player.GetTotalMoney() < inventoryBuilding.solarPanelStructure.costOfDemolition)
                {
                    Debug.Log("You can't demolish this building");
                    //HideInfoPanel();
                    HideEverything();
                    NotDestroyThisBuilding();
                    return;

                }
                player.IncreaseEnergy(-inventoryBuilding.solarPanelStructure.amountOfEnergy);
                player.DecreaseMoney(inventoryBuilding.solarPanelStructure.costOfDemolition);

                //   player.IncreasePollution(-inventoryBuilding.solarPanelStructure.amountOfPollution);

            }
            else if(selectedNode.buildingThere.tag == "Gas")
            {
                if (player.GetTotalMoney() < inventoryBuilding.gasExtractorStructure.costOfDemolition)
                {
                    Debug.Log("You can't demolish this building");
                    //HideInfoPanel();
                    HideEverything();
                    NotDestroyThisBuilding();
                    return;

                }
                player.IncreaseEnergy(-inventoryBuilding.gasExtractorStructure.amountOfEnergy);
                player.DecreaseMoney(inventoryBuilding.gasExtractorStructure.costOfDemolition);

                //  player.IncreasePollution(-inventoryBuilding.gasExtractorStructure.amountOfPollution);

            }
            Destroy(selectedNode.buildingThere);
            selectedNode.gameObject.GetComponent<MeshRenderer>().enabled = true;
            // player.UpdateEnergy();
            //player.UpdateMoney();
            //HideInfoPanel();
            HideEverything();
            NotDestroyThisBuilding();
        }
        
    }

    public void NotDestroyThisBuilding()
    {
        destroyPanelSelection.SetActive(false);
        destroyActivate = false;
    }

    public StructureBlueprint GetStructureToBuild()
    {
        return structureToBuild;
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.HideReplaceFactory();
        nodeUI.HideHouses();
        HideInfoPanel();
        HideConstructionPanel();
        Grid.SetActive(false);
    }

    public void HideInfoPanel()
    {
        infoPanel.SetActive(false);

    }

    public void HideConstructionPanel()
    {
        buildPanel.SetActive(false);
    }

    public bool ConstructionPanelActivated()
    {
        return buildPanel.activeSelf;
    }


    public void SelectStructureToBuild (StructureBlueprint structure)
    {
        structureToBuild = structure;
        
        DeselectNode();
        Grid.SetActive(true);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
