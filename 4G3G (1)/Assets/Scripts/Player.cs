using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int totalCurrency;

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

    public Slider energySlider;

    public GameObject enoughMoney;
   

    TextMeshProUGUI moneyText;


    CreateEnvironment createEnvironment;


    private void Start()
    {
        moneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        createEnvironment = GameObject.Find("GameManager").GetComponent<CreateEnvironment>();

        if (GameControl.control.loaded)
        {
            totalCurrency = GameControl.control.money;
            totalEnergy = GameControl.control.energy;
            totalHappiness = GameControl.control.happiness;
            totalPollution = GameControl.control.pollution;

            energySlider.maxValue = GameControl.control.maxEnergy;
            pollutionSlider.maxValue = GameControl.control.maxPollution;

        }
        else
        {
            

            energySlider.maxValue = createEnvironment.numHouses * energyForEachHouse;

            energySlider.value = 0;
        }
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
        if(totalEnergy >= energySlider.maxValue)
        {
            Debug.Log("aaaProblema???");
            createEnvironment.NextStage();
            Debug.Log("Problema???");
            levelCity++;
            energySlider.maxValue = createEnvironment.numHouses * energyForEachHouse;
            energySlider.value = totalEnergy;
            return;
            
        }
        energySlider.value = totalEnergy;
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
