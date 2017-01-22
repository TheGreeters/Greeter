using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GooglePlayServices : MonoBehaviour
{
    //This is our leaderboard ID on Google Play Services. Do not change!
    private const string leaderboard = "CgkIhr--hY4cEAIQAQ";

    #region DEFAULT_UNITY_CALLBACKS
    void Start()
    {
#if UNITY_EDITOR
        // recommended for debugging:
        //PlayGamesPlatform.DebugLogEnabled = true;
#endif

        // Activate the Google Play Games platform
        //PlayGamesPlatform.Activate();

        LogIn();
    }
    #endregion

    #region BUTTON_CALLBACKS

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
        //		Social.ShowLeaderboardUI (); // Show all leaderboard
        //((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(leaderboard); // Show current (Active) leaderboard
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
        //((PlayGamesPlatform)Social.Active).SignOut();
    }
#endregion
}