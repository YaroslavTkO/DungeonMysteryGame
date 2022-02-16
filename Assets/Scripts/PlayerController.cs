using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    private Animator _animator;
    private bool _facingRight = true;
    private float _savedRollStartTime;
    private float _savedAttackStartTime;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        switch (playerStats.currentState)
        {
            case PlayerStats.States.Idle:
                transform.Translate(HandleInput() * playerStats.movementSpeed * Time.deltaTime);
                _animator.SetBool("Running", false);
                break;
            case PlayerStats.States.Walking:
                transform.Translate(HandleInput() * playerStats.movementSpeed * Time.deltaTime);
                _animator.SetBool("Running", true);
                break;
            case PlayerStats.States.Rolling:
                transform.Translate( new Vector2(_facingRight?1:-1, 0)*playerStats.movementSpeed * Time.deltaTime);
                Roll();
                break;
            case PlayerStats.States.Attacking:
                Attack();
                break;
                
        }
    }

    private Vector2 HandleInput()
    {
        
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        playerStats.currentState = input == Vector2.zero
            ? PlayerStats.States.Idle
            : playerStats.currentState = PlayerStats.States.Walking;

        if (_facingRight && input.x < 0)
        {
            Flip();
        }
        else if (!_facingRight && input.x > 0)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerStats.currentState = PlayerStats.States.Attacking;
            _animator.SetTrigger("Attack");
            _savedAttackStartTime = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            playerStats.currentState = PlayerStats.States.Rolling;
            _animator.SetTrigger("Roll");
            _savedRollStartTime = Time.time;
        }

        return input.normalized;
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.localScale =
            new Vector3(transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
    }

    private void Attack()
    {
        if (Time.time - _savedAttackStartTime > _animator.GetCurrentAnimatorStateInfo(0).length - 0.25)
        {
            playerStats.currentState = PlayerStats.States.Walking;
        }
    }

    private void Roll()
    {
        if (Time.time - _savedRollStartTime > _animator.GetCurrentAnimatorStateInfo(0).length)
        {
            playerStats.currentState = PlayerStats.States.Walking;
        }
    }
}