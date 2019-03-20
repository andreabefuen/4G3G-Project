using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;


    private StructureBlueprint structureToBuild;
    private NodeTouch selectedNode;

    public NodeUI nodeUI;

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
            DeselectNode();
            return;
        }
        selectedNode = node;
        structureToBuild = null;
        nodeUI.SetTarget(node);
    }

    public StructureBlueprint GetStructureToBuild()
    {
        return structureToBuild;
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }


    public void SelectStructureToBuild (StructureBlueprint structure)
    {
        structureToBuild = structure;

        DeselectNode();

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
