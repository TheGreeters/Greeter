using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    static int Score;

	//These variable values are stolen from the MenuUI object.
	private int sceneToStart = 1;
	private PlayMusic playMusic;

	void Awake ()
	{
		GameObject menuUI = GameObject.Find("MenuUI");
		playMusic = menuUI.GetComponent<PlayMusic>();
		sceneToStart = menuUI.GetComponent<StartOptions>().sceneToStart;
	} 

	// Use this for initialization
	void Start () {

		PlayMusic musicScript = GameObject.Find("MenuUI").GetComponent<PlayMusic>();
		musicScript.StopMusic();

        Score = GameController.GetScore();

        GameObject.Find("ScoreLabel").GetComponent<Text>().text = "Score: " + Score;

		GameObject GP = GameObject.Find("GooglePlayServices");
		if(GP != null)
		{
			GooglePlayManager services = GP.GetComponent<GooglePlayManager>();
            services.OnAddScoreToLeaderBoard(Score);
        }

    }

	public void TryAgainButtonClicked()
	{
		playMusic.FadeUp(0.01f);
		playMusic.PlaySelectedMusic(1);
		SceneManager.LoadScene(sceneToStart);
	}
}
