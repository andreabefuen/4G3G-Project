using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;


    public GameObject Grid;


    [Header("Panels and UI")]
    public GameObject cityHall;
    public int startingMoney;
    public GameObject uiAllQuests;
    public GameObject buildPanel;

    public GameObject infoPanel;
    public GameObject destroyPanelSelection;

    public bool destroyActivate = false;

    public GameObject infoBuildingPanel;

  


    private StructureBlueprint structureToBuild;
    private NodeTouch selectedNode;

    public NodeUI nodeUI;

    public bool haveCityHall;

    private Player player;
    private InventoryBuilding inventoryBuilding;
    private CreateEnvironment createEnvironment;

    [Header("Particles")]
    private ParticleSystem destroyParticle;
    private ParticleSystem buildParticle;
    public GameObject destroyParticlePrefab;
    public GameObject buildParticlePrefab;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than a one buildmanager in the scene");
            return;
        }
        instance = this;

        player = GameObject.Find("Player").GetComponent<Player>();
        createEnvironment = this.gameObject.GetComponent<CreateEnvironment>();
        inventoryBuilding = GameObject.Find("TypesOfBuildings").GetComponent<InventoryBuilding>();


        //destroyParticle = destroyParticlePrefab.GetComponentInChildren<ParticleSystem>();
    }

    public void RebuildStructures(NodeTouch node)
    {
        if(structureToBuild == null)
        {
            return;
        }

        GameObject structure = (GameObject)Instantiate(structureToBuild.prefab, node.GetBuildPosition(), structureToBuild.prefab.transform.rotation);

        node.nodeInfo.idBuilding = (int)structureToBuild.id;


        node.structureThere = structureToBuild;

        Debug.Log("CONSTRUIDO");
        //SoundManager.soundManager.PlayConstruction();
        //Hide the node
        node.gameObject.GetComponent<MeshRenderer>().enabled = false;
        if (!structureToBuild.isWater)
        {

            node.buildingThere = structure;
        }
        else
        {
            node.haveWater = true;
        }
        structureToBuild.nodeAsociate = node;
        structureToBuild = null;


       //GameObject aux = Instantiate(buildParticlePrefab, node.transform.position, Quaternion.identity);
       //buildParticle = aux.GetComponentInChildren<ParticleSystem>();
       //buildParticle.Play();
    }

    public void BuildStructureOn(NodeTouch node)
    {
        if(structureToBuild == null)
        {
            return;
        }
        if (!HasEnoughMoney() )
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

        if(player.activeQuest.Count != 0)
        {
            player.activeQuest[0].goal.BuildGoal(structureToBuild);

        }



        GameObject structure = (GameObject)Instantiate(structureToBuild.prefab, node.GetBuildPosition(), structureToBuild.prefab.transform.rotation);
        
        node.nodeInfo.idBuilding = (int) structureToBuild.id;


        node.structureThere = structureToBuild;

        Debug.Log("CONSTRUIDO");
        SoundManager.soundManager.PlayConstruction();
        //Hide the node
        node.gameObject.GetComponent<MeshRenderer>().enabled = false;
        if (!structureToBuild.isWater)
        {
           
            node.buildingThere = structure;
        }
        else
        {
            node.haveWater = true;
        }
        structureToBuild.nodeAsociate = node;

        if (structureToBuild.id == idBuildings.diner)
        {
            if (SceneManager.GetActiveScene().name == "CoalIsland")
            {
                inventoryBuilding.coalFactoryStructure.timeMoney *= 0.75f;
            }
        }

        structureToBuild = null;

       
        GameObject aux = Instantiate(buildParticlePrefab, node.transform.position, Quaternion.identity);
        buildParticle = aux.GetComponentInChildren<ParticleSystem>();
        buildParticle.Play();

  

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

        if (GameControl.control.tutorial == true)
            player.IncreaseMoney(startingMoney);
        node.nodeInfo.idBuilding = (int) idBuildings.cityhall;

        structureToBuild = null;
        haveCityHall = true;
        DeselectNode();
        if (GameControl.control.tutorial && SceneManager.GetActiveScene().buildIndex == 1)
        {
            GameObject.Find("TutorialArrows").GetComponent<Tutorial>().FirtsIndicator();
        }
        /*GameObject tut = GameObject.Find("TutorialArrows");
        if(tut != null)
        {
            tut.GetComponent<Tutorial>().FirtsIndicator();
        }*/

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
                if(node.buildingThere == null)
                {
                    DeselectNode();
                    return;
                }

                //If in the node are something
                if(node.buildingThere != null && node.buildingThere.tag != "House" && node.buildingThere.tag != "CityHall" && node.buildingThere.tag != "Monuments")
                {
                    selectedNode = node;
                //infoPanel.SetActive(true);
                    if (destroyActivate){
                    //destroyPanelSelection.SetActive(true);
                         DestroyThisBuilding();
                        HideEverything();
                        return;
                    }
                    //HideConstructionPanel();
                    HideEverything();
                    ShowInfoPanelBuildings();

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

        if(id == (int)idBuildings.coalfactory)
        {
            structureToBuild = inventoryBuilding.coalFactoryStructure;

        }
        else if (id == (int)idBuildings.windmill)
        {
            structureToBuild = inventoryBuilding.windmillStructure;
    
        }
        else if (id == (int)idBuildings.gasextractor)
        {
            structureToBuild = inventoryBuilding.gasExtractorStructure;
  
        }
        else if (id == (int)idBuildings.solarpanel)
        {
            structureToBuild = inventoryBuilding.solarPanelStructure;

        }
        else if (id == (int)idBuildings.river)
        {
            structureToBuild = inventoryBuilding.riverPart;

        }
        else if(id == (int)idBuildings.statueOfLiberty)
        {
            structureToBuild = inventoryBuilding.statueOfLiberty;
        }
        else if( id == (int)idBuildings.shop)
        {
            structureToBuild = inventoryBuilding.shop;
        }
        else if(id == (int)idBuildings.park)
        {
            structureToBuild = inventoryBuilding.park;
        }
        else if(id == (int)idBuildings.eiffle)
        {
            structureToBuild = inventoryBuilding.eiffleTower;
        }
        else if(id == (int)idBuildings.wheel)
        {
            structureToBuild = inventoryBuilding.ferrisWheel;
        }
        else if(id == (int)idBuildings.pisaTower)
        {
            structureToBuild = inventoryBuilding.pisaTower;
        }

    }

    public void HideEverything()
    {
        infoPanel.SetActive(false);
        destroyPanelSelection.SetActive(false);
        if (buildPanel.activeSelf)
        {
            buildPanel.SetActive(false);
        }
        uiAllQuests.SetActive(false);
        UIManager.instance.HideInfoPanel();
        HideInfoPanelBuildings();
        
    }
    
    public void BuildEnergyButton()
    {

        //HideInfoPanel();
        if (buildPanel.activeSelf)
        {
            buildPanel.SetActive(!buildPanel.activeSelf);
            HideEverything();


        }
        else
        {
            HideEverything();
            buildPanel.SetActive(true);
        }

        //.SetActive(true);
    }

    public void DestroyButton()
    {
        HideEverything();
        if (destroyActivate)
        {
            destroyActivate = false;
            destroyPanelSelection.SetActive(false);
            createEnvironment.HideRenderNodes();
            //Grid.SetActive(false);
        }
        else
        {
            destroyActivate = true;
            destroyPanelSelection.SetActive(true);
            createEnvironment.ShowRenderNodes();
            //Grid.SetActive(true);
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

            else if(selectedNode.buildingThere.tag == "Park")
            {
                if (player.GetTotalMoney() < inventoryBuilding.park.costOfDemolition)
                {
                    Debug.Log("You can't demolish this building");
                    //HideInfoPanel();
                    HideEverything();
                    NotDestroyThisBuilding();
                    return;

                }
                player.IncreaseEnergy(-inventoryBuilding.park.amountOfEnergy);
                player.DecreaseMoney(inventoryBuilding.park.costOfDemolition);

            }
            selectedNode.nodeInfo.idBuilding = 0;
            selectedNode.structureThere = null;
            Destroy(selectedNode.buildingThere);
            selectedNode.gameObject.GetComponent<MeshRenderer>().enabled = true;
            // player.UpdateEnergy();
            //player.UpdateMoney();
            //HideInfoPanel();
            HideEverything();
            Debug.Log("DEMOLISH PARTICLE ASDASFDASAF");
            SoundManager.soundManager.PlayDemolish();
            GameObject aux= Instantiate(destroyParticlePrefab, selectedNode.transform.position, Quaternion.identity);
            destroyParticle = aux.GetComponentInChildren<ParticleSystem>();
            destroyParticle.Play();
            NotDestroyThisBuilding();
            Destroy(aux, 2f);
            
        }
        
    }

    public void HideRendererNodes()
    {
        
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
        HideInfoPanelBuildings();
        //Grid.SetActive(false);
        createEnvironment.HideRenderNodes();
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
        createEnvironment.ShowRenderNodes();
       // Grid.SetActive(true);

    }

    public void ShowInfoPanelBuildings()
    {
        HideEverything();
        infoBuildingPanel.SetActive(true);
        UIManager.instance.UpdateLevel(selectedNode.structureThere);
        //inventoryBuilding.UpdateLevel(selectedNode.structureThere);
    }

    public void BuyUpgradeBuilding()
    {
        if(selectedNode.structureThere.levelBuilding >= 4)
        {
            //Completed
            return;
        }
        if(player.totalCurrency >= selectedNode.structureThere.costUpgrades[selectedNode.structureThere.levelBuilding-1] )
        {
            player.DecreaseMoney(selectedNode.structureThere.costUpgrades[selectedNode.structureThere.levelBuilding-1]);
            selectedNode.structureThere.levelBuilding++;
            UIManager.instance.UpdateLevel(selectedNode.structureThere);
            selectedNode.structureThere.moneyPerTap += 500;

        }
        else
        {
            player.NotEnoughMoneyPlay();
        }
    }

    public void InfoBuildButton()
    {
        infoBuildingPanel.SetActive(false);
        UIManager.instance.ShowInfoWithOutBuyButton(selectedNode.structureThere);
    }

    public void HideInfoPanelBuildings()
    {
        infoBuildingPanel.SetActive(false);
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
