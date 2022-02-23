using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool facingRight;
    public float hp;
    public int damage;
    public float movementSpeed;
    public float runningMovementSpeed;
    public float timeToChangeDirection;
    public EnemyState CurrentState;
    public Animator animator;
    public GameObject player;

    public delegate void AttackPlayer(Enemy enemy);

    public static event AttackPlayer Attacked;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        CurrentState = new EnemyIdle(this);
    }
    void Update()
    {
        CurrentState.Update();
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
            Destroy(gameObject);
    }

    public void OnChildTriggerEnter(GameObject _player)
    {
        player = _player;
        CurrentState = CurrentState.ChangeState(new EnemyChase(this));
    }

    public void OnChildTriggerExit()
    {
        player = null;
        CurrentState = CurrentState.ChangeState(new EnemyIdle(this));
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            Attacked?.Invoke(this);
    }
}