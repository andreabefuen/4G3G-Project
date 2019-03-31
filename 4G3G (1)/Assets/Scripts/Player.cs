using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public int totalCurrency;
    public int totalHappiness;

    public int levelCity;

    public List<Quest> activeQuest;

    TextMeshProUGUI moneyText;

    private void Start()
    {
        moneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        moneyText.text = totalCurrency.ToString();
    }
}
