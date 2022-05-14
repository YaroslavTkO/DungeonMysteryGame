using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "InventorySystem/CreateItem")]
public class Item : ScriptableObject
{
    public int id;
    public Sprite itemSprite;
    public ItemType type;
    public bool isStakable;
    public List<ItemBoost> boosts;
    public int price;
    [TextArea(15,20)]
    public string description;
    
}

[System.Serializable]
public class ItemBoost
{
    public Boost boostType;
    public float value;
}

public enum ItemType
{
    Emty,
    Loot,
    Food,
    Helmet,
    Weapon,
    Armor,
    Ring
}

public enum Boost
{
    MaxStamina,
    MaxHealth,
    HealAmount,
    StaminaHealAmount,
    Speed,
    AttackPower,
    AttackSpeed,
    StaminaRegeneration,
    AttackLength,
    AttackStaminaUsage,
    HealthRegeneration,
    
    
}
