using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
    IPopulationModel engine;
    bool won;
	// Use this for initialization
	void Start () {
        engine = FindObjectOfType<GameManager>().populationEngine;
        won = engine.getPopularity(0) > 0.5;
        if (won)
        {
            GetComponents<AudioSource>()[0].Play();
        }
        else
        {
            GetComponents<AudioSource>()[1].Play();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        int w = Screen.width;
        int h = Screen.height;

        GUIStyle style = new GUIStyle(GUI.skin.box);
        style.fontSize = 30;

        GUI.Box(new Rect(w / 2.0f - 100, 20, 200, 50), "Game over", style);
        style.wordWrap = true;
        if (won)
        {
            GUI.Box(new Rect(w / 2.0f - 300, 200, 600, 500), "Congratulations, you are now the president and world domination has been achieved", style);
        }
        else
        {
            GUI.Box(new Rect(w / 2.0f - 300, 200, 600, 500), "Too bad, looks like we have to bow down to our reptilian overlords now :(", style);
        }

        if (GUI.Button(new Rect(w / 2.0f - 50, 600, 100, 40), "Main Menu"))
        {
            FindObjectOfType<GameManager>().init();
            Application.LoadLevel("menu");
        }
    }
}
