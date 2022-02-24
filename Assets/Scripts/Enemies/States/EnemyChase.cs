using UnityEngine;

public class EnemyChase : EnemyState
{
    public float SavedTime;

    public EnemyChase(Enemy enemy)
    {
        Enemy = enemy;
        Enemy.animator.SetBool("speedIsZero", false);
    }

    public override void Update()
    {
        if (Enemy.wallCollisions[0])
        {
            var vector = new Vector2(Enemy.transform.position.x < Enemy.player.transform.position.x ? -1 : 1, 0);
            if (vector == Vector2.left && Enemy.facingRight)
                Flip();
            else if (vector == Vector2.right && !Enemy.facingRight)
                Flip();
            Enemy.transform.Translate(vector * Enemy.movementSpeed * Time.deltaTime);
        }

        if (Enemy.wallCollisions[1])
        {
            Enemy.transform.Translate(
                new Vector2(0, Enemy.transform.position.y < Enemy.player.transform.position.y ? -1 : 1) *
                Enemy.movementSpeed * Time.deltaTime);
        }
        if (!Enemy.wallCollisions[0] && !Enemy.wallCollisions[1])
        {
            var moveAmount = Enemy.runningMovementSpeed * Time.deltaTime;
            Enemy.transform.position = Vector3.MoveTowards(Enemy.gameObject.transform.position,
                Enemy.player.transform.position, moveAmount);
            if (Enemy.facingRight && Enemy.gameObject.transform.position.x > Enemy.player.transform.position.x)
                Flip();
            else if (!Enemy.facingRight && Enemy.gameObject.transform.position.x < Enemy.player.transform.position.x)
                Flip();
        }

        if (Time.time - Enemy.SavedTime > 0.8)
        {
            Enemy.wallCollisions[0] = false;
            Enemy.wallCollisions[1] = false;
        }
    }

    public override void OnTriggerEnter()
    {
    }
}