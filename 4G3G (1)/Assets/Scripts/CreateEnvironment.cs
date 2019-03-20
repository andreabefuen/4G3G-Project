using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnvironment : MonoBehaviour
{


    public float offset;
    public int numStage;

    public GameObject prefabNode;

    public Node[,] matrixNodes;

    Vector3 startPos = new Vector3(0, 0, 0);

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
                aux.transform.position = startPos;
                startPos = new Vector3((offset + nodeDiameter)*c, 0 , startPos.z);

                matrixNodes[f, c] = new Node(aux.transform.position, f, c);
                matrixNodes[f, c].nodeGameobject = aux;

            }

            startPos = new Vector3(0, 0, (offset + nodeDiameter)* f);
        }
        centerNode = matrixNodes[indexCenterRow, indexCenterColumn].nodeGameobject;
        //matrixNodes[0, 0].nodeGameobject.SetActive(false);

        CreateStage();
        SpawnHouses();
    }


    void CreateStage()
    {
        for (int i = indexCenterRow - stageRows; i < indexCenterRow + stageRows; i++)
        {
            for (int j = indexCenterColumn - stageColumns; j < indexCenterColumn + stageColumns; j++)
            {
                matrixNodes[i, j].nodeGameobject.GetComponent<Renderer>().material.color = Color.green;
            }
        }
        centerNode.GetComponent<Renderer>().material.color = Color.cyan;

    }

    void SpawnHouses()
    {
        int cont = 0;
        while(cont <= numHouses)
        {
            int rdnX = Random.Range(indexCenterRow - stageRows, indexCenterRow + stageRows);
            int rdnY = Random.Range(indexCenterColumn - stageColumns, indexCenterColumn + stageColumns);

            if(rdnX == indexCenterRow && rdnY == indexCenterColumn)
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
