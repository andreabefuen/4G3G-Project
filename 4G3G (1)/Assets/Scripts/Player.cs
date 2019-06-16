using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;

    public int totalCurrency;

    public bool unlockCoal, unlockGas, unlockWind, unlockSolar;

    [Range(0, 100)]
    public int totalHappiness;
    [Range(0, 100)]
    public int totalPollution;

    public int totalEnergy;
    public int energyForEachHouse;


    public int levelCity;


    public List<Quest> activeQuest;



    [Header("Stats UI")]
    public Slider happinessSlider;
    public Slider pollutionSlider;

    public Slider levelObject;

    //public Slider energySlider;

    public GameObject enoughMoney;
 
    public TextMeshProUGUI levelText;



    TextMeshProUGUI moneyText;
    



    CreateEnvironment createEnvironment;

    private void Awake()
    {
        instance = this;


        moneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        if(GameObject.Find("GameManager") != null)
        {
            createEnvironment = GameObject.Find("GameManager").GetComponent<CreateEnvironment>();

        }

    }


    private void Start()
    {

        if (GameControl.control.firstTimeCoal)
        {
            totalCurrency = GameControl.control.money;
            unlockCoal = GameControl.control.unlockIslandCoal;
            unlockSolar = GameControl.control.unlockIslandSolar;
            unlockGas = GameControl.control.unlockIslandGas;
            unlockWind = GameControl.control.unlockIslandWind;
        }
       else if (GameControl.control.loaded && !GameControl.control.firstTimeCoal)
        {

            totalCurrency = GameControl.control.money;
            totalEnergy = GameControl.control.energy;
            totalHappiness = GameControl.control.happiness;
            totalPollution = GameControl.control.pollution;
            

            levelObject.maxValue = GameControl.control.maxEnergy;
            pollutionSlider.maxValue = GameControl.control.maxPollution;

            levelCity = GameControl.control.levelCity;
        }
        else
        {


            levelObject.maxValue = 14 * energyForEachHouse;

            levelObject.value = 0;
        }

        levelText.text = levelCity.ToString();


        //levelText.text = levelCity.ToString();
        UpdateMoney();

        UpdateHappiness();

        


        UpdateEnergy();
        UpdatePollution();
    }



    public void ReloadPlayerInfo()
    {
        Debug.Log("ENTRAAAA ");
        totalCurrency = GameControl.control.money;

        unlockCoal = GameControl.control.unlockIslandCoal;
        unlockSolar = GameControl.control.unlockIslandSolar;
        unlockGas = GameControl.control.unlockIslandGas;
        unlockWind = GameControl.control.unlockIslandWind;

        totalEnergy = GameControl.control.energy;
        totalHappiness = GameControl.control.happiness;
        totalPollution = GameControl.control.pollution;


        levelObject.maxValue = GameControl.control.maxEnergy;
        pollutionSlider.maxValue = GameControl.control.maxPollution;

        levelCity = GameControl.control.levelCity;

        UpdateMoney();

        UpdateHappiness();




        UpdateEnergy();
        UpdatePollution();
    }

    public void UpdateMoney()
    {
        moneyText.text = totalCurrency.ToString();
    }

    public void UpdateHappiness()
    {

        happinessSlider.value = totalHappiness;
        ChangeIcon();
    }

    public void UpdateEnergy()
    {
        if (levelCity >= createEnvironment.numStage)
        {
            Debug.Log("finish level");
            return;
        }

        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            if (totalEnergy >= levelObject.maxValue)
            {
                createEnvironment.NextStage();
                levelCity++;
                levelText.text = levelCity.ToString();
                levelObject.maxValue += 100;
                levelObject.value = totalEnergy;
            }
            levelObject.value = totalEnergy;
            return;
        }
        else
        {
            if (totalEnergy >= levelObject.maxValue)
            {
                //Debug.Log("aaaProblema???");
                createEnvironment.NextStage();
                // Debug.Log("Problema???");
                levelCity++;
                levelText.text = levelCity.ToString();
                levelObject.maxValue = CreateEnvironment.houses.Count * energyForEachHouse;
                levelObject.value = totalEnergy;
                //Call to the window of the new level reach
                return;

            }
            levelObject.value = totalEnergy;

            int aux = totalEnergy / energyForEachHouse;
            Debug.Log("We have " + CreateEnvironment.houses.Count);
            Debug.Log("We need " + levelObject.maxValue);
            Debug.Log("Energy for: " + aux);


            for (int i = 1; i < aux; i++)
            {
                CreateEnvironment.houses[i].GetComponent<EnergyBuilding>().EnoughtEnergy();
            }
        }
      

    
    }

    public void UpdatePollution()
    {
        pollutionSlider.value = totalPollution;

        if(pollutionSlider.value == 0)
        {
            //End the game, you kill the city, game over
            Debug.Log("DIE ");
        }
    }

    public int GetTotalMoney()
    {
        return totalCurrency;
    }

    public void SetTotalMoney(int current)
    {
        totalCurrency = current;
    }

    public void IncreaseMoney(int amount)
    {
        totalCurrency += amount;
        if(activeQuest.Count != 0)
        {
            activeQuest[0].goal.IncreaseTheGoalMoney(amount);

        }
        UpdateMoney();
    }

    public void DecreaseMoney (int amount)
    {
        totalCurrency -= amount;
        UpdateMoney();
    }

    public void DecreaseHappiness (int amount)
    {
        totalHappiness -= amount;
        UpdateHappiness();
    }
    public void IncreaseHappiness(int amount)
    {
        totalHappiness += amount;
        UpdateHappiness();

    }

    public void IncreasePollution(int amount)
    {
        totalPollution += amount;
        UpdatePollution();
    }

    public void IncreaseEnergy(int amount)
    {
        totalEnergy += amount;
        UpdateEnergy();
    }

    public void ChangeIcon()
    {
        if(totalHappiness > 60)
        {
            happinessSlider.GetComponent<OnHappinessChange>().ChangeToHigh();
        }
        else if(totalHappiness < 40)
        {
            happinessSlider.GetComponent<OnHappinessChange>().ChangeToLow();

        }
        else
        {
            happinessSlider.GetComponent<OnHappinessChange>().ChangeToNormal();

        }
    }

    public void NotEnoughMoneyPlay()
    {
        enoughMoney.GetComponent<Animation>().Play();
    }

    public List<Quest> GetActiveQuests()
    {
        return activeQuest;
    }
}
