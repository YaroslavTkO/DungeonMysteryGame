using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class n : MonoBehaviour
{
    
    public static n instance = null;
    int levelUnLock;
    void Start()
    {
    
        levelUnLock = PlayerPrefs.GetInt("levels");
    }
    


    public void re()
    {
       

            SceneManager.LoadScene(11);
        
    }
}
