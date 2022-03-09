using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Nmascene : MonoBehaviour
{
    public string levelIndex;

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetString("lastLevel", SceneManager.GetActiveScene().name);
            
            SceneManager.LoadScene(levelIndex);
        }
    }
}