using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    //public GameObject nodeGameobject;
    public Transform nodeTransform;
    public Vector3 worldPosition;

    public GameObject objectInNode; //The thing that is on the node (building, solar panel, etc)

    public List<Node> neighbours; //The neighbours of the node


    public int gridX, gridY;



    public Node(Vector3 _worldPos, int _gridX, int _gridY)
    {
        //nodeGameobject = this.gameObject;
        _worldPos = worldPosition;
        _gridX = gridX;
        _gridY = gridY;

        objectInNode = null;
    }




}
