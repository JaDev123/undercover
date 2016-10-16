using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadCredits : MonoBehaviour {

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait; ;
    }

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

}