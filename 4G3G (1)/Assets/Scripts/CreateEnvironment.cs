using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnvironment : MonoBehaviour
{


    public float offset;

    public GameObject prefabNode;

    public Node[,] matrixNodes;

    Vector3 startPos = new Vector3(0, 0, 0);

    public Vector2 gridWorldSize;
    public float nodeRadius;
    int gridSizeX, gridSizeY;
    float nodeDiameter;

    private int rows, columns;
    private GameObject grid;


    private void Awake()
    {
        nodeDiameter = nodeRadius * 2;

        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        rows = gridSizeX;
        columns = gridSizeY;

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

            }

            startPos = new Vector3(0, 0, (offset + nodeDiameter)* f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
