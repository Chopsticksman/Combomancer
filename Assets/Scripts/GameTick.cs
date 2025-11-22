using System;
using UnityEngine;


public class GameTick : MonoBehaviour
{
    float tickTime = 0.01f; //Time in seconds
    float timePassed = 0f; //Time passed so far

    //Whenever update is called enough, 
    public static event Action OnTick;

    // Update is called once per frame
    private void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= tickTime) //If time passed is enough 
        {
            timePassed = 0f;
            OnTick?.Invoke();
        }
    }
}