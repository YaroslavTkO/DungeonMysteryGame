using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

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
                    slot.ChangeQuantity(1);
                    return true;
                }
            }
        }

        foreach (var slot in slots)
        {
            if (slot.item.id == 0)
            {
                slot.item = item;
                slot.ChangeQuantity(1);
                return true;
            }
        }

        return false;
    }

    public void SwapItems(InventorySlot slot1, InventorySlot slot2)
    {
        InventorySlot temp = new InventorySlot(slot2.item, slot2.amount);
        slot2.UpdateSlot(slot1.item, slot1.amount);
        slot1.UpdateSlot(temp.item, temp.amount);
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
    }

    public void ClearInventory()
    {
        foreach (var slot in slots)
        {
            slot.ClearSlot(database);
        }
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
        var path = Application.persistentDataPath + "/" + saveFileName;
        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var file = File.Open($"{Application.persistentDataPath}/{saveFileName}", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(file).ToString(), this);
            file.Close();
        }
    }
}