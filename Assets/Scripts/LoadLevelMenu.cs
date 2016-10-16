using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class LoadLevelMenu : MonoBehaviour {

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

        GameObject birds1 = GameObject.FindWithTag("BirdAttackMenu1");
        birds1.GetComponent<Rigidbody>().isKinematic = false; 

        GameObject birds2 = GameObject.FindWithTag("BirdAttackMenu2");
        birds2.GetComponent<Rigidbody>().isKinematic = false; 

        GameObject birds3 = GameObject.FindWithTag("BirdAttackMenu3");
        birds3.GetComponent<Rigidbody>().isKinematic = false; 

        GameObject birds4 = GameObject.FindWithTag("BirdAttackMenu4");
        birds4.GetComponent<Rigidbody>().isKinematic = false; 

        //AllyBird.birdspeed = 450f;
        Debug.Log("Play");
        Invoke("loadMain",1);
        //Application.LoadLevel(1);
    }

    void loadMain() {
        SceneManager.LoadScene("Main");
    }

    public void loadInstructions()
    {
        Debug.Log("Play");
        SceneManager.LoadScene("Instructions");
        //Application.LoadLevel(1);
    }

    public void loadCredits()
    {
        Debug.Log("Play");
        SceneManager.LoadScene("Credits");
        //Application.LoadLevel(1);
    }

    public void exitMenu()
    {
       // ShowAd();
        Application.Quit();
    }

    

    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
        
    }

    public void loadSettings()
    {
        Debug.Log("Play");
        SceneManager.LoadScene("Settings");
        //Application.LoadLevel(1);
    }

}