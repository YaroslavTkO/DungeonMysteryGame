using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private string saveFileName = "playerStats.aaa";

    public float maxHpWithoutBuffs = 100;
    public float maxHp = 100;
    public float hp = 100;
    
    public float maxStaminaWithoutBuffs = 100;
    public float maxStamina = 100;
    public float stamina = 100;
    
    public float movementSpeedWithoutBuffs = 5;
    public float movementSpeed = 5;
    public float exhaustedMovementSpeed = 2;
    
    public float damageWithoutBuffs;
    public float damage = 1.0f;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;

    public float invincibilityTime = 1;
    public float savedStartInvincibilityTime;
    
    public float experience = 0;
    public int currentLevel = 1;

    public int money = 0;
    
    [SerializeField]private Bar healthBar;
    [SerializeField]private Bar staminaBar;
    public PlayerInventory inventory;

    private Transform attackPointCopy;
    private Bar healthBarCopy;
    private Bar staminaBarCopy;
    private PlayerInventory inventoryCopy;

    private void Start()
    {
        
        Load();

        BaseEnemy.Attacked += TakeDamage;
        Enemy.Killed += KilledEnemy;
        healthBar.SetMaxValueOnBar(hp);
        staminaBar.SetMaxValueOnBar(stamina); 
        
    }

    private void OnEnable()
    {
        UpdateInventoryBuffs();
        inventory.equippedInventory.OnChange += UpdateInventoryBuffs;
        inventory.inventory.OnChange += UpdateInventoryBuffs;
    }

    private void OnDisable()
    {
        inventory.equippedInventory.OnChange -= UpdateInventoryBuffs;
        inventory.inventory.OnChange -= UpdateInventoryBuffs;
        BaseEnemy.Attacked -= TakeDamage;
        Enemy.Killed -= KilledEnemy;
    }

    public bool ChangeMoney(int amount)
    {
        if (money + amount < 0)
            return false;
        money += amount;
        return true;
    }
    public void ChangeHpValue(float changeValue)
    {
        if (hp + changeValue < 0)
        {
            hp = 0;
        }
        else if (hp + changeValue > maxHp)
        {
            hp = maxHp;
        }
        else hp += changeValue;
        healthBar.SetValueOnBar(hp);
    }

    public void ChangeStaminaValue(float changeValue)
    {
        if (stamina + changeValue < 0)
        {
            stamina = 0;
        }
        else if (stamina + changeValue > maxStamina)
        {
            stamina = maxStamina;
        }
        else stamina += changeValue;
        staminaBar.SetValueOnBar(stamina);
    }

    public void UpdateInventoryBuffs()
    {
        maxHp = maxHpWithoutBuffs;
        maxStamina = maxStaminaWithoutBuffs;
        if (hp > maxHpWithoutBuffs)
            hp = maxHpWithoutBuffs;
        if (stamina > maxStaminaWithoutBuffs)
            stamina = maxStaminaWithoutBuffs;
        movementSpeed = movementSpeedWithoutBuffs;
        damage = damageWithoutBuffs;
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
                        damage += boost.value;
                        break;
                    case Boost.MaxHealth:
                        maxHp += boost.value;
                        break;
                    case Boost.MaxStamina:
                        maxStamina += boost.value;
                        
                        break;
                }
            }
        }
        staminaBar.SetOnlyMaxValueOnBar(maxStamina);
        healthBar.SetOnlyMaxValueOnBar(maxHp);
    }

    public void LevelUp(int type)
    {
        if (experience >= currentLevel * 15)
        {
            experience -= currentLevel * 15;
            currentLevel++;
            switch (type)
            {
                case 1: maxHpWithoutBuffs += 10;
                    break;
                case 2: maxStaminaWithoutBuffs += 10;
                    break;
                case 3: movementSpeedWithoutBuffs += 0.1f;
                    break;
                case 4: damageWithoutBuffs += 1;
                    break;
            }
            UpdateInventoryBuffs();
        }

    }

    public void AddExperience(float amount)
    {
        experience += amount;
    }

    public void TakeDamage(BaseEnemy enemy)
    {
        if (Time.time - savedStartInvincibilityTime > invincibilityTime)
        {
            ChangeHpValue(-enemy.damage);
            savedStartInvincibilityTime = Time.time;
        }


    }

    public void KilledEnemy(int money, int exp)
    {
        ChangeMoney(money);
        AddExperience(exp);
    }
    
    public void Save()
    {
        //string saveData = JsonConvert.SerializeObject(this, Formatting.None, jsSettings);
        var saveData = JsonUtility.ToJson(this, true);
        var formatter = new BinaryFormatter();
        var file = File.Create($"{Application.persistentDataPath}/{saveFileName}");
        formatter.Serialize(file, saveData);
        file.Close();
        inventory.Save();
    }

    public void Load()
    {
        attackPointCopy = attackPoint;
        healthBarCopy = healthBar;
        staminaBarCopy = staminaBar;
        inventoryCopy = inventory;
        var path = Application.persistentDataPath + "/" + saveFileName;
        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var file = File.Open($"{Application.persistentDataPath}/{saveFileName}", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(file).ToString(), this);
            file.Close();
            attackPoint = attackPointCopy;
            healthBar = healthBarCopy;
            staminaBar = staminaBarCopy;
            inventory = inventoryCopy;
            inventory.Load();
            
            
            staminaBar.SetOnlyMaxValueOnBar(maxStamina);
            healthBar.SetOnlyMaxValueOnBar(maxHp);
            healthBar.SetValueOnBar(hp);
            staminaBar.SetValueOnBar(stamina);
            
        }
    }
    
}