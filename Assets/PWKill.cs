using UnityEngine;

public class PWKill : MonoBehaviour
{
    int dmg = 99999;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (EnemyManager.instance.IsEnemy(collision.gameObject))
        {
            Enemy hit = EnemyManager.instance.TryGetEnemy(collision.gameObject);
            hit.hitPoints -= dmg;
            if (hit.hitPoints <= 0)
            {
                hit.gameObject.SetActive(false);
            }
        }
    }
}
