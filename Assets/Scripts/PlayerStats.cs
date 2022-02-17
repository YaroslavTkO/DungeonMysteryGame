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

    public float hp = 100;
    public float stamina = 100;
    public float movementSpeed = 5;
    public float damageMultiplier = 1.0f;
    public float experience = 0;
    public States currentState;
}