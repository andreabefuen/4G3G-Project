using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    // Update is called once per frame
    private void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }
    public void PauseButton() {

        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
        
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        SoundManager.soundManager.PlayHitButton();
        //audioSource.Play();
        
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        //audioSource.Play();
        SoundManager.soundManager.PlayHitButton();
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
     //   GameControl.control.SaveGeneralInfo();
        GameControl.control.Save();
        SceneManager.LoadScene("MainMenuScene");
    }

    public void LoadMenuTravelScene()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("MainMenuScene");
    }


    public void QuitGame()
    {
        SoundManager.soundManager.PlayHitButton();
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void GoToTravelScene()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        //GameControl.control.SaveGeneralInfo();
        GameControl.control.Save();
        GameControl.control.LoadGeneralInfo();
        UIManager.instance.LoadScreenAnim();

        Invoke("TravelScene", 2.8f);
       // SceneManager.LoadScene("TravelScene");


    }

    public void GoToGasIsland()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        Debug.Log("Call the gas island");
        GameControl.control.LoadGasIsland();
        Invoke("TravelGas", 2f);
        UIManager.instance.LoadScreenAnim();

    }

    public void GoToCoalFactory()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        Debug.Log("Call the coal factory ");
        GameControl.control.LoadCoalIsland();

        Invoke("TravelCoal", 2f);
        UIManager.instance.LoadScreenAnim();
        //SceneManager.LoadScene("CoalIsland");

        // SceneManager.LoadScene("CoalIsland");


    }

    public void GoToSolarIsland()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        Debug.Log("Call the solar island");
        GameControl.control.LoadSolarIsland();
        Invoke("TravelSolar", 2f);
        UIManager.instance.LoadScreenAnim();
    }

    public void GoToWindIsland()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        Debug.Log("Call the wind island");
        GameControl.control.LoadWindIsland();
        Invoke("TravelWind", 2f);
        UIManager.instance.LoadScreenAnim();

    }

    public void GoToMainIsland()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameControl.control.LoadMainIsland();
        Invoke("TravelMain", 2f);
        UIManager.instance.LoadScreenAnim();
        //SceneManager.LoadScene(1);

    }
    void TravelScene()
    {
        SceneManager.LoadScene("TravelScene");

    }
    void TravelMain()
    {
        SceneManager.LoadScene(1);
    }
    void TravelCoal()
    {
        SceneManager.LoadScene("CoalIsland");
    }
    void TravelSolar()
    {
        SceneManager.LoadScene("SolarIsland");
    }
    void TravelWind()
    {
        SceneManager.LoadScene("WindIsland");

    }
    void TravelGas()
    {
        SceneManager.LoadScene("GasIsland");
    }

    /*public void TravelToCoalFactory()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameControl.control.SaveGeneralInfo();
        GameControl.control.Save();
        GameControl.control.LoadGeneralInfo();
        GameControl.control.LoadCoalIsland();
        SceneManager.LoadScene("CoalIsland");
    }*/
}

