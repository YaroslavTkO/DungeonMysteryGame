using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory inventory;
    public Inventory equippedInventory;
    public Inventory foodInventory;

    private void OnTriggerEnter2D(Collider2D col)
    {
        var item = col.GetComponent<GroundItem>();
        if (item)
        {
            if (inventory.AddItem(item.item))
                Destroy(col.gameObject);
        }
    }

    public void Save()
    {
        foodInventory.Save();
        inventory.Save();
        equippedInventory.Save();
    }

    public void Load()
    {
        inventory.ClearInventory();
        equippedInventory.ClearInventory();
        foodInventory.ClearInventory();
        foodInventory.Load();
        inventory.Load();
        equippedInventory.Load();
    }

    private void OnApplicationQuit()
    {
        inventory.ClearInventory();
        equippedInventory.ClearInventory();
        foodInventory.ClearInventory();
    }
}