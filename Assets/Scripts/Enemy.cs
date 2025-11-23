using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
	public float speed;
	public float hitPoints;
	public GameObject startPoint;
	float size;

	void Start() 
	{
		// HARD CODED NO TIME TO FIGURE OUT SIZE OF BOUNDING BOX
		size = 1;
	}

	public float Size()
	{
		return size;
	}
	
}
