

using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public Texture redTexture;
    public Texture greenTexture;

	void OnGUI () {
		// Make a background box
		

        int buttonH = 30;
        int buttonW = 100;
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
        GUIStyle style = new GUIStyle(GUI.skin.box);
        style.wordWrap = true;
        style.fontSize = 20;
        GUI.Box(new Rect(Screen.width / 2.0f - 200, Screen.height / 2, 400, 280), "The perfect politician knows how to balance personal opinions and those of the masses. At least until the election is won.\n\nUse the slider to see which direction the masses lean.\n\nChoose your hero",style);

		if(GUI.Button(new Rect((Screen.width/2) - 190,
            ((Screen.height/4)*3f)  ,105, 115), redTexture)) 
        {
            FindObjectOfType<GameManager>().playSong();
			Application.LoadLevel("gamescene");
		}
		if(GUI.Button(new Rect((Screen.width/2) + 90,
            ((Screen.height/4)*3f)  ,105, 115), greenTexture)) 
        {
            FindObjectOfType<GameManager>().playSong();
			Application.LoadLevel("gamescene");
		}
	}
}