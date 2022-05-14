using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplayInventory1 : MonoBehaviour
{
    public Inventory inventory;
    public GameObject slotObject;
    public GameObject[] slotsToPlaceObjects;
    public Text descriptionField;
    protected Dictionary<GameObject, InventorySlot> DisplayedItems = new Dictionary<GameObject, InventorySlot>();
    protected InventoryMouseData MouseData = new InventoryMouseData();
    protected bool /*_currentlyDragging = false,*/ ItemChecked = false;

    void Start()
    {
        CreateSlots();
    }

    private void OnEnable()
    {
        UpdateSlots();
        inventory.OnChange += UpdateSlots;
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        inventory.OnChange -= UpdateSlots;
        Time.timeScale = 1;
    }
    public void CreateSlots()
    {
        DisplayedItems = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            var obj = Instantiate(slotObject, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<Transform>().position = slotsToPlaceObjects[i].transform.position;
       //     AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
        //    AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
        //    AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
        //    AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
        //    AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerDown, delegate { OnPointerDown(obj); });
            DisplayedItems.Add(obj, inventory.slots[i]);
        }
    }

    public void UpdateSlots()
    {
        foreach (var slot in DisplayedItems)
        {
            if (slot.Value.item.id == 0)
            {
                slot.Key.GetComponent<Image>().sprite = null;
                slot.Key.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                slot.Key.GetComponentInChildren<Text>().text = "";
            }
            else
            {
                slot.Key.GetComponent<Image>().sprite = slot.Value.item.itemSprite;
                slot.Key.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                slot.Key.GetComponentInChildren<Text>().text =
                    slot.Value.amount == 1 ? "" : slot.Value.amount.ToString();
            }
        }
    }

    /*public void OnEnter(GameObject obj)
    {
        if (DisplayedItems.ContainsKey(obj))
            MouseData.HoveredSlot = DisplayedItems[obj];
    }

    public void OnExit(GameObject obj)
    {
        MouseData.HoveredSlot = null;
    }

    public void OnDragStart(GameObject obj)
    {
        if (_currentlyDragging || DisplayedItems[obj].item.id == 0)
        {
        }
        else
        {
            _currentlyDragging = true;
            var mouseObj = new GameObject();
            var rectTransform = mouseObj.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(80, 80);
            mouseObj.transform.SetParent(transform.parent);
            if (DisplayedItems[obj].item.id > 0)
            {
                var image = mouseObj.AddComponent<Image>();
                image.sprite = DisplayedItems[obj].item.itemSprite;
                image.raycastTarget = false;
            }

            MouseData.MouseObj = mouseObj;
            MouseData.MouseItem = DisplayedItems[obj];
        }
    }

    public void OnDragEnd(GameObject obj)
    {
        if (MouseData.HoveredSlot != null)
        {
            inventory.SwapItems(MouseData.HoveredSlot, MouseData.MouseItem);
        }
        else
        {
            inventory.RemoveItem(DisplayedItems[obj]);
        }

        Destroy(MouseData.MouseObj);
        MouseData.MouseItem = null;
        _currentlyDragging = false;
    }

    public void OnDrag(GameObject obj)
    {
        if (MouseData.MouseObj != null)
        {
            MouseData.MouseObj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }*/

    public void OnPointerDown(GameObject obj)
    {
        if (ItemChecked)
        {
            MouseData.HoveredSlot = DisplayedItems[obj];
            if (MouseData.HoveredSlot != MouseData.MouseItem)
            {
                inventory.SwapItems(MouseData.HoveredSlot, MouseData.MouseItem);
                MouseData.MouseItem = DisplayedItems[obj];
            }
            else ItemChecked = false;
        }
        else if (DisplayedItems[obj].item.id != 0)
        {
            ItemChecked = true;
            MouseData.MouseItem = DisplayedItems[obj];
        }
        descriptionField.text = DisplayedItems.ContainsKey(obj) ? DisplayedItems[obj].item.description : "";
        if (DisplayedItems.ContainsKey(obj) && DisplayedItems[obj].item.id != 0 && ItemChecked)
            foreach (var slot in slotsToPlaceObjects)
            {
                if ((slot.transform.position - obj.transform.position).magnitude <= 1)
                    slot.GetComponent<Image>().color = new Color(1, 0.8f, 0.8f);
                else slot.GetComponent<Image>().color = new Color(1, 1, 1);
            }
        else
            foreach (var slotInv in slotsToPlaceObjects)
                slotInv.GetComponent<Image>().color = new Color(1, 1, 1);

        if (!ItemChecked)
            descriptionField.text = "";
    }

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
}