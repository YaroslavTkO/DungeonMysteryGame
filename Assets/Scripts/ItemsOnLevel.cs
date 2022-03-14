using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemsOnLevel : MonoBehaviour
{
    public GameObject[] itemsOnLevel;

    public bool[] isActive;

    private void Start()
    {
        Load();
        SpawnItems();
    }

    public void SpawnItems()
    {
        for (int i = 0; i < itemsOnLevel.Length; i++)
        {
            if (!isActive[i])
                itemsOnLevel[i].SetActive(false);
        }
    }

    public void CheckCollected()
    {
        for (int i = 0; i < itemsOnLevel.Length; i++)
            if (itemsOnLevel[i] == null)
                isActive[i] = false;
    }

    public void Save()
    {
        for (int i = 0; i < isActive.Length; i++)
            PlayerPrefs.SetInt($"{i}.{SceneManager.GetActiveScene().name}", isActive[i] ? 1 : 0);
    }

    public void Load()
    {
        isActive = new bool[itemsOnLevel.Length];
        for (int i = 0; i < isActive.Length; i++)
        {
            if (PlayerPrefs.HasKey($"{i}.{SceneManager.GetActiveScene().name}"))
                isActive[i] = PlayerPrefs.GetInt($"{i}.{SceneManager.GetActiveScene().name}") == 1;
            else isActive[i] = true;
        }
    }
}