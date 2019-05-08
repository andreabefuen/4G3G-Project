using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBuilding : MonoBehaviour
{

    public enum idBuildings
    {
        windmill = 1,
        solarpanel = 2,
        coalfactory = 3,
        gasextractor = 4,
        houses = 5,
        river = 6
    };


    public StructureBlueprint windmillStructure;
    public StructureBlueprint solarPanelStructure;

    public StructureBlueprint coalFactoryStructure;
    public StructureBlueprint gasExtractorStructure;


    public StructureBlueprint riverPart;

    List<StructureBlueprint> listOfBlueprints = new List<StructureBlueprint>();



    BuildManager buildManager;

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
        windmillStructure.informationPanel.SetActive(true);
    }
    public void HideInfoWindmill()
    {
        windmillStructure.informationPanel.SetActive(false);

    }
    public void SelectShowInfoCoalFactory()
    {
        buildManager.HideConstructionPanel();
        coalFactoryStructure.informationPanel.SetActive(true);
    }
    public void HideInfoCoalFactory()
    {
        coalFactoryStructure.informationPanel.SetActive(false);
    }

    public void SelectShowInfoSolarpanel()
    {
        buildManager.HideConstructionPanel();

        solarPanelStructure.informationPanel.SetActive(true);
    }
    public void HideInfoSolarpanel()
    {
        solarPanelStructure.informationPanel.SetActive(false);

    }

    public void SelectShowInfoGas()
    {
        buildManager.HideConstructionPanel();

        gasExtractorStructure.informationPanel.SetActive(true);
    }
    public void HideInfoGas()
    {
        gasExtractorStructure.informationPanel.SetActive(false);
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

    public void HideInfoRiver()
    {
        riverPart.informationPanel.SetActive(false);
    }

    public void ShowInfoRiver()
    {
        buildManager.HideConstructionPanel();
        riverPart.informationPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
