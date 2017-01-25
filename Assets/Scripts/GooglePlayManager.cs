using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GooglePlayManager : MonoBehaviour
{
	//For convenience
    private const string leaderboard = GPGSIds.leaderboard_greeter__high_scores;
	
    void Start()
    {
		if (Application.platform == RuntimePlatform.Android)
		{
#if UNITY_EDITOR
			// recommended for debugging:
			PlayGamesPlatform.DebugLogEnabled = true;
#endif
			// Activate the Google Play Games platform
			PlayGamesPlatform.Activate();

			if (!PlayerPrefs.HasKey("UseGooglePlayGames") || PlayerPrefs.GetInt("UseGooglePlayGames") == 1)
			{
				LogIn();
			}
		}
    }

	public void ToggleLogin()
	{
		if(Social.Active.GetType() == typeof(PlayGamesPlatform) && Social.localUser.authenticated)
		{
			OnLogOut();
		}
		else
		{
			LogIn();
		}
	}
	
    /// <summary>
    /// Login In Into Your Google+ Account
    /// </summary>
    public void LogIn()
    {
        Social.localUser.Authenticate((bool success) =>
        {
			if (success)
            {
				Debug.Log("Login Sucess");
            }
            else
            {
				Debug.Log("Login failed");
            }

			PlayerPrefs.SetInt("UseGooglePlayGames", success ? 1 : 0);
			PlayerPrefs.Save();

			UpdateUI(success);
		});
    }

	/// <summary>
	/// Show the Achievements Menu
	/// </summary>
	public void OnShowAchievements()
	{
		Social.ShowAchievementsUI();
	}

    /// <summary>
    /// Shows All Available Leaderborad
    /// </summary>
    public void OnShowLeaderBoard()
    {
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(leaderboard); // Show current (Active) leaderboard
    }

    /// <summary>
    /// Adds Score To leader board
    /// </summary>
    public void OnAddScoreToLeaderBoard(int score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, leaderboard, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Update Score Success");
                }
                else
                {
                    Debug.Log("Update Score Fail");
                }
            });
        }
    }

    /// <summary>
    /// On Logout of your Google+ Account
    /// </summary>
    public void OnLogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();

		PlayerPrefs.SetInt("UseGooglePlayGames", 0);
		PlayerPrefs.Save();

		UpdateUI(false);
    }

	public void UpdateUI(bool loggedIn)
	{
		GameObject gpmLogin = GameObject.Find("GooglePlayGamesLogin");
		if (gpmLogin != null)
		{
			GooglePlayLogin gpmLoginScript = gpmLogin.GetComponent<GooglePlayLogin>();
			gpmLogin.GetComponent<Image>().sprite = loggedIn ? gpmLoginScript.LoggedInImage : gpmLoginScript.LoggedOutImage;
		}

		GameObject leaderboardObj = GameObject.Find("Leaderboard");
		if (leaderboardObj != null)
		{
			leaderboardObj.GetComponent<Button>().interactable = loggedIn;
		}

		GameObject achievementsObj = GameObject.Find("Achievements");
		if (achievementsObj != null)
		{
			achievementsObj.GetComponent<Button>().interactable = loggedIn;
		}
	}
}