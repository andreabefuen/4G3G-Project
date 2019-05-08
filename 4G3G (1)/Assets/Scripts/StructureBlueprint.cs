using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class StructureBlueprint 
{

    public int id;

    public GameObject prefab;

    public GameObject informationPanel;

    public int cost;

    public NodeTouch nodeAsociate;

    public float timeMoney;

    public int moneyPerTap;

    public int amountOfEnergy;

    public int amountOfHappiness;

    public int amountOfPollution;

    public int costOfDemolition;

    public bool needWater;

    public bool isWater;


}
