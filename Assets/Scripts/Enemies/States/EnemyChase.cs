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
        var origin = Enemy.gameObject.transform.position;
        var target = Enemy.player.transform.position;
        var direction = target - origin;
        var hits = new RaycastHit2D[10];
        var times = Physics2D.RaycastNonAlloc(origin, direction, hits, 1.5f);
        var moveAmount = Enemy.runningMovementSpeed * Time.deltaTime;
        for (int i = 0; i < times; i++)
        {
            if (hits[i].collider.gameObject.CompareTag("Wall"))
            {
                moveAmount = 0;
                break;
            }
        }
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