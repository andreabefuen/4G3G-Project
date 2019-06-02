using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Light directionalLight;
    public int timerFactor;
    public int maxDaysRaining;
    public int daysWithRain;
    public bool raining = false;
    public GameObject rainParticlePrefab;
    private ParticleSystem rainParticle;

    public float seconds, fakeSeconds;
    public int minutes, fakeMinutes, hour, fakeHour, days, fakeDays;

    // Start is called before the first frame update
    void Start()
    {
        GameObject aux = Instantiate(rainParticlePrefab);
        rainParticle = aux.GetComponent<ParticleSystem>();
        rainParticle.Stop();
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
                    directionalLight.intensity = 1;
                    daysWithRain = 0;
                }
            }
        }
        if(fakeDays%5 == 0 && !raining)
        {
            Debug.Log("ITS RAINING MEN");
            raining = true;
            directionalLight.intensity = 0.8f;
            rainParticle.Play();
        }

        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        timerText.text = fakeDays + " days, " + fakeHour + ":" + fakeHour + ":" + fakeMinutes;
    }
}
