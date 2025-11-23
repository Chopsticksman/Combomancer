using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int dmg = 100;
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