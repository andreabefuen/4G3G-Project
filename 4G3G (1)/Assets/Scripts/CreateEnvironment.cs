using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnvironment : MonoBehaviour
{

    
    public float offset;
    public int numStage;

    public GameObject prefabNode;

    public Node[,] matrixNodes;

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
    public GameObject housePrefab;
    public int numHouses;
    public GameObject coalFactory;
    public int numCoalFactories;


    public static List<GameObject> houses;


    private void Awake()
    {
        nodeDiameter = nodeRadius * 2;

        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        rows = gridSizeX;
        columns = gridSizeY;

        stageColumns = rows / numStage;
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


            }

            actualPos = new Vector3(startPos.x, startPos.y, (offset + nodeDiameter + actualPos.z));
        }
        centerNode = matrixNodes[indexCenterRow, indexCenterColumn].nodeGameobject;
        centerNode.tag = "CityPlace";
        
        //matrixNodes[0, 0].nodeGameobject.SetActive(false);

        CreateStage();
        //SpawnHouses();
        //SpawnHouseAllScenario();
        //SpawnCoalFactories();
    }


    void CreateStage()
    {
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
        centerNode.GetComponent<Renderer>().material.color = Color.cyan;

        numHouses *= 2; 
        SpawnHouses();
        

    }

    public void NextStageButton()
    {
        stageRows += 1;
        stageColumns += 1;
        Invoke("CreateStage",0.5f);
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
            NodeTouch aux = matrixNodes[rdnX, rdnY].nodeGameobject.GetComponent<NodeTouch>();

            GameObject spawnHouse = Instantiate(housePrefab, aux.GetBuildPosition(), housePrefab.transform.rotation);

            aux.buildingThere = spawnHouse;
            matrixNodes[rdnX, rdnY].objectInNode = spawnHouse;
            aux.gameObject.GetComponent<MeshRenderer>().enabled = false;
            houses.Add(spawnHouse);
            cont++;
        }
       
    }

    void SpawnHouseAllScenario()
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
            matrixNodes[rdnX, rdnY].objectInNode = spawnHouse;
            aux.gameObject.GetComponent<MeshRenderer>().enabled = false;
            cont++;
        }
    }

    void SpawnCoalFactories()
    {
        int cont = 0;
        while (cont < numCoalFactories)
        {
            int rdnX = Random.Range(indexCenterRow - stageRows, indexCenterRow + stageRows);
            int rdnY = Random.Range(indexCenterColumn - stageColumns, indexCenterColumn + stageColumns);

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

            GameObject spawnFactory = Instantiate(coalFactory, aux.GetBuildPosition(), coalFactory.transform.rotation);

            aux.buildingThere = spawnFactory;
            matrixNodes[rdnX, rdnY].objectInNode = spawnFactory;
            aux.gameObject.GetComponent<MeshRenderer>().enabled = false;
            cont++;
        }
    }

    public List<GameObject> GetHouses()
    {
        return houses;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
