using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

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
        });
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
    }
}