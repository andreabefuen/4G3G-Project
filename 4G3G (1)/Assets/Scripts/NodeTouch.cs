using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeTouch : MonoBehaviour
{

    public Color hoverColor;
    public Color normalColor;

    [Header("Optional")]
    public GameObject buildingThere;


    Renderer rend;
    Color startColor;


    BuildManager buildManager;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        startColor = rend.material.color;


        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position;
    }


    private void OnMouseDown()
    {
       // if (EventSystem.current.IsPointerOverGameObject())
       // {
       //     return;
       // }

        if(buildingThere != null)
        {
            Debug.Log("Can't build there!");
        }


        buildManager.BuildStructureOn(this);
        
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
