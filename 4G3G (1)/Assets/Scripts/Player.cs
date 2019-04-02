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

    public int levelCity;

    public List<Quest> activeQuest;



    [Header("Stats UI")]
    public Slider happinessSlider;
    public Slider pollutionSlider;
   

    TextMeshProUGUI moneyText;


    private void Start()
    {
        moneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();

        UpdateMoney();

        UpdateHappiness();
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
