using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public enum ItemType
{
    Food,
    Equipment,
    Default
}
public enum Attributes{
    Agility,
    Intellect,
    Stamina,
    Strength

}
public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public Sprite uiDisplay;
    public ItemType type;
    [TextArea(15,20)]
    public string description;

    public ItemBuff[] buffs;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }


}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public ItemBuff[] buffs;

    public Item(ItemObject obj)
    {
        Id = obj.Id;
        Name = obj.name;
        buffs = new ItemBuff[obj.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(obj.buffs[i].min, obj.buffs[i].max);
            buffs[i].attribute = obj.buffs[i].attribute;
        }
    }
}
[System.Serializable]
public class ItemBuff
{
    public Attributes attribute;
    public int value;
    public int min;
    public int max;

    public ItemBuff(int min, int max)
    {
        this.max = max;
        this.min = min;
        GenerateValue();
    }

    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }
    
}
