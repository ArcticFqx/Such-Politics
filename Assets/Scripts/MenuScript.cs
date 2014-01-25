

using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	void OnGUI () {
		// Make a background box
		

        int buttonH = 30;
        int buttonW = 100;
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect((Screen.width/2) - (buttonW/2),
            (9*Screen.height/18 - buttonH)  ,buttonW, buttonH), "Start")) {
			Application.LoadLevel("gamescene");
		}

        if (GUI.Button(new Rect((Screen.width / 2) - (buttonW / 2),
             (12 * Screen.height / 18 - buttonH), buttonW, buttonH), "Load"))
        {
            Application.LoadLevel("Scene1");
        }

        if (GUI.Button(new Rect((Screen.width / 2) - (buttonW / 2),
             (15 * Screen.height / 18) - buttonH, buttonW, buttonH), "Credits or w/e"))
        {
            Application.LoadLevel("Scene1");
        }
	}
}