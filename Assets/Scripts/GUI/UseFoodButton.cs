using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseFoodButton : MonoBehaviour
{
    public Inventory foodInventory;
    public PlayerStats playerStats;

    public void ButtonClicked()
    {
        if (foodInventory.slots[0].item.id > 0)
        {
            foreach (var buff in foodInventory.slots[0].item.boosts)
            {
                if (buff.boostType == Boost.HealAmount)
                    playerStats.ChangeHpValue(buff.value);
                else if (buff.boostType == Boost.StaminaHealAmount)
                    playerStats.ChangeStaminaValue(buff.value);
            }
            foodInventory.slots[0].ChangeQuantity(-1, foodInventory.database);
        }
    }
}