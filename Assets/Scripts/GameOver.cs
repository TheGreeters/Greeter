using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    static int Score;

    // Use this for initialization
    void Start () {

        Score = GameController.GetScore();

        GameObject.Find("ScoreLabel").GetComponent<Text>().text = "Score: " + Score;

		GameObject GP = GameObject.Find("GooglePlayServices");
		if(GP != null)
		{
			GooglePlayServices services = GP.GetComponent<GooglePlayServices>();
            services.OnAddScoreToLeaderBoard(Score);
        }

    }
	
	// Update is called once per frame
	void Update () {

        
    }
}
