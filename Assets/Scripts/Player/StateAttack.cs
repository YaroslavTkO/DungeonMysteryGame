using UnityEngine;

public class StateAttack : State
{
    public float _savedAttackStartTime;

    public StateAttack(PlayerController controller)
    {
        Controller = controller;
        Controller.playerStats.ChangeStaminaValue(-10);
        Controller._animator.SetTrigger("Attack");
        _savedAttackStartTime = Time.time;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(controller.playerStats.attackPoint.position,
            controller.playerStats.attackRange, controller.playerStats.enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            var En = enemy.GetComponent<Enemy>();
            if (En)
                En.TakeDamage(controller.playerStats.damage);
        }
    }

    public override State Update()
    {
        return Time.time - _savedAttackStartTime > Controller._animator.GetCurrentAnimatorStateInfo(0).length / 2
            ? ChangeState(new StateIdle(Controller))
            : this;
    }

    public override Vector2 HandleInput()
    {
        return Vector2.zero;
    }

    public override State ChangeState(State state)
    {
        return state;
    }

    public override Vector2 FixedUpdate()
    {
        return Vector2.zero;
    }
}