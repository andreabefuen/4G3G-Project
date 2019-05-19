using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public AudioClip audio;


    AudioSource audioSource;

    // Update is called once per frame
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audio;
        
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
        audioSource.Play();
        GameControl.control.Save();
        SceneManager.LoadScene("MainMenuScene");
    }

    public void QuitGame()
    {
        audioSource.Play();
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}

