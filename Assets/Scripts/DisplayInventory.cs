using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public abstract class DisplayInventory : MonoBehaviour
{
    public InventoryMouseInteraction MouseItem;
    
    public GameObject InventoryPrefab;
    public InventoryObject inventory;
    public List<GameObject> inventorySlots;
    protected Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

    private void Start()
    {
        CreateSlots();
    }

    private void Update()
    {
        UpdateSlots();
    }

    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> slot in itemsDisplayed)
        {
            if (slot.Value.ID >= 0)
            {
                slot.Key.GetComponent<Image>().sprite = inventory.database.GetItem[slot.Value.item.Id].uiDisplay;
                slot.Key.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                slot.Key.GetComponentInChildren<Text>().text = slot.Value.amount == 1 ? "" : slot.Value.amount.ToString();
            }
            else
            {
                slot.Key.GetComponent<Image>().sprite = null;
                slot.Key.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                slot.Key.GetComponentInChildren<Text>().text = "";
            }

        }
    }


    public void CreateSlots()
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
            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
            

        }
    }

    public void OnEnter(GameObject obj)
    {
        MouseItem.hoverObj = obj;
        if (itemsDisplayed.ContainsKey(obj))
        {
            MouseItem.hoverItem = itemsDisplayed[obj];
        }
    }
    public void OnExit(GameObject obj)
    {
        MouseItem.hoverObj = null;
        MouseItem.hoverItem = null;

    }
    public void OnDragStart(GameObject obj)
    {
        var mouseObj = new GameObject();
        var rectTransform = mouseObj.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(35, 35);
        mouseObj.transform.SetParent(transform.parent);
        if (itemsDisplayed[obj].ID >= 0)
        {
            var image = mouseObj.AddComponent<Image>();
            image.sprite = inventory.database.GetItem[itemsDisplayed[obj].ID].uiDisplay;
            image.raycastTarget = false;
        }

        MouseItem.obj = mouseObj;
        MouseItem.item = itemsDisplayed[obj];
    }
    public void OnDragEnd(GameObject obj)
    {
        if (MouseItem.hoverObj)
        {
            inventory.SwapItems(itemsDisplayed[obj], itemsDisplayed[MouseItem.hoverObj]);
        }
        else
        {
            inventory.RemoveItem(itemsDisplayed[obj].item);
        }
        Destroy(MouseItem.obj);
        MouseItem.item = null;
    }
    public void OnDrag(GameObject obj)
    {
        if (MouseItem.obj != null)
        {
            MouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public Vector3 GetPosition(int i)
    {
        return inventorySlots[i].transform.localPosition;
    }

    
}

