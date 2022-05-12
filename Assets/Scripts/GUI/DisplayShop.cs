using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        if (itemToBuy == DisplayedItems[obj].item)
        {
            ItemChecked = false;
            descriptionField.text = "";
            itemToBuy = null;
        }
        else
        {
            ItemChecked = true;
            descriptionField.text = DisplayedItems.ContainsKey(obj) ? DisplayedItems[obj].item.description : "";
            itemToBuy = DisplayedItems[obj].item;
        }

        if (ItemChecked)
            foreach (var slot in slotsToPlaceObjects)
            {
                if ((slot.transform.position - obj.transform.position).magnitude <= 1)
                    slot.GetComponent<Image>().color = new Color(1, 0.8f, 0.8f);
                else slot.GetComponent<Image>().color = new Color(1, 1, 1);
            }
        else
            foreach (var slot in slotsToPlaceObjects)
                slot.GetComponent<Image>().color = new Color(1, 1, 1);
    }
}