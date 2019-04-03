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
   

    TextMeshProUGUI moneyText;

    List<GameObject> allTheHouses;

    CreateEnvironment createEnvironment;


    private void Start()
    {
        moneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();

        UpdateMoney();

        UpdateHappiness();

        

        allTheHouses = CreateEnvironment.houses;

        createEnvironment = GameObject.Find("GameManager").GetComponent<CreateEnvironment>();

        energySlider.maxValue = createEnvironment.numHouses * energyForEachHouse;
      
        energySlider.value = 0;

        UpdateEnergy();
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
        if(totalEnergy > energySlider.maxValue)
        {
            createEnvironment.NextStageButton();
            energySlider.maxValue = createEnvironment.numHouses * energyForEachHouse;
            energySlider.value = totalEnergy;
            
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

    public List<Quest> GetActiveQuests()
    {
        return activeQuest;
    }
}
