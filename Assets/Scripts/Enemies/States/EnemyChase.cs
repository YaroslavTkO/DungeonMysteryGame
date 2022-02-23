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
        var moveAmount = Enemy.runningMovementSpeed * Time.deltaTime;
        Enemy.transform.position = Vector3.MoveTowards(Enemy.gameObject.transform.position, Enemy.player.transform.position, moveAmount);
        if (Enemy.facingRight && Enemy.gameObject.transform.position.x > Enemy.player.transform.position.x)
            Flip();
        else if (!Enemy.facingRight && Enemy.gameObject.transform.position.x < Enemy.player.transform.position.x)
            Flip();
    }

    public override void OnTriggerEnter()
    {
    }
}
