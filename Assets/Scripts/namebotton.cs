using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class namebotton : MonoBehaviour
{
    public string levelIndex;
    // Start is called before the first frame update
    public void botton()
    {
        var saveStats = FindObjectOfType<PlayerStats>();
        if (saveStats)
        {
            saveStats.Save();
            PlayerPrefs.SetString("savedLevel", levelIndex);
        }
        SceneManager.LoadScene(levelIndex);
    }

    
}
