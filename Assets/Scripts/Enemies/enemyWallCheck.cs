using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWallCheck : MonoBehaviour
{
    public int colliderId;
    public Enemy enemy;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Wall")){
            enemy.wallCollisions[colliderId] = true;
            enemy.SavedTime = Time.time;
        }
    }
}
