using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Items/Food")]
public class FoodObject : ItemObject
{
    public int restoreHealthAmount;
    public int restoreEnergyAmount;
    public void Awake()
    {
        type = ItemType.Food;
    }
}
