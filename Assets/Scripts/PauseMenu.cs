using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;

    private void Start()
    {
        pausePanel.SetActive(false);
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadLevel(string level)
    {
        if (level.Equals("level1"))
            PlayerPrefs.SetString("savedLevel", level);
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }
}