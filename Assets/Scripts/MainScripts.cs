using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class MainScripts : MonoBehaviour {

   public void onClick()
    {
        loadTheLevel();
    }

    public void loadTheLevel()
    {
        Debug.Log("Play");
        SceneManager.LoadScene("Menu");
        //Application.LoadLevel(1);
    }

    public void exit()
    {
        ShowAd();
        SceneManager.LoadScene("Menu");
    }

    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
    }
}