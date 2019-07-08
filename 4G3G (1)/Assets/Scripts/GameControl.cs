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
    public bool firstTimeCoal = true;
    public bool tutorial = true;
    public bool cheatsOn = false;


    public bool unlockIslandCoal, unlockIslandGas, unlockIslandWind, unlockIslandSolar;

    public int cantCompletedQuest;

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
        cheatsOn = false;

}

    public void SaveGeneralInfo()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;

        file = File.Create(Application.persistentDataPath + "/generalInfo.dat");

        GeneralInfo info = new GeneralInfo();


        CreateEnvironment ce = GameObject.Find("GameManager").GetComponent<CreateEnvironment>();
        WeatherController weather = GameObject.Find("GameManager").GetComponent<WeatherController>();
        Player player = GameObject.Find("Player").GetComponent<Player>();

        info.money = player.totalCurrency;



        info.days = weather.fakeDays;
        info.hour = weather.fakeHour;
        info.minute = weather.fakeMinutes;
        info.month = weather.actualMonthNumber;
        info.night = weather.isNight;

        info.firstTimeCoal = firstTimeCoal;
        info.tutorial = tutorial;

        info.unlockIslandCoal = player.unlockCoal;
        info.unlockIslandGas = player.unlockGas;
        info.unlockIslandSolar = player.unlockSolar;
        info.unlockIslandWind = player.unlockWind;



        bf.Serialize(file, info);
        file.Close();



    }

    public void LoadUnlockIslandInfo()
    {
        if (File.Exists(Application.persistentDataPath + "/generalInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/generalInfo.dat", FileMode.Open);
            GeneralInfo info = (GeneralInfo)bf.Deserialize(file);
            file.Close();


            unlockIslandCoal = info.unlockIslandCoal;
            unlockIslandGas = info.unlockIslandGas;
            unlockIslandSolar = info.unlockIslandSolar;
            unlockIslandWind = info.unlockIslandWind;

            //loaded = true;
            Debug.Log("General info loaded ");
        }
    }

    public void LoadGeneralInfo()
    {

        if (File.Exists(Application.persistentDataPath + "/generalInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/generalInfo.dat", FileMode.Open);
            GeneralInfo info = (GeneralInfo)bf.Deserialize(file);
            file.Close();

            money = info.money;

            days = info.days;
            minute = info.minute;
            hour = info.hour;
            month = info.month;
            night = info.night;

            tutorial = info.tutorial;
            firstTimeCoal = info.firstTimeCoal;

            unlockIslandCoal = info.unlockIslandCoal;
            unlockIslandGas = info.unlockIslandGas;
            unlockIslandSolar = info.unlockIslandSolar;
            unlockIslandWind = info.unlockIslandWind;

            //loaded = true;
            Debug.Log("General info loaded ");
        }
    }

    public void LoadWindIsland()
    {
        LoadGeneralInfo();

        if (File.Exists(Application.persistentDataPath + "/windIslandInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/windIslandInfo.dat", FileMode.Open);
            LevelData data = (LevelData)bf.Deserialize(file);
            file.Close();

            levelCity = data.levelCity;

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

            cantCompletedQuest = data.cantCompletedQuest;

           

            loaded = true;

            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAA GAS LOAD");

        }
        else
        {
            Debug.Log("No se ha creado archivo de guardado");

        }
    }

    public void SaveWindIsland()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        file = File.Create(Application.persistentDataPath + "/windIslandInfo.dat");

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
        data.maxEnergy = (int)player.levelObject.maxValue;

        data.pollution = player.totalPollution;
        data.maxPollution = (int)player.pollutionSlider.maxValue;
        data.happiness = player.totalHappiness;
        data.cantCompletedQuest = player.cantCompletedQuest;


        Node[,] aux = ce.GetMatrixNode();

        information = new NodeInformation[gridSizeX, gridSizeY];

        for (int f = 1; f < gridSizeX; f++)
        {
            for (int c = 1; c < gridSizeY; c++)
            {
                if (aux[f, c].idBuilding == 0)
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

    public void SaveMainIsland()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        file = File.Create(Application.persistentDataPath + "/mainIslandInfo.dat");

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
        data.maxEnergy = (int)player.levelObject.maxValue;

        data.pollution = player.totalPollution;
        data.maxPollution = (int)player.pollutionSlider.maxValue;
        data.happiness = player.totalHappiness;

        data.cantCompletedQuest = player.cantCompletedQuest;
        // data.money = player.totalCurrency;


        //  data.days = weather.fakeDays;
        //  data.hour = weather.fakeHour;
        //  data.minute = weather.fakeMinutes;
        //  data.month = weather.actualMonthNumber;
        //  data.night = weather.isNight;

        Node[,] aux = ce.GetMatrixNode();

        information = new NodeInformation[gridSizeX, gridSizeY];

        for (int f = 1; f < gridSizeX; f++)
        {
            for (int c = 1; c < gridSizeY; c++)
            {
                if (aux[f, c].idBuilding == 0)
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

    public void SaveCoalIsland()
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        file = File.Create(Application.persistentDataPath + "/coalIslandInfo.dat");

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
        data.maxEnergy = (int)player.levelObject.maxValue;

        data.pollution = player.totalPollution;
        data.maxPollution = (int)player.pollutionSlider.maxValue;
        data.happiness = player.totalHappiness;

        data.cantCompletedQuest = player.cantCompletedQuest;

        // data.money = player.totalCurrency;


        //  data.days = weather.fakeDays;
        //  data.hour = weather.fakeHour;
        //  data.minute = weather.fakeMinutes;
        //  data.month = weather.actualMonthNumber;
        //  data.night = weather.isNight;

        Node[,] aux = ce.GetMatrixNode();

        information = new NodeInformation[gridSizeX, gridSizeY];

        for (int f = 1; f < gridSizeX; f++)
        {
            for (int c = 1; c < gridSizeY; c++)
            {
                if (aux[f, c].idBuilding == 0)
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

    public void SaveGasIsland()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        file = File.Create(Application.persistentDataPath + "/gasIslandInfo.dat");

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
        data.maxEnergy = (int)player.levelObject.maxValue;

        data.pollution = player.totalPollution;
        data.maxPollution = (int)player.pollutionSlider.maxValue;
        data.happiness = player.totalHappiness;

        data.cantCompletedQuest = player.cantCompletedQuest;


        Node[,] aux = ce.GetMatrixNode();

        information = new NodeInformation[gridSizeX, gridSizeY];

        for (int f = 1; f < gridSizeX; f++)
        {
            for (int c = 1; c < gridSizeY; c++)
            {
                if (aux[f, c].idBuilding == 0)
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

    public void Save()
    {
        SaveGeneralInfo();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        if(SceneManager.GetActiveScene().name == "CoalIsland")
        {
           file = File.Create(Application.persistentDataPath + "/coalIslandInfo.dat");
            firstTimeCoal = false;
    
        }
        else if (SceneManager.GetActiveScene().name == "GasIsland")
        {
            file = File.Create(Application.persistentDataPath + "/gasIslandInfo.dat");
        }
        else if(SceneManager.GetActiveScene().name == "SolarIsland")
        {
            file = File.Create(Application.persistentDataPath + "/solarIslandInfo.dat");
        }
        else if (SceneManager.GetActiveScene().name == "WindIsland")
        {
            file = File.Create(Application.persistentDataPath + "/windIslandInfo.dat");
        }
        else if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            file = File.Create(Application.persistentDataPath + "/mainIslandInfo.dat");

        }
 
        else
        {
            Debug.Log("Ni uno ni otro");
            return;
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
        data.cantCompletedQuest = player.cantCompletedQuest;
        // data.money = player.totalCurrency;


        //  data.days = weather.fakeDays;
        //  data.hour = weather.fakeHour;
        //  data.minute = weather.fakeMinutes;
        //  data.month = weather.actualMonthNumber;
        //  data.night = weather.isNight;

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

    public void LoadGasIsland()
    {
        LoadGeneralInfo();

        if (File.Exists(Application.persistentDataPath + "/gasIslandInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gasIslandInfo.dat", FileMode.Open);
            LevelData data = (LevelData)bf.Deserialize(file);
            file.Close();

            levelCity = data.levelCity;

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

            cantCompletedQuest = data.cantCompletedQuest;

            loaded = true;

            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAA GAS LOAD");

        }
        else
        {
            Debug.Log("No se ha creado archivo de guardado");

        }
    }

    public void LoadMainIsland()
    {
        LoadGeneralInfo();
        if (File.Exists(Application.persistentDataPath + "/mainIslandInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/mainIslandInfo.dat", FileMode.Open);
            LevelData data = (LevelData)bf.Deserialize(file);
            file.Close();

            levelCity = data.levelCity;

           // money = data.money;
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
            cantCompletedQuest = data.cantCompletedQuest;


            // days = data.days;
            // minute = data.minute;
            // hour = data.hour;
            // month = data.month;
            // night = data.night;

            //  Debug.Log(money);

            //Llamar a la función que cree todo el environment con los datos guardados
            loaded = true;

            Debug.Log("Loading the main island");

        }
        else
        {
            Debug.Log("Not loading the main island NOT");
        }
    }
    public void LoadSolarIsland()
    {
        LoadGeneralInfo();

        if (File.Exists(Application.persistentDataPath + "/solarIslandInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/solarIslandInfo.dat", FileMode.Open);
            LevelData data = (LevelData)bf.Deserialize(file);
            file.Close();

            levelCity = data.levelCity;

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

            cantCompletedQuest = data.cantCompletedQuest;


            loaded = true;

            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAA COAL LOAD");

        }
        else
        {
            Debug.Log("No se ha creado archivo de guardado");

        }
    }
    public void LaodWindIsland()
    {
        LoadGeneralInfo();

        if (File.Exists(Application.persistentDataPath + "/windIslandInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/windIslandInfo.dat", FileMode.Open);
            LevelData data = (LevelData)bf.Deserialize(file);
            file.Close();

            levelCity = data.levelCity;

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

            cantCompletedQuest = data.cantCompletedQuest;



            loaded = true;

            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAA COAL LOAD");

        }
        else
        {
            Debug.Log("No se ha creado archivo de guardado");

        }
    }

    public void LoadCoalIsland()
    {
        LoadGeneralInfo();

        if (File.Exists(Application.persistentDataPath + "/coalIslandInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/coalIslandInfo.dat", FileMode.Open);
            LevelData data = (LevelData)bf.Deserialize(file);
            file.Close();

            levelCity = data.levelCity;

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

            cantCompletedQuest = data.cantCompletedQuest;


            loaded = true;

            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAA COAL LOAD");

        }
        else
        {
            Debug.Log("No se ha creado archivo de guardado");

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
        if (File.Exists(Application.persistentDataPath + "/generalInfo.dat"))
        {

            File.Delete(Application.persistentDataPath + "/generalInfo.dat");
            Debug.Log("Deleted save file");

        }
    
        if (File.Exists(Application.persistentDataPath + "/windIslandInfo.dat"))
        {

            File.Delete(Application.persistentDataPath + "/windIslandInfo.dat");
            Debug.Log("Deleted save file");

        }
        if (File.Exists(Application.persistentDataPath + "/gasIslandInfo.dat"))
        {

            File.Delete(Application.persistentDataPath + "/gasIslandInfo.dat");
            Debug.Log("Deleted save file");

        }
        if (File.Exists(Application.persistentDataPath + "/solarIslandInfo.dat"))
        {

            File.Delete(Application.persistentDataPath + "/solarIslandInfo.dat");
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
    public class GeneralInfo
    {
        public bool tutorial;
        public bool firstTimeCoal;

        public int money;
        public int days, month, hour, minute;
        public bool night;

        public bool unlockIslandCoal, unlockIslandGas, unlockIslandWind, unlockIslandSolar;
    }


    [Serializable]
    public class LevelData
    {
        public int levelCity;
        public int gridSizeX;
        public int gridSizeY;
        public int stageSizeX;
        public int stageSizeY;
 
        public int pollution;
        public int maxPollution;
        public int happiness;
        public int energy;
        public int maxEnergy;

        public NodeInformation[,] information;
        public float sizeXPlane;
        public float sizeYPlane;

        public int cantCompletedQuest;

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
