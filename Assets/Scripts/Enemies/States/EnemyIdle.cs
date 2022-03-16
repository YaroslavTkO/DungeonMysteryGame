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
        var hits = new RaycastHit2D[6];
        var times = Physics2D.RaycastNonAlloc(origin, Vector2.up, hits, 0.4f);
        for (int i = 0; i < times; i++)
            if (hits[i].collider.gameObject.CompareTag("Wall"))
            {
                yRand = Random.Range(-1, 1);
                break;
            }

        times = Physics2D.RaycastNonAlloc(origin, Vector2.down, hits, 0.4f);
        for (int i = 0; i < times; i++)
            if (hits[i].collider.gameObject.CompareTag("Wall"))
            {
                yRand = Random.Range(0, 2);
                break;
            }

        times = Physics2D.RaycastNonAlloc(origin, Vector2.left, hits, 0.4f);
        for (int i = 0; i < times; i++)
            if (hits[i].collider.gameObject.CompareTag("Wall"))
            {
                xRand = Random.Range(0, 2);
                break;
            }
        times = Physics2D.RaycastNonAlloc(origin, Vector2.right, hits, 0.4f);
        for (int i = 0; i < times; i++)
            if (hits[i].collider.gameObject.CompareTag("Wall"))
            {
                xRand = Random.Range(-1, 1);
                break;
            }
        if (xRand == 0 && yRand == 0)
            Enemy.animator.SetBool("speedIsZero", true);
        else Enemy.animator.SetBool("speedIsZero", false);

        //ahead  0
        //upDown 1
        /*  if (Enemy.wallCollisions[0])
          {
              xRand = -xRand;
              Enemy.wallCollisions[0] = false;
              SavedTime = Time.time;
          }
          if (Enemy.wallCollisions[1])
          {
              yRand = -yRand;
              Enemy.wallCollisions[1] = false;
              SavedTime = Time.time;
          }*/
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