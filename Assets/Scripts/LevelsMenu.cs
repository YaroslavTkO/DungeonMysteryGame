using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsMenu : MonoBehaviour
{
    int levelUnLock;
    public Button[] buttons;

    void Start()
    {
        levelUnLock = PlayerPrefs.GetInt("levels", 1);
        for (int i = 1; i < buttons.Length; i++)
            buttons[i].interactable = i < levelUnLock;
    }

    public void LoadLevel(string levelName)
    {
        PlayerPrefs.SetString("lastLevel", SceneManager.GetActiveScene().name);
            
        var saveStats = FindObjectOfType<PlayerStats>();
        if (saveStats)
        {
            saveStats.Save();
            PlayerPrefs.SetString("savedLevel", levelName);
        }
        SceneManager.LoadScene(levelName);
    }

    public void Reset()
    {
        for (int i = 1; i < buttons.Length; i++)
            buttons[i].interactable = false;

        PlayerPrefs.DeleteKey("levels");
    }
}