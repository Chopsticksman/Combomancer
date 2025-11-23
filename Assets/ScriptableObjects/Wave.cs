using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave", order=1)]
public class Wave : ScriptableObject
{
	// this is horrible
	public List<int> enemyIndices;
	public List<int> enemyCounts;
}
