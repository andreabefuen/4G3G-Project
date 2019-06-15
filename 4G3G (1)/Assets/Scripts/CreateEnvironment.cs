using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateEnvironment : MonoBehaviour
{

    
    public float offset;
    public int numStage;

    public GameObject prefabNode;

    public Node[,] matrixNodes;

    public GameObject planeLimit;
    GameObject auxPlaneLimit;


    Vector3 startPos;
    Vector3 actualPos;
    

    public Vector2 gridWorldSize;
    public float nodeRadius;
    int gridSizeX, gridSizeY;
    float nodeDiameter;

    private int rows, columns;
    private int stageRows, stageColumns;
    private GameObject grid;

    int indexCenterRow, indexCenterColumn;
    public GameObject centerNode;


    [Header("Spawn things")]
    public bool spawnThings;
    public List<GameObject> differentHouses;
    public GameObject housePrefab;
    public int numHouses;


    public static List<GameObject> houses;


    private void Awake()
    {
        
       // if(matrixNodes.Length != 0)
       // {
       //
       // }
        
        nodeDiameter = nodeRadius * 2;

        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        rows = gridSizeX;
        columns = gridSizeY;

            stageColumns = columns / numStage;
            stageRows = rows / numStage;

        

        indexCenterColumn = columns / 2;
        indexCenterRow = rows / 2;

        prefabNode.gameObject.transform.localScale = new Vector3(nodeDiameter, prefabNode.transform.localScale.y, nodeDiameter);

        grid = GameObject.Find("Grid");

        startPos = grid.transform.position;
        actualPos = startPos;

        houses = new List<GameObject>();

       
    }
    

    // Start is called before the first frame update
    void Start()
    {
        matrixNodes = new Node[rows, columns];

        for (int f = 1; f < rows; f++)
        {
            for (int c = 1; c < columns; c++)
            {
                GameObject aux = Instantiate(prefabNode);
                
                aux.transform.parent = grid.transform;
                aux.transform.position = actualPos;
                actualPos = new Vector3((offset + nodeDiameter + actualPos.x) , 0 , actualPos.z);

                matrixNodes[f, c] = new Node(aux.transform.position, f, c);
                matrixNodes[f, c].nodeGameobject = aux;
                matrixNodes[f, c].nodeGameobject.GetComponent<MeshRenderer>().enabled = false;
                aux.GetComponent<NodeTouch>().nodeInfo = matrixNodes[f, c];


            }

            actualPos = new Vector3(startPos.x, startPos.y, (offset + nodeDiameter + actualPos.z));
        }
        centerNode = matrixNodes[indexCenterRow, indexCenterColumn].nodeGameobject;
        centerNode.tag = "CityPlace";
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            BuildManager.instance.BuildCityHall(matrixNodes[indexCenterRow, indexCenterColumn].nodeGameobject.GetComponent<NodeTouch>());
        }

        //matrixNodes[0, 0].nodeGameobject.SetActive(false);


        //SpawnHouses();
        //SpawnHouseAllScenario();
        //SpawnCoalFactories();

        CreateStage();
        if (GameControl.control.loaded /*&& !GameControl.control.firstTimeCoal /*&& !GameControl.control.tutorial && !GameControl.control.firstTimeCoal*/)
        {
            stageRows = GameControl.control.stageSizeX;
            stageColumns = GameControl.control.stageSizeY;
            //CreateStage();
            Debug.Log("Reload info");
            ReloadSceneWithInfo();
            return;
           
        }
        //CreateStage();
    }

    public void ReloadSceneWithInfo()
    {
        GameControl.NodeInformation[,] aux = GameControl.control.information;
        for (int f = 1; f < rows; f++)
        {
            for (int c = 1; c < columns; c++)
            {
                matrixNodes[f, c].idBuilding = aux[f, c].idBuilding;
                //Debug.Log("ides: " + aux[f, c].idBuilding);
                if (aux[f, c].idBuilding == (int)idBuildings.houses)
                {
                    HouseInstantiate(matrixNodes[f, c]);
                    //ResetHouses(matrixNodes[f, c]);
                    
                }
                else if(aux[f, c].idBuilding == (int)idBuildings.cityhall)
                {
                    BuildManager.instance.BuildCityHall(matrixNodes[f, c].nodeGameobject.GetComponent<NodeTouch>());
                    
                }
                else
                {
                    ResetBuilding(aux[f, c].idBuilding);
                    BuildManager.instance.RebuildStructures(matrixNodes[f, c].nodeGameobject.GetComponent<NodeTouch>());
                }
                
                
            }
        }
        Player.instance.ReloadPlayerInfo();

        //Invoke("EverythingLoaded", 3f);
        GameControl.control.loaded = false;


    }

    void EverythingLoaded()
    {
        GameControl.control.loaded = false;
    }

    void ResetBuilding(int id)
    {
        if(id == 0 )
        {
            return;
        }
        else
        {
            BuildManager.instance.SelectThisBuilding(id);
            
        }
    }

    void ResetHouses(Node n)
    {
        HouseInstantiate(n);
    }

    void CreateStage()
    {


        
        Destroy(auxPlaneLimit);
        for (int i = indexCenterRow - stageRows; i < indexCenterRow + stageRows; i++)
        {
            for (int j = indexCenterColumn - stageColumns; j < indexCenterColumn + stageColumns; j++)
            {
                if(matrixNodes[i,j].nodeGameobject.GetComponent<NodeTouch>().isUnlock == true)
                {
                    continue;
                }
                matrixNodes[i, j].nodeGameobject.GetComponent<MeshRenderer>().enabled = true;
                matrixNodes[i, j].nodeGameobject.GetComponent<NodeTouch>().isUnlock = true;
               // matrixNodes[i, j].nodeGameobject.GetComponent<Renderer>().material.color = Color.green;
            }
        }
        if (spawnThings)
        {
            centerNode.GetComponent<Renderer>().material.color = Color.cyan;

        }



        Vector3 auxvect = matrixNodes[indexCenterRow - stageRows, indexCenterColumn - stageColumns].nodeGameobject.transform.position - matrixNodes[indexCenterRow + stageRows, indexCenterColumn + stageColumns].nodeGameobject.transform.position;
        float auxSize = Vector3.Magnitude(auxvect);
        auxPlaneLimit = Instantiate(planeLimit);

        if (GameControl.control.loaded && !GameControl.control.firstTimeCoal/* && !GameControl.control.tutorial && !GameControl.control.firstTimeCoal*/)
        {
            //Destroy(auxPlaneLimit);
            //auxPlaneLimit = Instantiate(planeLimit);
            auxPlaneLimit.transform.position = new Vector3(0.5f, 0.1f, -0.8f);
            
            auxPlaneLimit.transform.localScale += new Vector3(GameControl.control.sizeXPlane, 1, GameControl.control.sizeYPlane);
            return;
        }
        else
        {
            auxPlaneLimit.transform.position = new Vector3(0.5f, 0.1f, -0.8f);
            auxPlaneLimit.transform.localScale = new Vector3(auxSize / 12, 1, auxSize / 12);
        }
        

        
        
        if (spawnThings && GameControl.control.loaded == false)
        {
            numHouses += 10;
            SpawnHouses();
            return;
        }
       
        
       
       

    }

    public void HideRenderNodes()
    {
        for (int i = indexCenterRow - stageRows; i < indexCenterRow + stageRows; i++)
        {
            for (int j = indexCenterColumn - stageColumns; j < indexCenterColumn + stageColumns; j++)
            {
                if (matrixNodes[i, j].nodeGameobject.GetComponent<NodeTouch>().isUnlock == true)
                {
                    matrixNodes[i, j].nodeGameobject.GetComponent<MeshRenderer>().enabled = false;

                }
                //matrixNodes[i, j].nodeGameobject.GetComponent<NodeTouch>().isUnlock = true;
                // matrixNodes[i, j].nodeGameobject.GetComponent<Renderer>().material.color = Color.green;
            }
        }
    }

    public void ShowRenderNodes()
    {
        for (int i = indexCenterRow - stageRows; i < indexCenterRow + stageRows; i++)
        {
            for (int j = indexCenterColumn - stageColumns; j < indexCenterColumn + stageColumns; j++)
            {
                if (matrixNodes[i, j].nodeGameobject.GetComponent<NodeTouch>().buildingThere != null)
                {
                    continue;
                }
                if (matrixNodes[i, j].nodeGameobject.GetComponent<NodeTouch>().isUnlock == true)
                {
                    matrixNodes[i, j].nodeGameobject.GetComponent<MeshRenderer>().enabled = true;
                }
            
                //matrixNodes[i, j].nodeGameobject.GetComponent<NodeTouch>().isUnlock = true;
                // matrixNodes[i, j].nodeGameobject.GetComponent<Renderer>().material.color = Color.green;
            }
        }
    }

    public void ExpandLevel()
    {
        Destroy(auxPlaneLimit);
        for (int i = indexCenterRow - stageRows; i < indexCenterRow + stageRows; i++)
        {
            for (int j = indexCenterColumn - stageColumns; j < indexCenterColumn + stageColumns; j++)
            {
                if (matrixNodes[i, j].nodeGameobject.GetComponent<NodeTouch>().isUnlock == true)
                {
                    continue;
                }
                matrixNodes[i, j].nodeGameobject.GetComponent<MeshRenderer>().enabled = true;
                matrixNodes[i, j].nodeGameobject.GetComponent<NodeTouch>().isUnlock = true;
                // matrixNodes[i, j].nodeGameobject.GetComponent<Renderer>().material.color = Color.green;
            }
        }



        Vector3 auxvect = matrixNodes[indexCenterRow - stageRows, indexCenterColumn - stageColumns].nodeGameobject.transform.position - matrixNodes[indexCenterRow + stageRows, indexCenterColumn + stageColumns].nodeGameobject.transform.position;
        float auxSize = Vector3.Magnitude(auxvect);
        auxPlaneLimit = Instantiate(planeLimit);
        auxPlaneLimit.transform.position = new Vector3(0.5f, 0.1f, -0.8f);
        auxPlaneLimit.transform.localScale = new Vector3(auxSize / 12, 1, auxSize / 12);
        if (spawnThings)
        {
            numHouses += 10;
            SpawnHouses();
            return;
        }

    }

    public void NextStage()
    {
        stageRows += 1;
        stageColumns += 1;
        ExpandLevel();
        //Invoke("ExpandLevel", 0.2f);
    }

    void SpawnHouses()
    {
        int cont = houses.Count;
        while(cont < numHouses)
        {
            int rdnX = Random.Range(indexCenterRow - stageRows, indexCenterRow + stageRows);
            int rdnY = Random.Range(indexCenterColumn - stageColumns, indexCenterColumn + stageColumns);

            if(rdnX == indexCenterRow && rdnY == indexCenterColumn)
            {
                continue;
            }

            if (matrixNodes[rdnX, rdnY].nodeGameobject.GetComponent<NodeTouch>().buildingThere != null)
            {
                Debug.Log("Cant spawn here");
                continue;
            }
            HouseInstantiate(matrixNodes[rdnX, rdnY]);
            cont++;
        }
       
    }

    void HouseInstantiate(Node node)
    {
        NodeTouch aux = node.nodeGameobject.GetComponent<NodeTouch>();
        int rdn = Random.Range(0, differentHouses.Count);
        housePrefab = differentHouses[rdn];
        GameObject spawnHouse = Instantiate(housePrefab, aux.GetBuildPosition(), housePrefab.transform.rotation);

        aux.buildingThere = spawnHouse;
        node.objectInNode = spawnHouse;
        node.idBuilding = (int)idBuildings.houses;
        aux.gameObject.GetComponent<MeshRenderer>().enabled = false;
        houses.Add(spawnHouse);
        
    }

    /*void SpawnHouseAllScenario()
    {
        int cont = 0;
        while (cont < numHouses)
        {
            int rdnX = Random.Range(1, rows-1);
            int rdnY = Random.Range(1, columns-1);

            if (rdnX == indexCenterRow && rdnY == indexCenterColumn)
            {
                continue;
            }

            if (matrixNodes[rdnX, rdnY].objectInNode != null)
            {
                Debug.Log("Cant spawn here");
                continue;
            }
            NodeTouch aux = matrixNodes[rdnX, rdnY].nodeGameobject.GetComponent<NodeTouch>();
            

            GameObject spawnHouse = Instantiate(housePrefab, aux.GetBuildPosition(), housePrefab.transform.rotation);

            aux.buildingThere = spawnHouse;
       
            matrixNodes[rdnX, rdnY].idBuilding = (int)InventoryBuilding.idBuildings.houses;
            matrixNodes[rdnX, rdnY].objectInNode = spawnHouse;
            aux.gameObject.GetComponent<MeshRenderer>().enabled = false;
            cont++;
        }
    }*/


    public List<GameObject> GetHouses()
    {
        return houses;
    }

    public Node[,] GetMatrixNode()
    {
        return matrixNodes;
    }

    public int GetStageX()
    {
        return stageRows;
    }
    public int GetStageY()
    {
        return stageColumns;
    }

    public int GetRows()
    {
        return gridSizeX;
    }
    public int GetColumns()
    {
        return gridSizeY;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
