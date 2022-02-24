using UnityEngine;

public class WalkingState : State
{
    public WalkingState(PlayerController controller)
    {
        Controller = controller;
        Controller._animator.SetBool("Running", true);
    }
    public override State Update()
    {
        if ((Controller.attackButtonIsPressed || Input.GetKeyDown(KeyCode.Mouse1) ) && Controller.playerStats.stamina >= 20)
            return ChangeState(new StateAttack(Controller));
        Controller.playerStats.ChangeStaminaValue(-0.01f);
        _direction = HandleInput();
        return _direction == Vector2.zero ? ChangeState(new StateIdle(Controller)) : this;
    }

    public override Vector2 HandleInput()
    {
        Vector2 input = new Vector2(Controller.joystick.Horizontal + Input.GetAxisRaw("Horizontal"),Controller.joystick.Vertical + Input.GetAxisRaw("Vertical"));
        if (Controller._facingRight && input.x < 0)
        {
            Controller.Flip();
        }
        else if (!Controller._facingRight && input.x > 0)
        {
            Controller.Flip();
        }

        return input.normalized;
    }

    public override State ChangeState(State state)
    {
        return state;
    }

    public override Vector2 FixedUpdate()
    {
        return Controller.playerStats.stamina > 20
            ? _direction * Controller.playerStats.movementSpeed * Time.deltaTime
            : _direction * Controller.playerStats.exhaustedMovementSpeed * Time.deltaTime;
    }
}