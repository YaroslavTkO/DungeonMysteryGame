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
            SetEmptySlot(_item, _amount);
            return;
        }
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID == _item.Id)
            {
                Container.Items[i].AddAmount(_amount);
                return;
            }
        }

        SetEmptySlot(_item, _amount);
    }

    public void RemoveItem(Item itemToRemove)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item == itemToRemove)
            {
                Container.Items[i].UpdateSlot(-1, null, 0);
            }
            
        }
    }

    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.ID, item2.item, item2.amount);
        item2.UpdateSlot(item1.ID, item1.item, item1.amount);
        item1.UpdateSlot(temp.ID,temp.item, temp.amount);
    }

    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID <= -1)
            {
                Container.Items[i].UpdateSlot(_item.Id, _item, _amount);
                return Container.Items[i];
            }
            
        }
        //full inventory
        return null;

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
    public InventorySlot[] Items = new InventorySlot[18];
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
    public InventorySlot()
    {
        ID = -1;
        item = null;
        amount = 0;
    }

    public void UpdateSlot(int id, Item item, int amount)
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