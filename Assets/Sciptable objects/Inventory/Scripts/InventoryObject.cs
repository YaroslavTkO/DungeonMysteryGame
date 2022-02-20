/*using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEditor.Timeline.Actions;

[CreateAssetMenu(fileName = "NewInventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    
    public ItemsDatabase database;
    public Inventory Container;
    public bool isFull = false;
    public delegate void InventoryChange();
    public event InventoryChange OnChange;
    

    public void AddItem(Item _item, int _amount)
    {
        
        isFull = false;
        if (_item.buffs.Length > 0)
        {
            SetEmptySlot(_item, _amount);
            OnChange?.Invoke();
            return;
        }
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item.Id == _item.Id)
            {
                Container.Items[i].AddAmount(_amount);
                OnChange?.Invoke();
                return;
            }
        }

        SetEmptySlot(_item, _amount);
        OnChange?.Invoke();
    }

    public void RemoveItem(Item itemToRemove)
    {
        
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item == itemToRemove)
            {
                Container.Items[i].UpdateSlot(null, 0);
                OnChange?.Invoke();
            }
            
        }
    }

    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        if (item2.CheckAllowedTypeMatch(item1.item) &&
            (item2.item.Id <= -1 || item1.CheckAllowedTypeMatch(item2.item)))
        {
            InventorySlot temp = new InventorySlot(item2.item, item2.amount);
            item2.UpdateSlot(item1.item, item1.amount);
            item1.UpdateSlot(temp.item, temp.amount);
            OnChange?.Invoke();
        }
    }

    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item.Id <= -1)
            {
                Container.Items[i].UpdateSlot(_item, _amount);
                OnChange?.Invoke();
                return Container.Items[i];
            }
            
        }
        OnChange?.Invoke();
        isFull = true;
        
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
        OnChange?.Invoke();
    }

}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[18];
    public void ClearInventory()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].amount = 0;
            Items[i].item = new Item();

        }
    }
}
[System.Serializable]
public class InventorySlot
{
    public ItemType AllowedType = 0;
    public Item item;
    public int amount;
    

    public InventorySlot(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
    public InventorySlot()
    {
        item = null;
        amount = 0;
    }

    public void UpdateSlot(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

    public bool CheckAllowedTypeMatch(Item item)
    {
        return AllowedType == ItemType.Default || item.type == AllowedType;

    }

    
}*/