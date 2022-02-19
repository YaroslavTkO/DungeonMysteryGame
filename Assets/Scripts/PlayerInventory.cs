using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject equippedInventory;
    public GameObject inventoryGUI;
    private void OnTriggerEnter2D(Collider2D col)
    {
        var item = col.GetComponent<GroundItem>();
        if (item)
        {
            inventory.AddItem(new Item(item.item), 1);
            if (!inventory.isFull)
                Destroy(col.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            inventoryGUI.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            inventory.Save();
            equippedInventory.Save();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            inventory.Load();
            equippedInventory.Load();
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[18];
        equippedInventory.Container.ClearInventory();
    }
}