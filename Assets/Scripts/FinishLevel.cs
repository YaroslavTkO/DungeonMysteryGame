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
            var saveStats = collision.gameObject.GetComponent<PlayerStats>();
            if (saveStats)
            {
                saveStats.Save();
                PlayerPrefs.SetString("savedLevel", "level1");
            }
            var items = FindObjectOfType<ItemsOnLevel>();
            if (items)
            {
                items.CheckCollected();
                items.Save();
            }
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