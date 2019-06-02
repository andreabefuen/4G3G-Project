using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class StructureBlueprint 
{
    public string title, description;
    public Sprite icon;

    public idBuildings id;

    public GameObject prefab;

    public int levelBuilding = 1;


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
