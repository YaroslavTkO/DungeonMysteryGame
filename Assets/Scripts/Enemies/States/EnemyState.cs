using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    public Enemy Enemy;

    public abstract void Update();

    public EnemyState ChangeState(EnemyState newState)
    {
        return newState;
    }

    public abstract void OnTriggerEnter();

    public void Flip()
    {
        Enemy.facingRight = !Enemy.facingRight;
        Enemy.transform.localScale = new Vector3(Enemy.transform.localScale.x * (-1), Enemy.transform.localScale.y,
            Enemy.transform.localScale.z);
    }
}