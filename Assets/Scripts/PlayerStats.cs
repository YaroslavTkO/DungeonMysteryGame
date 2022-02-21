using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public enum States
    {
        Idle,
        Walking,
        Attacking
    }

    public float maxHpWithoutBuffs = 100;
    public float maxHp = 100;
    public float hp = 100;
    public float maxStaminaWithoutBuffs = 100;
    public float maxStamina = 100;
    public float stamina = 100;
    public float movementSpeedWithoutBuffs = 5;
    public float movementSpeed = 5;
    public float exhaustedMovementSpeed = 2;
    public float damageMultiplierWithoutBuffs;
    public float damageMultiplier = 1.0f;
    public float experience = 0;
    public States currentState;
    public HealthBar healthBar;
    public HealthBar staminaBar;
    public PlayerInventory inventory;

    private void Start()
    {
        healthBar.SetMaxValueOnBar(hp);
        staminaBar.SetMaxValueOnBar(stamina);
    }

    private void OnEnable()
    {
        inventory.equippedInventory.OnChange += EquipInventoryBuffs;
        inventory.inventory.OnChange += EquipInventoryBuffs;
    }

    private void OnDisable()
    {
        inventory.equippedInventory.OnChange -= EquipInventoryBuffs;
        inventory.inventory.OnChange -= EquipInventoryBuffs;
    }

    private void LateUpdate()
    {
        healthBar.SetValueOnBar(hp);
        staminaBar.SetValueOnBar(stamina);
    }

    public void EquipInventoryBuffs()
    {
        Debug.Log("Method Called");
        maxHp = maxHpWithoutBuffs;
        maxStamina = maxStaminaWithoutBuffs;
        if (hp > maxHpWithoutBuffs)
            hp = maxHpWithoutBuffs;
        if (stamina > maxStaminaWithoutBuffs)
            stamina = maxStaminaWithoutBuffs;
        if (movementSpeed > movementSpeedWithoutBuffs)
            movementSpeed = movementSpeedWithoutBuffs;
        if (damageMultiplier > damageMultiplierWithoutBuffs)
            damageMultiplier = damageMultiplierWithoutBuffs;
        foreach (var slot in inventory.equippedInventory.slots)
        {
            if (slot.item.id == 0)
                continue;
            foreach (var boost in slot.item.boosts)
            {
                switch (boost.boostType)
                {
                    case Boost.Speed:
                        movementSpeed += boost.value;
                        break;
                    case Boost.AttackPower:
                        damageMultiplier += boost.value;
                        break;
                    case Boost.MaxHealth:
                        maxHp += boost.value;
                        healthBar.SetOnlyMaxValueOnBar(maxHp);
                        break;
                    case Boost.MaxStamina:
                        maxStamina += boost.value;
                        staminaBar.SetOnlyMaxValueOnBar(maxStamina);
                        break;
                }
            }
        }
    }
}