using UnityEngine;
using GooglePlayGames;

public class LeaderBoard : MonoBehaviour
{
    GuiMessage showMessageBtn;
    #region PUBLIC_VAR
    public string leaderboard;
    #endregion
    #region DEFAULT_UNITY_CALLBACKS
    void Start()
    {
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;

        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
        //LogIn();

        GameObject Scoreb = GameObject.FindWithTag("Score Board");
        showMessageBtn = Scoreb.GetComponent<GuiMessage>();
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
                GlobalVariables.loggedIn = true;
            }
            else {
                Debug.Log("Login failed");
                GlobalVariables.loggedIn = false;
            }
        });
    }
    /// <summary>
    /// Shows All Available Leaderborad
    /// </summary>
    public void OnShowLeaderBoard()
    {

        if (Social.localUser.authenticated)
        {
            //LogIn();
            //        Social.ShowLeaderboardUI (); // Show all leaderboard
            ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(leaderboard); // Show current (Active) leaderboard
            GlobalVariables.loggedIn = true;
        }
        else Social.localUser.Authenticate((bool success) =>
             {
                 if (success)
                 {
                     Debug.Log("Login Sucess");
                     ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(leaderboard); // Show current (Active) leaderboard

                     GlobalVariables.loggedIn = true;
                 }
                 else {
                     Debug.Log("Login failed");
                     showMessageBtn.Open();

                     GlobalVariables.loggedIn = false;
                 }
             });
    }
    /// <summary>
    /// Adds Score To leader board
    /// </summary>
    public void OnAddScoreToLeaderBorad(int score)
    {
        if (Social.localUser.authenticated)
        {
            GlobalVariables.loggedIn = true;
            Social.ReportScore(score, leaderboard, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Update Score Success");

                }
                else {
                    Debug.Log("Update Score Fail");
                    
                }
            });
        }
        else GlobalVariables.loggedIn = false;
        /*
        else Social.localUser.Authenticate((bool loginsuccess) =>
        {
            if (loginsuccess)
            {
                Debug.Log("Login Sucess");
                Social.ReportScore(score, leaderboard, (bool scoresuccess) =>
                {
                    if (scoresuccess)
                    {
                        Debug.Log("Update Score Success");

                    }
                    else {
                        Debug.Log("Update Score Fail");
                    }
                });
            }
            else {
                Debug.Log("Login failed");
            }
        });*/
    }
    /// <summary>
    /// On Logout of your Google+ Account
    /// </summary>
    public void OnLogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
    }

    #endregion
}

