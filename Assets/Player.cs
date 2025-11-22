using UnityEngine;

public class Player : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (EnemyManager.instance.IsEnemy(collision.gameObject))
        {
            Debug.Log("Die");
        }
    }
}
