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
        GameControl.control.Save();
        SceneManager.LoadScene("MainMenuScene");
    }

    public void QuitGame()
    {
        SoundManager.soundManager.PlayHitButton();
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void TravelToCoalFactory()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameControl.control.Save();
        GameControl.control.LoadCoalIsland();
        SceneManager.LoadScene("CoalIsland");
    }
}

