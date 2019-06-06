using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Light directionalLight;
    public int hourNight, hourDay;
    public int actualMonthNumber;
    public bool isNight = false;
    public int timerFactor;
    public int maxDaysRaining;
    public int daysWithRain;
    public bool raining = false;
    public GameObject rainParticlePrefab;
    private ParticleSystem rainParticle;

    public float seconds, fakeSeconds;
    public int minutes, fakeMinutes, hour, fakeHour , days, fakeDays;

    string actualMonth;
    //int actualMonthNumber;

    string[] months = new string[] { "January", "February", "March", "April", "May", "June", "July", "Agoust", "September", "October", "November", "Decemeber" };

    // Start is called before the first frame update
    void Awake()
    {
        GameObject aux = Instantiate(rainParticlePrefab);
        rainParticle = aux.GetComponent<ParticleSystem>();
        rainParticle.Stop();


        if (GameControl.control!= null && GameControl.control.loaded)
        {
            fakeDays = GameControl.control.days;
            days = GameControl.control.days;
            fakeMinutes = GameControl.control.minute;
            minutes = GameControl.control.minute;

            fakeHour = GameControl.control.hour;
            hour = GameControl.control.hour;

            actualMonthNumber = GameControl.control.month;
            isNight = GameControl.control.night;
            if (isNight)
            {
                directionalLight.intensity = directionalLight.intensity = 0.3f;
            }

            UpdateTimerText();

        }
        else
        {
            hour = 10;
            fakeHour = 10;
            days = 1;
            fakeDays = 1;
            actualMonthNumber = 0;
        }

        actualMonth = months[actualMonthNumber];
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;

        fakeSeconds = seconds * timerFactor;

        if(fakeSeconds >= 60f)
        {
            fakeSeconds = 0;
            fakeMinutes++;
            seconds = 0;
        }
        if(fakeMinutes >= 60)
        {
            fakeMinutes = 0;
            fakeHour++;
        }
        if(fakeHour >= 24)
        {
            fakeHour = 0;
            fakeDays++;
            if (raining)
            {
                daysWithRain++;
                if(daysWithRain >= maxDaysRaining)
                {
                    raining = false;
                    rainParticle.Stop();
                    if (!isNight)
                    {
                        directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, 1f, Time.deltaTime);
                    }
                    daysWithRain = 0;
                }
            }
        }
        if(fakeDays%5 == 0 && !raining)
        {
            Debug.Log("ITS RAINING MEN");
            raining = true;
            
            rainParticle.Play();
        }
        if (raining)
        {
            if (!isNight)
            {
                directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, 0.5f, Time.deltaTime);

            }
        }

        if(fakeDays > 30)
        {
            fakeDays = 1;
            actualMonthNumber++;
            if (actualMonthNumber > 11)
            {
                actualMonthNumber = 0;
            }
        }

        UpdateTimerText();

        if(fakeHour >= hourDay && fakeHour < hourNight && isNight)
        {
            Debug.Log("day");
            isNight = false;
            directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, 1f, Time.deltaTime);
        }
        if(!isNight && fakeHour >= hourNight)
        {
            Debug.Log("NIGHT");
            directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, 0.3f, Time.deltaTime);
            isNight = true;
        }

        if (isNight)
        {
            directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, 0.3f, Time.deltaTime);
        }
        if (!isNight)
        {
            directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, 1f, Time.deltaTime);

        }
        /*
        if(fakeHour >= hourNight)
        {
            Debug.Log("NIGHT");
            directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, 0.3f, Time.deltaTime);
            isNight = true;
        }
        if(fakeHour <= hourDay && fakeHour < hourNight)
        {
            Debug.Log("day");
            isNight = false;
            directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, 1f, Time.deltaTime);
        }*/
    }

    void UpdateTimerText()
    {
        timerText.text = months[actualMonthNumber] + " " +  fakeDays + ", " + fakeHour + ":" + fakeMinutes/10 + "0";
    }
}
