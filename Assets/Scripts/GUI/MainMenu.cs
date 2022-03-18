using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject continueGame;
    public Inventory[] inventories;
    public Database database;
    private void Start()
    {
        database.Serialize();
        foreach (var inv in inventories)
        {
            inv.ClearInventory();
            inv.Load();
        }
        if (!PlayerPrefs.HasKey("GameStarted"))
            continueGame.SetActive(false);
    }

    public void Continue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("savedLevel"));
    }

    public void NewGame()
    {
        foreach (var inv in inventories)
            inv.ClearInventory();
        var saveData = Application.persistentDataPath + "/" + "playerStats.aaa";
        var saveDataInv1 = Application.persistentDataPath + "/" + "PlayerEquipment.eqp";
        var saveDataInv2 = Application.persistentDataPath + "/" + "PlayerInv.inv";
        var saveDataInv3 = Application.persistentDataPath + "/" + "foodinv.food";
        PlayerPrefs.DeleteAll();
        if (File.Exists(saveData))
        {
            File.Delete(saveData);
            File.Delete(saveDataInv1);
            File.Delete(saveDataInv2);
            File.Delete(saveDataInv3);
        }
        
        PlayerPrefs.SetInt("GameStarted", 1);
        PlayerPrefs.SetString("savedLevel", "map");
        SceneManager.LoadScene("map");

    }
    
}