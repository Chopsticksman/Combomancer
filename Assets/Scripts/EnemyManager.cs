using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
	// [SerializeField] public List<Tuple<Enemy, GameObject, GameObject>> enemyTypes;
	[SerializeField] List<Enemy> enemyTypes;
	[SerializeField] List<Wave> waveOptions;
	public GameObject player;
	List<Enemy> allEnemies;
	bool hasRun = false;
	int tick = 0;
	public static EnemyManager instance;
    void Awake()
    {
		allEnemies = new List<Enemy>();
		instance = this;

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

	void summonWave(Wave wave) 
	{
		// foreach((Enemy, int) ePair in wave.data) 
		for(int i = 0; i < wave.enemyIndices.Count; i++)
		{
			Enemy e = enemyTypes[wave.enemyIndices[i]];
			int count = wave.enemyCounts[i];
			Vector3 positionOffset = new Vector3(0,0,0);
			float enemySize = e.Size();
			for(int j = 0; j < count; j++) 
			{
				GameObject startPoint = e.startPoint;
				Enemy newEnemy = Instantiate(e,
						startPoint.transform.position,
						startPoint.transform.rotation);
				newEnemy.transform.position -= positionOffset;
				positionOffset.x -= enemySize;
				allEnemies.Add(newEnemy);
			}
		}
	}

	void SummonEnemy()
	{
		if(hasRun)
			return;
		if (tick <= 100) {
			return;
		}
		Debug.Log("summon!");
		int randomIndex = Random.Range(0, waveOptions.Count - 1);
		Wave randomWave = waveOptions[randomIndex];
		summonWave(randomWave);
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
		foreach (Enemy e in allEnemies)
		{
			// 1. direction vector
			Vector3 direction = new Vector3(-1, 0, 0);
			// 2. multiply by speed
			Vector3 movement = direction * e.speed;
			// 3. add to position
			e.transform.position += movement;
		}
	}

    public bool IsEnemy(GameObject obj) 
    {
        foreach (Enemy e in allEnemies) 
        {
            if (e.gameObject.Equals(obj)) 
            {
                return true;
            }
        }
        return false;
    }
	public Enemy ClosestEnemy()
	{
		if(allEnemies.Count < 1) 
			return null;
		Enemy e = allEnemies[0];
		for(int i = 1; i < allEnemies.Count; i++)
		{
			if (allEnemies[i].transform.position.x < e.transform.position.x)
			{
				e = allEnemies[i];
			}
		}
		return e;
	}
}
