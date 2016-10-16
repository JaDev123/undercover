using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadInstructions : MonoBehaviour {

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait; ;
    }

    public void onClick()
    {
        loadTheLevel();
    }

    void loadTheLevel()
    {
        Debug.Log("Play");
        SceneManager.LoadScene("Menu");
        //Application.LoadLevel(1);
    }
}