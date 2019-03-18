using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBuilding : MonoBehaviour
{
    public StructureBlueprint windmillStructure;

    List<StructureBlueprint> listOfBlueprints = new List<StructureBlueprint>();



    BuildManager buildManager;

    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;

        listOfBlueprints.Add(windmillStructure);
    }

    public void SelectWindmillStructure()
    {
        buildManager.SelectStructureToBuild(windmillStructure);
        Debug.Log("Selected windmill");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
