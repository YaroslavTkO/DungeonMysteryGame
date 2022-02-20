/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplayPlayerInventory
{
    public bool trashCanEntered;
    public InventoryObject equippedInventory;

    private void Start()
    {
        CreateSlots();
    }

    private void OnEnable()
    {
        inventory.OnChange += UpdateSlots;
        equippedInventory.OnChange += UpdateSlots;
    }

    private void OnDisable()
    {
        inventory.OnChange -= UpdateSlots;
        equippedInventory.OnChange -= UpdateSlots;
    }

    private void Update()
    {
       // UpdateSlots();
    }

    private new void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(InventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerDown, delegate { OnPointerDown(obj); });
            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        }

        for (int i = 0; i < equippedInventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(InventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i + inventory.Container.Items.Length);
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerDown, delegate { OnPointerDown(obj); });
            itemsDisplayed.Add(obj, equippedInventory.Container.Items[i]);
        }
    }

    public new void OnDragEnd(GameObject obj)
    {
        if (MouseItem.hoverObj)
        {
            
            
            {
                inventory.SwapItems(itemsDisplayed[obj], itemsDisplayed[MouseItem.hoverObj]);
            }
        }
        else if (trashCanEntered)
        {
            inventory.RemoveItem(itemsDisplayed[obj].item);
            equippedInventory.RemoveItem(itemsDisplayed[obj].item);
        }


        Destroy(MouseItem.obj);
        MouseItem.item = null;
    }

    public void OnEnterTrashCan(GameObject obj)
    {
        trashCanEntered = true;
    }

    public void OnExitTrashCan(GameObject obj)
    {
        trashCanEntered = false;
    }
}*/