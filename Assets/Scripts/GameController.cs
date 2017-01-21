using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    static float Satisfaction = 50f;
    static float GameSpeed = 1f;
    static int Score = 0;

    public const float MaxSpeed = 4f; //Let's be reasonable now

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameSpeed < MaxSpeed)
        {
            GameSpeed += Mathf.Min(Random.Range(0f, 0.0004f), MaxSpeed - GameSpeed);
        }
	}

    public void AddScore(int value)
    {
        Score += value;
    }
}
