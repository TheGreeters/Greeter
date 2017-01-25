using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	private GooglePlayManager GPM;

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
#if UNITY_ANDROID
		GameObject GPObject = GameObject.Find("GooglePlayManager");
		if(GPObject != null)
		{
			bool loggedIn = Social.Active.GetType() == typeof(PlayGamesPlatform) && Social.localUser.authenticated;
			GPM = GPObject.GetComponent<GooglePlayManager>();
			GPM.UpdateUI(loggedIn);
		}
#endif
	} 

	// Use this for initialization
	void Start () {

		if (playMusic != null)
		{
			playMusic.StopMusic();
		}

        GameObject.Find("ScoreLabel").GetComponent<Text>().text = "Score: " + GameController.Score;
		
		if(GPM != null && Social.localUser.authenticated)
		{
			GPM.OnAddScoreToLeaderBoard(GameController.Score);
        }
		else
		{
			GameObject.Find("Leaderboard").GetComponent<Button>().interactable = false;
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

	public void LeaderboardClicked()
	{
		if (GPM != null)
		{
			GPM.OnShowLeaderBoard();
		}
	}

	public void AchievementsClicked()
	{
		if (GPM != null)
		{
			GPM.OnShowAchievements();
		}
	}
}
