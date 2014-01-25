using UnityEngine;
using System.Collections;
public class GameScreen : MonoBehaviour {
	// Use this for initialization
    public float hSliderValue = 0.0F;
    Statement[] statements;

    string question;
    string category;
    GameManager manager;
	void Start () {
        statements = Statement.getStatements();
        int start = Random.Range(0, statements.Length);
        question = statements[start].getIssue();
        category = statements[start].getCategory() + " issue";
        manager = GameObject.FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void addScoreAndLoadNext(int score)
    {
        manager.addScore(score);
        print("Your score is now " + manager.getScore());
        if (manager.getScore() > 5)
        {
            Application.LoadLevel("menu");
        }
    }

    void OnGUI()
    {
        float h = Screen.height;
        float w = Screen.width;

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

        if (GUI.Button(new Rect(w * 0.025f, h / 2, w * 0.2f, h * 0.1f), "Answer 1"))
        {
            addScoreAndLoadNext(1);
        }
        if (GUI.Button(new Rect(w - w * 0.225f, h / 2, w * 0.2f, h * 0.1f), "Answer 2"))
        {
            addScoreAndLoadNext(-1);
        }
    }
}
