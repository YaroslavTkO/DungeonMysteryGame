using UnityEngine;

public enum ItemType
{
    Default,
    Food,
    Weapon,
    Ring,
    Helmet,
    Armor,
    Loot
}
public enum Boosts{
    SpeedBoost,
    DefenceBoost,
    AttackBoost,
    HealthBoost,
    EnergyBoost

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
    public ItemType type;
    public ItemBuff[] buffs;

    public Item(ItemObject obj)
    {
        Id = obj.Id;
        Name = obj.name;
        type = obj.type;
        buffs = new ItemBuff[obj.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(obj.buffs[i].value);
            buffs[i].boost = obj.buffs[i].boost;
        }
    }
    public Item()
    {
        Id = -1;
        Name = null;
        type = ItemType.Default;

    }
}
[System.Serializable]
public class ItemBuff
{
    public Boosts boost;
    public float value;

    public ItemBuff(float value)
    {
        this.value = value;
    }


    
}
