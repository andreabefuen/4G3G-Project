using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeTouch : MonoBehaviour
{
    public Node nodeInfo;

    public bool isUnlock = false;

    public bool haveWater = false;

    public Color hoverColor;
    public Color normalColor;

    [Header("Optional")]
    public GameObject buildingThere;


    Vector3 offset;

    Renderer rend;
    Color startColor;


    BuildManager buildManager;

    Vector3 touchPosWorld;



    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        startColor = rend.material.color;


        buildManager = BuildManager.instance;

        offset = new Vector3(0f, 0, 0f);

        //buildingThere = null;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + offset;
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void OnMouseDown()
    {

      // if(isUnlock == false)
      // {
      //     //buildManager.DeselectNode();
      //     return;
      // }
       //if (isUnlock == false)
       //{
       //    if (!EventSystem.current.IsPointerOverGameObject())
       //    {
       //        buildManager.HideConstructionPanel();
       //        return;
       //    }
       //        
       //    return;       
       //}

        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetKeyDown(KeyCode.Mouse0))

        {
            if (!IsPointerOverUIObject())
            {

                if (buildManager.haveCityHall == false)
                {
                    buildManager.SelectNode(this);
                    return;
                }

                if (buildingThere == null && this.isUnlock == true)
                {
                    Debug.Log("Selected a free node");
                    buildManager.SelectNode(this);
                    return;
                }

                if (buildingThere != null && buildManager.destroyActivate == false)
                {
                    Debug.Log("Can't build there!");
                    //Show the info panel of the building
                    buildManager.ShowInfoPanelBuildings();
                    //buildManager.SelectNode(this);
                    //buildManager.DeselectNode();
                    return;
                }

                if (buildingThere != null && buildManager.destroyActivate)
                {
                    Debug.Log("Demolish!!!");
                    buildManager.SelectNode(this);
                }

                if (buildManager.GetStructureToBuild() == null)
                {
                    Debug.Log("Nothing to construct, buy something");
                    buildManager.DeselectNode();
                    return;
                }

         


                // buildManager.BuildStructureOn(this);
            }
        }

       
        
    }

    private void OnMouseEnter()
    {
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return;
        //}

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                rend.material.color = hoverColor;
               
            }
        }
                
    }


    private void OnMouseExit()
    {
        rend.material.color = startColor ;
    }
    // Update is called once per frameç


    void Update()
    {
        /*
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {   
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    Debug.Log("TOUCH Gameobject");
                    if (buildManager.haveCityHall == false)
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
                        buildManager.SelectNode(this);
                        //buildManager.DeselectNode();
                        return;
                    }


                    if (buildManager.GetStructureToBuild() == null)
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
            }

       }*/
    }
    
}
