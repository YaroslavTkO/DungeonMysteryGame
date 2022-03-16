using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyState
{
    public float SavedTime;
    private int xRand;
    private int yRand;

    public EnemyIdle(Enemy enemy)
    {
        Enemy = enemy;
    }

    public override void Update()
    {
        if (Time.time - SavedTime > Enemy.timeToChangeDirection)
        {
            SavedTime = Time.time;
            xRand = Random.Range(-1, 2);
            yRand = Random.Range(-1, 2);
        }

        var origin = Enemy.gameObject.transform.position;
        
        if (Physics2D.Raycast(origin, Vector2.up, 0.7f, Enemy.wallLayer))
            yRand = Random.Range(-1, 1);
        if (Physics2D.Raycast(origin, Vector2.down, 0.7f, Enemy.wallLayer))
            yRand = Random.Range(0, 2);
        if (Physics2D.Raycast(origin, Vector2.left, 0.7f, Enemy.wallLayer))
            xRand = Random.Range(0, 2);
        if (Physics2D.Raycast(origin, Vector2.right, 0.7f, Enemy.wallLayer))
            xRand = Random.Range(-1, 1);

        if (xRand == 0 && yRand == 0)
            Enemy.animator.SetBool("speedIsZero", true);
        else Enemy.animator.SetBool("speedIsZero", false);
        var moveAmount = new Vector2(xRand, yRand).normalized;
        Enemy.transform.Translate(moveAmount * Enemy.movementSpeed * Time.deltaTime);
        if (Enemy.facingRight && moveAmount.x < 0)
            Flip();
        else if (!Enemy.facingRight && moveAmount.x > 0)
            Flip();
    }

    public override void OnTriggerEnter()
    {
    }
}