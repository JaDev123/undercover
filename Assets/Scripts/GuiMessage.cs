using UnityEngine;
using System.Collections;

public class GuiMessage : MonoBehaviour
{
    // 200x300 px window will apear in the center of the screen.
    private Rect windowRect = new Rect((Screen.width/4), (Screen.height/4), Screen.width/2, Screen.height/8);
    // Only show it if needed.
    private bool show = false;
    private GUIStyle guiStyle;
    private GUISkin gSkin;

    void OnGUI()
    {
       // gSkin = GUI.skin;
        guiStyle = GUI.skin.GetStyle("Window");
        guiStyle.fontSize = 50;

        if (show)
            //windowRect = GUI.Window(0, windowRect, DialogWindow, "No Internet");
          windowRect = GUI.Window(0, windowRect, DialogWindow, "No Internet", guiStyle);
    }

    // This is the actual window.
    void DialogWindow(int windowID)
    {
        
        //GUI.Label(new Rect(5, 20, 20, 20), "You need internet connection to view the leaderboard!", guiStyle);
        
        /*
        if (GUI.Button(new Rect(5, y, windowRect.width - 10, 20), "Restart"))
        {
            Application.LoadLevel(0);
            show = false;
        }*/

        if (GUI.Button(new Rect(5, 60, windowRect.width, windowRect.height/2f), "Ok", guiStyle))
        {
            //Application.Quit();
            show = false;
        }
    }

    // To open the dialogue from outside of the script.
    public void Open()
    {
        show = true;
    }
}