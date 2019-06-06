using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{

    public static GameControl control;

    public int levelCity;
    public int money;
    public int pollution;
    public int maxPollution;
    public int happiness;
    public int energy;
    public int maxEnergy;
    public int stageSizeX;
    public int stageSizeY;
    public int gridSizeX, gridSizeY;
    public int days, month, hour, minute;
    public bool night;
    public NodeInformation[,] information;

    public float sizeXPlane;
    public float sizeYPlane;


    public bool loaded = false;

    

    void Awake()
    {

        if (control == null)
        {
            control = this;
        }
        else if (control != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        if(SceneManager.GetActiveScene().name == "CoalIsland")
        {
           file = File.Create(Application.persistentDataPath + "/coalIslandInfo.dat");
            Debug.Log("ahsfadsfajsgfgsss ");
        }
        else
        {
            file = File.Create(Application.persistentDataPath + "/mainIslandInfo.dat");

        }

        LevelData data = new LevelData();


        CreateEnvironment ce = GameObject.Find("GameManager").GetComponent<CreateEnvironment>();
        WeatherController weather = GameObject.Find("GameManager").GetComponent<WeatherController>();
        Player player = GameObject.Find("Player").GetComponent<Player>();

        stageSizeX = ce.GetStageX();
        stageSizeY = ce.GetStageY();
        data.stageSizeX = stageSizeX;
        data.stageSizeY = stageSizeY;
        gridSizeX = ce.GetRows();
        gridSizeY = ce.GetColumns();
        data.gridSizeX = gridSizeX;
        data.gridSizeY = gridSizeY;

        data.levelCity = player.levelCity;

        data.sizeXPlane = ce.planeLimit.transform.localScale.x;
        data.sizeYPlane = ce.planeLimit.transform.localScale.z;

        data.energy = player.totalEnergy;
        data.maxEnergy = (int) player.levelObject.maxValue;
        
        data.pollution = player.totalPollution;
        data.maxPollution = (int)player.pollutionSlider.maxValue;
        data.happiness = player.totalHappiness;
        data.money = player.totalCurrency;


        data.days = weather.fakeDays;
        data.hour = weather.fakeHour;
        data.minute = weather.fakeMinutes;
        data.month = weather.actualMonthNumber;
        data.night = weather.isNight;

        Node[,] aux = ce.GetMatrixNode();

        information = new NodeInformation[gridSizeX, gridSizeY];

        for (int f = 1; f < gridSizeX; f++)
        {
            for (int c = 1; c < gridSizeY; c++)
            {
                if(aux[f,c].idBuilding == 0)
                {
                    information[f, c].isUnlock = false;
                    information[f, c].haveWater = false;
                    information[f, c].idBuilding = 0;
                    //continue;
                }
                else
                {
                    information[f, c].idBuilding = aux[f, c].idBuilding;
                    information[f, c].isUnlock = true;
                    information[f, c].haveWater = false;
                }
            }
        }

        data.information = information;
       // data.money = GameObject.Find("Player").GetComponent<Player>().totalCurrency;

        if (information.Length != 0)
        {
            Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        }

        bf.Serialize(file, data);
        file.Close();

        Debug.Log("Se ha creado archivo de guardado");
    }

    public void LoadMainIsland()
    {
       
        if (File.Exists(Application.persistentDataPath + "/mainIslandInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/mainIslandInfo.dat", FileMode.Open);
            LevelData data = (LevelData)bf.Deserialize(file);
            file.Close();

            levelCity = data.levelCity;

            money = data.money;
            stageSizeX = data.stageSizeX;
            stageSizeY = data.stageSizeY;
            gridSizeX = data.gridSizeX;
            gridSizeY = data.gridSizeY;

            pollution = data.pollution;
            happiness = data.happiness;
            energy = data.energy;
            maxPollution = data.maxPollution;
            maxEnergy = data.maxEnergy;

            information = data.information;

            sizeXPlane = data.sizeXPlane;
            sizeYPlane = data.sizeYPlane;

            days = data.days;
            minute = data.minute;
            hour = data.hour;
            month = data.month;
            night = data.night;

            Debug.Log(money);

            //Llamar a la función que cree todo el environment con los datos guardados
            loaded = true;
           
            

        }
        else
        {
            Debug.Log("no se ha creado archivo");
        }
    }

    public void LoadCoalIsland()
    {
        if (File.Exists(Application.persistentDataPath + "/coalIslandInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/coalIslandInfo.dat", FileMode.Open);
            LevelData data = (LevelData)bf.Deserialize(file);
            file.Close();

            levelCity = data.levelCity;
            money = data.money;
            stageSizeX = data.stageSizeX;
            stageSizeY = data.stageSizeY;
            gridSizeX = data.gridSizeX;
            gridSizeY = data.gridSizeY;

            pollution = data.pollution;
            happiness = data.happiness;
            energy = data.energy;
            maxPollution = data.maxPollution;
            maxEnergy = data.maxEnergy;

            information = data.information;

            sizeXPlane = data.sizeXPlane;
            sizeYPlane = data.sizeYPlane;

            Debug.Log(money);

            Debug.Log("Coal island information loaded");

            //Llamar a la función que cree todo el environment con los datos guardados
            loaded = true;
        }
    }

    public void DeleteSave()
    {
        if (File.Exists(Application.persistentDataPath + "/mainIslandInfo.dat"))
        {
            
            File.Delete(Application.persistentDataPath + "/mainIslandInfo.dat");
            Debug.Log("Deleted save file");
            
        }
        if (File.Exists(Application.persistentDataPath + "/coalIslandInfo.dat"))
        {

            File.Delete(Application.persistentDataPath + "/coalIslandInfo.dat");
            Debug.Log("Deleted save file");

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Serializable]
    public class LevelData
    {
        // public int[,] matrixOfNodes;
        public int levelCity;
        public int gridSizeX;
        public int gridSizeY;
        public int stageSizeX;
        public int stageSizeY;
        public int money;
        public int pollution;
        public int maxPollution;
        public int happiness;
        public int energy;
        public int maxEnergy;

        public NodeInformation[,] information;
        public float sizeXPlane;
        public float sizeYPlane;

        public int days, month, hour, minute;
        public bool night;

    }

    [Serializable]
    public class CoalInformation
    {

    }

    [Serializable]
    public struct NodeInformation
    {
        public int idBuilding;
        public bool isUnlock;
        public bool haveWater;
    }


}
