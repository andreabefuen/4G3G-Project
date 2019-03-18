using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;


    private StructureBlueprint structureToBuild;
    private NodeTouch selectedNode;


    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than a one buildmanager in the scene");
            return;
        }
        instance = this;
    }

    public void BuildStructureOn(NodeTouch node)
    {
        GameObject structure = (GameObject)Instantiate(structureToBuild.prefab, node.GetBuildPosition(), structureToBuild.prefab.transform.rotation);

        node.buildingThere = structure;
        structureToBuild.nodeAsociate = node;

        Debug.Log("CONSTRUIDO");
        //Hide the node
        node.gameObject.GetComponent<MeshRenderer>().enabled = false;

    }

    public void SelectNode(NodeTouch node)
    {
        if(selectedNode == node)
        {
            node.gameObject.GetComponent<MeshRenderer>().enabled = false;
            selectedNode = null;
            return;
        }
        selectedNode = node;
        structureToBuild = null;
    }

    public StructureBlueprint GetStructureToBuild()
    {
        return structureToBuild;
    }

    public void SelectStructureToBuild (StructureBlueprint structure)
    {
        structureToBuild = structure;
        


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
