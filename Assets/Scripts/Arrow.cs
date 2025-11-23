using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int dmg = 40;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (EnemyManager.instance.IsEnemy(collision.gameObject))
        {
            Enemy hit = EnemyManager.instance.TryGetEnemy(collision.gameObject);
            hit.hitPoints -= dmg;
            if (hit.hitPoints <= 0)
            {
                EnemyManager.instance.DeleteDead();
            }
        }
    }
}