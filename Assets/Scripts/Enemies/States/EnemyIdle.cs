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
            if (xRand == 0 && yRand == 0)
                Enemy.animator.SetBool("speedIsZero", true);
            else Enemy.animator.SetBool("speedIsZero", false);
        }

        //ahead  0
        //upDown 1
        if (Enemy.wallCollisions[0])
        {
            xRand = -xRand;
            Enemy.wallCollisions[0] = false;
        }
        if (Enemy.wallCollisions[1])
        {
            yRand = -yRand;
            Enemy.wallCollisions[1] = false;
        }

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