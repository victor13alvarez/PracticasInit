using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;
    public GameObject turnUI;
    public GameObject getClose;
    public GameObject pauseUI;
    public static bool isPaused = false;

    // Update is called once per frame

    public void buttonPressed()
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

    public void Resume()
    {
        isPaused = false;
        pauseUI.SetActive(false);
        turnUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
    }

    public void Pause()
    {
        isPaused = true;
        pauseUI.SetActive(true);
        turnUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
