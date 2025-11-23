using UnityEngine;

public class GustOfWind : MonoBehaviour
{
	int pushBack = 3;
	void OnCollisionEnter2D(Collision2D collision)
	{
		Enemy hit = EnemyManager.instance(collision.gameObject);
		if (hit != null)
			hit.transform.position.x += pushBack;
		}
	}
}
