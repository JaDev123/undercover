using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSettings : MonoBehaviour {

    Toggle s;
    Toggle t;
    Toggle d;
    int S;
    int T;
    int D;


    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        GameObject Sound = GameObject.FindWithTag("Sound");
        GameObject Touches = GameObject.FindWithTag("Touches");
        GameObject Difficulty = GameObject.FindWithTag("Difficulty");

        s = Sound.GetComponent<Toggle>();
        t = Touches.GetComponent<Toggle>();
        d = Difficulty.GetComponent<Toggle>();

        if (PlayerPrefs.GetInt("Sound", 0) == 0)
        {
            s.isOn = false;
        }
        else
        {
            s.isOn = true;
        }

        if (PlayerPrefs.GetInt("Touches", 0) == 0)
        {
            t.isOn = false;
        }
        else
        {
            t.isOn = true;
        }


        if (PlayerPrefs.GetInt("Difficulty", 1) == 0)
        {
            d.isOn = false;
        }
        else
        {
            d.isOn = true;
        }
    }

    public void onClick()
    {
        loadTheLevel();
    }

    void loadTheLevel()
    {



        if (s.isOn)
        {
            S = 1;
            AudioListener.pause = true;
        }
        else {
            S = 1;
            AudioListener.pause = false;
        }

        S = s.isOn ? 1 : 0;
        T = t.isOn ? 1 : 0;
        D = d.isOn ? 1 : 0;

        PlayerPrefs.SetInt("Sound",S);
        PlayerPrefs.SetInt("Touches", T);
        PlayerPrefs.SetInt("Difficulty",D);

        Debug.Log("Play");
        SceneManager.LoadScene("Menu");
        //Application.LoadLevel(1);
    }
}
