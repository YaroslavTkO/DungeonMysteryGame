using UnityEngine;
public class EnemyView : MonoBehaviour
{
    private Enemy enemy;
    void Start()
    {
        enemy = transform.parent.GetComponent<Enemy>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            enemy.OnChildTriggerEnter(col.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            enemy.OnChildTriggerExit();
    }
}
