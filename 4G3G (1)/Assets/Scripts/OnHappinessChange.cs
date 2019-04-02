using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnHappinessChange : MonoBehaviour
{

    public Image changeImagen;
    public Sprite normalHappiness;
    public Sprite highHappiness;
    public Sprite lowHappiness;

    Image normalImage;
    Image highImage;
    Image lowImage;

    private void Start()
    {
    }

    public void ChangeToNormal()
    {
        changeImagen.sprite = normalHappiness;
    }
    public void ChangeToHigh()
    {
        changeImagen.sprite = highHappiness;
    }
    public void ChangeToLow()
    {
        changeImagen.sprite = lowHappiness;
    }
}
