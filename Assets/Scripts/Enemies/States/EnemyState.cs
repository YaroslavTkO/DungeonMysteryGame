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

    public void Flip()
    {
        Enemy.facingRight = !Enemy.facingRight;
        var scale = Enemy.transform.localScale;
        Enemy.transform.localScale = new Vector3(scale.x * (-1), scale.y,
            scale.z);
    }
}