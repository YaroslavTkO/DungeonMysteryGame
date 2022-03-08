using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public int damage;
    
    public delegate void AttackPlayer(BaseEnemy enemy);

    public static event AttackPlayer Attacked;
    
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            Attacked?.Invoke(this);
    }
}
