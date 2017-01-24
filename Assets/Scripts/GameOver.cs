using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
	
	//These variable values are stolen from the MenuUI object.
	private int sceneToStart = 1;
	private PlayMusic playMusic;

	void Awake ()
	{
		GameObject menuUI = GameObject.Find("MenuUI");
		if (menuUI != null)
		{
			playMusic = menuUI.GetComponent<PlayMusic>();
			sceneToStart = menuUI.GetComponent<StartOptions>().sceneToStart;
		}
	} 

	// Use this for initialization
	void Start () {

		if (playMusic != null)
		{
			playMusic.StopMusic();
		}

        GameObject.Find("ScoreLabel").GetComponent<Text>().text = "Score: " + GameController.Score;

		GameObject GP = GameObject.Find("GooglePlayServices");
		if(GP != null)
		{
			GooglePlayManager services = GP.GetComponent<GooglePlayManager>();
            services.OnAddScoreToLeaderBoard(GameController.Score);
        }

    }

	public void TryAgainButtonClicked()
	{
		if (playMusic != null)
		{
			playMusic.FadeUp(0.01f);
			playMusic.PlaySelectedMusic(1);
		}
		SceneManager.LoadScene(sceneToStart);
	}
}
