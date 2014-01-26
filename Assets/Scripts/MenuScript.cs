

using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	void OnGUI () {
		// Make a background box
		

        int buttonH = 30;
        int buttonW = 100;
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect((Screen.width/2) - (buttonW/2),
            (Screen.height/2)  ,buttonW, buttonH), "Start")) 
        {
            FindObjectOfType<GameManager>().playSong();
			Application.LoadLevel("gamescene");
		}

        if (GUI.Button(new Rect((Screen.width / 2) - (buttonW / 2),
             (Screen.height /2 + buttonH + 10), buttonW, buttonH), "Load"))
        {
            Application.LoadLevel("gamescene");
        }

        if (GUI.Button(new Rect((Screen.width / 2) - (buttonW / 2),
             Screen.height/2 + buttonH*2 + 20, buttonW, buttonH), "Credits or w/e"))
        {
            Application.LoadLevel("menu");
        }
	}
}