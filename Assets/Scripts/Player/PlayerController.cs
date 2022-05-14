using System;
using System.Collections;
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
    private IEnumerator StaminaCor;

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
        if (playerStats.healthRegenChanged)
        {
            playerStats.healthRegenChanged = false;
            StopCoroutine(nameof(HpChange));
            StartCoroutine(HpChange());
        }
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

    IEnumerator StaminaChange(float amount)
    {
        while (true)
        {
            //  Debug.Log("Amount: " + amount);
            if (Time.timeScale != 0)
                playerStats.ChangeStaminaValue(amount);
            yield return new WaitForSeconds(0.005f);
        }
    }

    IEnumerator HpChange()
    {
        while (playerStats.HealthRegen > 0.000001 || playerStats.HealthRegen < -0.000001)
        {
            if (Time.timeScale != 0)
                playerStats.ChangeHpValue(playerStats.HealthRegen);
            yield return new WaitForSeconds(0.0025f);
        }
    }

    public void CoroutineStamina(float amount)
    {
        if (StaminaCor != null)
            StopCoroutine(StaminaCor);
        StaminaCor = StaminaChange(amount);
        StartCoroutine(StaminaCor);
    }
}