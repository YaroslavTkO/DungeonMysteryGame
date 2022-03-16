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
        var hits = new RaycastHit2D[4];
        var times = Physics2D.RaycastNonAlloc(origin, Vector2.up, hits, 0.6f, Enemy.wallLayer);
        for (int i = 0; i < times; i++)
            if (hits[i])
            {
                yRand = Random.Range(-1, 1);
                break;
            }
        times = Physics2D.RaycastNonAlloc(origin, Vector2.down, hits, 0.6f, Enemy.wallLayer);
        for (int i = 0; i < times; i++)
            if (hits[i])
            {
                yRand = Random.Range(0, 2);
                break;
            }
        times = Physics2D.RaycastNonAlloc(origin, Vector2.left, hits, 0.6f, Enemy.wallLayer);
        for (int i = 0; i < times; i++)
            if (hits[i])
            {
                xRand = Random.Range(0, 2);
                break;
            }
        times = Physics2D.RaycastNonAlloc(origin, Vector2.right, hits, 0.6f, Enemy.wallLayer);
        for (int i = 0; i < times; i++)
            if (hits[i])
            {
                xRand = Random.Range(-1, 1);
                break;
            }
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