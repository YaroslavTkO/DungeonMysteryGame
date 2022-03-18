using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayShop : DisplayInventory1
{
    public Item itemToBuy;

    private void Start()
    {
        CreateSlots();
        UpdateSlots();
    }

    private new void CreateSlots()
    {
        DisplayedItems = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            var obj = Instantiate(slotObject, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<Transform>().position = slotsToPlaceObjects[i].transform.position;
            AddEvent(obj, EventTriggerType.PointerDown, delegate { OnPointerDown(obj); });
            DisplayedItems.Add(obj, inventory.slots[i]);
        }
    }
    private new void OnPointerDown(GameObject obj)
    {
        descriptionField.text = DisplayedItems.ContainsKey(obj) ? DisplayedItems[obj].item.description : "";
        itemToBuy = DisplayedItems[obj].item;
    }
    
}