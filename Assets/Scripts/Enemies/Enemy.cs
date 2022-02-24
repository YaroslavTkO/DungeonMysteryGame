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
    public Bar healthBar;
    public float SavedTime;


    //ahead  0
    //upDown 1
    public bool[] wallCollisions = new bool[2];


    public delegate void AttackPlayer(Enemy enemy);

    public static event AttackPlayer Attacked;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        wallCollisions = new bool[2];
        for (int i = 0; i < wallCollisions.Length; i++)
            wallCollisions[i] = false;
        healthBar.SetMaxValueOnBar(hp);
        facingRight = true;
        CurrentState = new EnemyIdle(this);
    }

    void Update()
    {
        CurrentState.Update();
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        healthBar.SetValueOnBar(hp);
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