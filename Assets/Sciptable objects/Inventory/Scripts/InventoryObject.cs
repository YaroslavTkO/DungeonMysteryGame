using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

[CreateAssetMenu(fileName = "NewInventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    
    public ItemsDatabase database;
    public Inventory Container;
    
    public void AddItem(Item _item, int _amount)
    {
        if (_item.buffs.Length > 0)
        {
            Container.Items.Add(new InventorySlot(_item.Id, _item, _amount));
            return;
        }
        for (int i = 0; i < Container.Items.Count; i++)
        {
            if (Container.Items[i].item.Id == _item.Id)
            {
                Container.Items[i].AddAmount(_amount);
                return;
            }
        }

        Container.Items.Add(new InventorySlot(_item.Id, _item, _amount));
    }

    public void Save()
    {
        string saveData = JsonUtility.ToJson(Container, true);
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create($"{Application.persistentDataPath}{savePath}");
        formatter.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists($"{Application.persistentDataPath}{savePath}"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open($"{Application.persistentDataPath}{savePath}", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(file).ToString(), Container);
            file.Close();
        }
    }

}

[System.Serializable]
public class Inventory
{
    public List<InventorySlot> Items = new List<InventorySlot>();
}
[System.Serializable]
public class InventorySlot
{
    public int ID;
    public Item item;
    public int amount;

    public InventorySlot(int id, Item item, int amount)
    {
        ID = id;
        this.item = item;
        this.amount = amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}