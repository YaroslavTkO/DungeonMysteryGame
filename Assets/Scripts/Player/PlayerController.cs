using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    public Joystick joystick;
    public PlayerStats playerStats;
    public Animator _animator;
    public bool _facingRight = true;
    private Rigidbody2D _rigidbody2D;
    private State currentState;
    public bool attackButtonIsPressed;

    private void Start()
    {
        attackButtonIsPressed = false;
        _facingRight = true;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        currentState = new StateIdle(this);
    }
    void Update()
    {
        _rigidbody2D.AddForce(Vector2.zero);
        currentState = currentState.Update();
    }
    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + currentState.FixedUpdate());
    }
    public void Flip()
    {
        _facingRight = !_facingRight;
        transform.localScale =
            new Vector3(transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
    }

    public void AttackButtonIsPressed(bool pressed)
    {
        attackButtonIsPressed = pressed;
    }
}