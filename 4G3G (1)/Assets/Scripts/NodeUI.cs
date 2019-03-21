using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    public GameObject uiReplaceFactory;

    public GameObject uiHouses;

    private NodeTouch target;

    private BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SetTargetReplaceFactory(NodeTouch t)
    {
        target = t;

        transform.position = target.GetBuildPosition();

        uiReplaceFactory.SetActive(true);
    }

    public void SetTargetHouses (NodeTouch t)
    {
        target = t;
        transform.position = target.GetBuildPosition();
        uiHouses.SetActive(true);
    }

    public void HideReplaceFactory()
    {
        uiReplaceFactory.SetActive(false);
    }

    public void HideHouses()
    {
        uiHouses.SetActive(false);
    }
    
    public void ReplaceButton()
    {
        GameObject aux = target.buildingThere;
        Destroy(aux);
        buildManager.BuildStructureOn(target);
    }

}
