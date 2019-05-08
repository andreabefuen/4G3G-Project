using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GameObject gameControl = GameObject.Find("GameController");
        if(gameControl != null)
        {
            GameControl.control.Load();
            
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void QuitGame()
    {
        Debug.Log("Game Quited!");
        Application.Quit();
    }
}
