using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeTouch : MonoBehaviour
{
    
    public bool isUnlock = false;

    public Color hoverColor;
    public Color normalColor;

    [Header("Optional")]
    public GameObject buildingThere;


    Vector3 offset;

    Renderer rend;
    Color startColor;


    BuildManager buildManager;



    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        startColor = rend.material.color;


        buildManager = BuildManager.instance;

        offset = new Vector3(0f, 0, 0f);
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + offset;
    }


    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if(buildManager.haveCityHall == false)
        {
            buildManager.SelectNode(this);
            return;
        }
        if (buildingThere == null)
        {
            Debug.Log("Selected a free node");
            buildManager.SelectNode(this);
            return;
        }

        if (buildingThere != null)
        {
            Debug.Log("Can't build there!");
            buildManager.DeselectNode();
            return;
        }
  
        
        if(buildManager.GetStructureToBuild() == null)
        {
            Debug.Log("Nothing to construct, buy something");
            buildManager.DeselectNode();
            return;
        }
        if (this.isUnlock == false)
        {
            return;
        }

       // buildManager.BuildStructureOn(this);
        
    }

    private void OnMouseEnter()
    {
       //if (EventSystem.current.IsPointerOverGameObject())
       //{
       //    return;
       //}
        rend.material.color = hoverColor;
    }


    private void OnMouseExit()
    {
        rend.material.color = startColor ;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
