using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "InventorySystem/Database")]
public class Database : ScriptableObject, ISerializationCallbackReceiver
{
    public Item[] items;

    public void Serialize()
    {
        for (int i = 0; i < items.Length; i++)
            items[i].id = i;
    }
    private void OnEnable()
    {
        for (int i = 0; i < items.Length; i++)
            items[i].id = i;
    }
    public void OnBeforeSerialize()
    {
    }
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < items.Length; i++)
            items[i].id = i;
    }
}
