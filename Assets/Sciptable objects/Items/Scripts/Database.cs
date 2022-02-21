using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "InventorySystem/Database")]
public class Database : ScriptableObject, ISerializationCallbackReceiver
{
    public Item[] items;

    public void OnBeforeSerialize()
    {
        
    }
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].id = i;
        }
    }
}
