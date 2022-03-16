using UnityEngine;

public class EnemyChase : EnemyState
{
    public EnemyChase(Enemy enemy)
    {
        Enemy = enemy;
        Enemy.animator.SetBool("speedIsZero", false);
    }

    public override void Update()
    {
        var origin = Enemy.gameObject.transform.position;
        var target = Enemy.player.transform.position;
        var direction = target - origin;
        RaycastHit2D hit;
        hit = Physics2D.Raycast(origin, direction, 1.3f, Enemy.wallLayer);
        var moveAmount = Enemy.runningMovementSpeed * Time.deltaTime;
        if (hit)
            moveAmount = 0;
        Enemy.animator.SetBool("speedIsZero", moveAmount == 0);
        Enemy.transform.position = Vector3.MoveTowards(origin,
            Enemy.player.transform.position, moveAmount);
        if (Enemy.facingRight && origin.x > target.x)
            Flip();
        else if (!Enemy.facingRight && origin.x < target.x)
            Flip();
    }

    public override void OnTriggerEnter()
    {
    }
}