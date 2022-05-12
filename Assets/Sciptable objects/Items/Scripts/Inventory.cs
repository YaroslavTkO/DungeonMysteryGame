using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Inventory", menuName = "InventorySystem/Inventory")]
public class Inventory : ScriptableObject
{
    public Database database;
    public string saveFileName;
    public InventorySlot[] slots;

    public delegate void InventoryChange();

    public event InventoryChange OnChange;
    public bool AddItem(Item item)
    {
        if (item.isStakable)
        {
            foreach (var slot in slots)
            {
                if (item.id == slot.item.id)
                {
                    slot.ChangeQuantity(1, database);
                    OnChange?.Invoke();
                    return true;
                }
            }
        }

        foreach (var slot in slots)
        {
            if (slot.item.id == 0)
            {
                slot.item = item;
                slot.ChangeQuantity(1, database);
                OnChange?.Invoke();
                return true;
            }
        }

        return false;
    }

    public void SwapItems(InventorySlot slot1, InventorySlot slot2)
    {
        if (slot1 != null && slot2 != null)
        {
            if (slot1.item.id == slot2.item.id && slot1.item.isStakable)
            {
                slot1.ChangeQuantity(slot2.amount, database);
                slot2.ClearSlot(database);
                OnChange?.Invoke();
            }
            else if (slot1.CanPlaceInSlot(slot2.item) &&
                     (slot1.item.id == 0 || slot2.CanPlaceInSlot(slot1.item)))
            {
                InventorySlot temp = new InventorySlot(slot2.item, slot2.amount);
                slot2.UpdateSlot(slot1.item, slot1.amount);
                slot1.UpdateSlot(temp.item, temp.amount);
                OnChange?.Invoke();
            }
        }
    }

    public void RemoveItem(InventorySlot item)
    {
        foreach (var slot in slots)
        {
            if (slot == item)
            {
                slot.ClearSlot(database);
            }
        }

        OnChange?.Invoke();
    }

    public void ClearInventory()
    {
        foreach (var slot in slots)
        {
            slot.ClearSlot(database);
        }

        OnChange?.Invoke();
    }

    public void Save()
    {
        var saveData = JsonUtility.ToJson(this, true);
        var formatter = new BinaryFormatter();
        var file = File.Create($"{Application.persistentDataPath}/{saveFileName}");
        formatter.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        ClearInventory();
        var copyDatabase = database;
        var path = Application.persistentDataPath + "/" + saveFileName;
        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var file = File.Open($"{Application.persistentDataPath}/{saveFileName}", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(file).ToString(), this);
            file.Close();
            database = copyDatabase;

        }
        else
        {
            database = copyDatabase;
            ClearInventory();
        }

        foreach (var slot in slots)
        {
            if (!slot.item)
                slot.item = database.items[0];

        }
        OnChange?.Invoke();
    }
}