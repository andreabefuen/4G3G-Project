using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class GameControl : MonoBehaviour
{

    public static GameControl control;

    public int money;
    public int stageSizeX;
    public int stageSizeY;
    public int gridSizeX, gridSizeY;
    public NodeInformation[,] information;


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
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        LevelData data = new LevelData();


        CreateEnvironment ce = GameObject.Find("GameManager").GetComponent<CreateEnvironment>();

        stageSizeX = ce.GetStageX();
        stageSizeY = ce.GetStageY();
        data.stageSizeX = stageSizeX;
        data.stageSizeY = stageSizeY;
        gridSizeX = ce.GetRows();
        gridSizeY = ce.GetColumns();
        data.gridSizeX = gridSizeX;
        data.gridSizeY = gridSizeY;

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
        data.money = GameObject.Find("Player").GetComponent<Player>().totalCurrency;

        if (information.Length != 0)
        {
            Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        }

        bf.Serialize(file, data);
        file.Close();

        Debug.Log("Se ha creado archivo de guardado");
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            LevelData data = (LevelData)bf.Deserialize(file);
            file.Close();

            
            money = data.money;
            stageSizeX = data.stageSizeX;
            stageSizeY = data.stageSizeY;
            gridSizeX = data.gridSizeX;
            gridSizeY = data.gridSizeY;

            information = data.information;

            Debug.Log(money);

            //Llamar a la función que cree todo el environment con los datos guardados
            loaded = true;
           
            

        }
        else
        {
            Debug.Log("no se ha creado archivo");
        }
    }

    public void DeleteSave()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            
            File.Delete(Application.persistentDataPath + "/playerInfo.dat");
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
        public int gridSizeX;
        public int gridSizeY;
        public int stageSizeX;
        public int stageSizeY;
        public int money;
        public NodeInformation[,] information;
    }

    [Serializable]
    public struct NodeInformation
    {
        public int idBuilding;
        public bool isUnlock;
        public bool haveWater;
    }
}
