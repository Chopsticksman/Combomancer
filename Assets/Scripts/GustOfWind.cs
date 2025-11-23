using UnityEngine;

public class GustOfWind : MonoBehaviour
{
	int pushBack = 3;
	void OnCollisionEnter2D(Collision2D collision)
	{
		Enemy hit = EnemyManager.instance.TryGetEnemy(collision.gameObject);
		if (hit != null)
			hit.transform.position += new Vector3(pushBack, 0, 0);
	}
}
