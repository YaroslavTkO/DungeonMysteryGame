using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsMenu : MonoBehaviour
{
    int levelUnLock;
    public Button[] buttons;
    public GameObject[] locks;

    void Start()
    {
        levelUnLock = PlayerPrefs.GetInt("levels", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = i < levelUnLock;
            locks[i].SetActive(!(i < levelUnLock));
        }
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
        var items = FindObjectOfType<ItemsOnLevel>();
        if (items)
        {
            items.CheckCollected();
            items.Save();
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