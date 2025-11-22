using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
	[SerializeField] Enemy enemy;
	[SerializeField] GameObject startPoint;
	List<Enemy> allEnemies;
	bool hasRun = false;
	int tick = 0;
    void Awake()
    {
		this.allEnemies = new List<Enemy>();

		// this is a temporary thing for now
		GameTick.OnTick += SummonEnemy;

		// this should always happen
		GameTick.OnTick += DeleteDead;
		GameTick.OnTick += MoveTick;
		GameTick.OnTick += IncrementEnemyTick;
    }

	void IncrementEnemyTick() 
	{
		tick++;
	}

	void SummonEnemy()
	{
		if(hasRun)
			return;
		if (tick <= 100) {
			return;
		}
		Debug.Log("summon!");
		Enemy newEnemy = Instantiate(enemy, startPoint.transform.position, startPoint.transform.rotation);
		// newEnemy.transform.position = new Vector3(0,0,0);
		allEnemies.Add(newEnemy);
		hasRun = true;
	}
	// treat allEnemies list as swapback array
	void DeleteDead()
	{
		if (tick <= 100) {
			return;
		}
		int end = allEnemies.Count;
		for(int i = 0; i < end;)
		{
			// in this case, allEnemies[i] will be fresh again
			if (allEnemies[i].hitPoints < 0) 
			{
				// 1. put last 
				Enemy replacement = allEnemies[end-1];
				Destroy(allEnemies[i]);
				allEnemies[i] = replacement;

				// 2. delete last and readjust
				allEnemies.RemoveAt(end-1);
				end--;
			} 
			// in this case, allEnemies[i] is fine. incr as normal
			else 
			{
				i++;
			}
		}
	}
	void MoveTick()
	{
		int x = 0;
		foreach (Enemy e in allEnemies)
		{
			Debug.Log("moving " + x++);
			// 1. direction vector
			Vector3 direction = Vector3.Normalize(e.destination.transform.position - e.transform.position);
			// 2. multiply by speed
			Vector3 movement = direction * e.speed;
			// 3. add to position
			e.transform.position += movement;
		}
	}
}
