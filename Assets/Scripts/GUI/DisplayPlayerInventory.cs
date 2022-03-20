using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayPlayerInventory : DisplayInventory1
{
    private bool inTrashCan;
    public GameObject trashCan;
    public Inventory equippedInventory;
    public Inventory foodInventory;

    void Start()
    {
        CreateSlots();
        UpdateSlots();
    }

    private void OnEnable()
    {
        UpdateSlots();
        inventory.OnChange += UpdateSlots;
        equippedInventory.OnChange += UpdateSlots;
        foodInventory.OnChange += UpdateSlots;
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        inventory.OnChange -= UpdateSlots;
        equippedInventory.OnChange -= UpdateSlots;
        foodInventory.OnChange -= UpdateSlots;
        Time.timeScale = 1;
    }

    private new void CreateSlots()
    {
        DisplayedItems = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            var obj = Instantiate(slotObject, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<Transform>().position = slotsToPlaceObjects[i].transform.position;
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerDown, delegate { OnPointerDown(obj); });
            DisplayedItems.Add(obj, inventory.slots[i]);
        }
        
        for (int i = 0; i < equippedInventory.slots.Length; i++)
        {
            var obj = Instantiate(slotObject, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<Transform>().position = slotsToPlaceObjects[i + inventory.slots.Length].transform.position;
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerDown, delegate { OnPointerDown(obj); });
            DisplayedItems.Add(obj, equippedInventory.slots[i]);
        }
        
        {
            var obj = Instantiate(slotObject, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<Transform>().position = slotsToPlaceObjects[inventory.slots.Length + equippedInventory.slots.Length].transform.position;
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerDown, delegate { OnPointerDown(obj); });
            DisplayedItems.Add(obj, foodInventory.slots[0]);
        }
    }

    public void OnTrashCanEnter()
    {
        inTrashCan = true;
    }

    public void OnTrashCanExit()
    {
        inTrashCan = false;
    }

    public new void OnDragEnd(GameObject obj)
    {
        if (MouseData.HoveredSlot != null && MouseData.HoveredSlot != MouseData.MouseItem)
        {
            inventory.SwapItems(MouseData.HoveredSlot, MouseData.MouseItem);
        }
        else if (inTrashCan)
        {
            inventory.RemoveItem(DisplayedItems[obj]);
            equippedInventory.RemoveItem(DisplayedItems[obj]);
            foodInventory.RemoveItem(DisplayedItems[obj]);
        }
        
        Destroy(MouseData.MouseObj);
        MouseData.MouseItem = null;
        _currentlyDragging = false;
    }
}