using UnityEngine;
using System.Collections;
using SimpleJSON;

public class GUITest : MonoBehaviour {
	// Use this for initialization
    public float hSliderValue = 0.0F;
    string question;
    string category;
    JSONNode json;
	void Start () {
        string text = System.IO.File.ReadAllText("assets/scripts/test.json");
        json = JSON.Parse(text);
        int start = Random.Range(0, json["statement"].Count);
        question = json["statement"][start]["issue"];
        category = json["statement"][start]["category"] + " issue";

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
      

        float h = this.camera.pixelRect.height;
        float w = this.camera.pixelRect.width;

        hSliderValue = GUI.HorizontalSlider(
                new Rect(w / 2 - w*0.2f, h - h * 0.29f, w*0.4f, 20.0f), 
                hSliderValue, -1.0f, 1.0f);

        GUIStyle style = new GUIStyle(GUI.skin.box);
        style.alignment = TextAnchor.UpperLeft;
        style.contentOffset = new Vector2(8.0f,20.0f);
        style.padding.right = 14;
        style.wordWrap = true;
        GUI.Box(new Rect(w / 2.0f - w * 0.25f, h - h * 0.25f, w * 0.5f, h * 0.22f), question, style);
        GUI.Box(new Rect(w / 2.0f - w * 0.25f, h - h * 0.25f, w * 0.5f, 20), category);

        /*
            To access slider value in other scripts, do 
            Camera.current.GetComponent<GUITest>().hSliderValue;
        */
        
    }
}
