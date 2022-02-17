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

    public float maxHp = 100;
    public float hp = 100;
    public float maxStamina = 100;
    public float stamina = 100;
    public float movementSpeed = 5;
    public float exhaustedMovementSpeed = 2;
    public float damageMultiplier = 1.0f;
    public float experience = 0;
    public States currentState;
    public HealthBar healthBar;
    public HealthBar staminaBar;

    private void Start()
    {
        healthBar.SetMaxHealthOnHealthBar(hp);
        staminaBar.SetMaxHealthOnHealthBar(stamina);
    }

    private void LateUpdate()
    {
        healthBar.SetHealthOnHealthBar(hp);
        staminaBar.SetHealthOnHealthBar(stamina);
    }
}