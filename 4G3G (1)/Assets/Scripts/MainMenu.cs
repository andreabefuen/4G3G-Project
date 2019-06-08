using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider volumenSlider;

    public void PlayGame()
    {
        SoundManager.soundManager.PlayHitButton();

        GameObject gameControl = GameObject.Find("GameController");
        if(gameControl != null)
        {
            GameControl.control.LoadGeneralInfo();
            GameControl.control.LoadMainIsland();
            
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void QuitGame()
    {
        SoundManager.soundManager.PlayHitButton();
        Debug.Log("Game Quited!");
        Application.Quit();
    }

    public void DeleteSaveFile()
    {

        SoundManager.soundManager.PlayHitButton();
        GameObject gameControl = GameObject.Find("GameController");
        if (gameControl != null)
        {
            GameControl.control.DeleteSave();

        }
    }

    public void OnValueChangeVolumen()
    {
        SoundManager.soundManager.ChangeVolumen(volumenSlider.value);
    }
}
