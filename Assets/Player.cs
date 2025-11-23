using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject game;
    [SerializeField] TMP_Text deathText;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (EnemyManager.instance.IsEnemy(collision.gameObject))
        {
            deathText.gameObject.SetActive(true);
            GameTick.instance.stopTick();
        }
    }
}
