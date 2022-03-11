using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public int level;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UnLockLevel();
            SceneManager.LoadScene("level1");
        }
    }
    public void UnLockLevel()
    {
        if (level >= PlayerPrefs.GetInt("levels"))
            PlayerPrefs.SetInt("levels", level + 1);
    }
}