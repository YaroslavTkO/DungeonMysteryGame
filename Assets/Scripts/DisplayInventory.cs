using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public GameObject InventoryPrefab;
    public Text text;
    public InventoryObject inventory;
    public int xStart;
    public int yStart;
    public int xSpaceBetweenItems;
    public int numberOfColumn;
    public int ySpaceBetweenItems;
    public List<GameObject> inventorySlotes;
    private Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    private void Start()
    {
        CreateDisplay();
    }

    private void Update()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            InventorySlot slot = inventory.Container.Items[i];
            if (itemsDisplayed.ContainsKey(slot))
            {
                itemsDisplayed[slot].GetComponentInChildren<Text>().text =
                    slot.amount.ToString();
            }
            else
            {
                var obj = Instantiate(InventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.transform.GetComponent<Image>().sprite =
                    inventory.database.GetItem[slot.item.Id].uiDisplay;
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<Text>().text = slot.amount == 1?"":slot.amount.ToString();
                itemsDisplayed.Add(slot, obj);
            }
        }
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            InventorySlot slot = inventory.Container.Items[i];
            var obj = Instantiate(InventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.transform.GetComponent<Image>().sprite =
                inventory.database.GetItem[slot.item.Id].uiDisplay;
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<Text>().text = slot.amount == 1?"":slot.amount.ToString();
            itemsDisplayed.Add(slot, obj);
        }
    }

    public Vector3 GetPosition(int i)
    {
        if (i < inventorySlotes.Count)
        {
            return inventorySlotes[i].transform.localPosition;
        }
        return new Vector3(xStart + xSpaceBetweenItems * (i % numberOfColumn),
            yStart + (-ySpaceBetweenItems) * (i / numberOfColumn), 0f);
    }
    
}