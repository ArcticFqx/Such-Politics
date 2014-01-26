using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GroupModel;

public class GameScreen : MonoBehaviour {
	// Use this for initialization
	public float hSliderValue = 0.0F;
    private float prevHSliderValue = 0.0F;

	Statement[] statements;
	
	string question;
	string category;
	string positive;
	string negative;
	
	float timestamp;
	GameManager manager;

    private GameObject sun;
	
	public GameObject baseObject;
	
	void Start () {
		timestamp = Time.time + 20.0f;
		
		manager = GameObject.FindObjectOfType<GameManager>();
		
		manager.populationEngine = new GroupModel.PopulationCollections ();

        sun = GameObject.Find("Sun");

		List<GameObjectMutator> mutators = new List<GameObjectMutator> ();
		mutators.Add (new ColorMutator (Color.red));
		mutators.Add (new ColorMutator (Color.blue));
		mutators.Add (new ColorMutator (Color.green));
		
		int rows = 4;
		int cols = 6;
		manager.populationEngine.generatePopulation (rows*cols, new double[]{0.33, 0.33, 0.34}, mutators, baseObject);
		
		List<GameObject> objects = manager.populationEngine.getPopulation ();
		
		Camera cam = Camera.main;
		
		for (int y = 0; y < rows; y++) {
			for (int x = 0; x < cols; x++) {
				GameObject obj = objects[y*cols + x];
				
				obj.transform.position = new Vector3(x, y, cam.nearClipPlane);
			}
		}
		
		statements = Statement.getStatements();
		manager.populationEngine.setStatements (new List<Statement>(statements));
		
		nextStatement();
	}
	
	bool isPlaying = false;
	bool timeOut = false;
	
	// Update is called once per frame
	void Update () {
		if ((timestamp - Time.time) < 4 && !isPlaying)
		{
			GetComponents<AudioSource>()[0].Play();
			isPlaying = true;
		}
		if ((timestamp - Time.time) < 1 && !timeOut)
		{
			timeOut = true;
			GetComponents<AudioSource>()[1].Play();
		}
		if ((timestamp - Time.time) < 0)
		{
			
			Application.LoadLevel("gamescene");
		}

        updateSun(false);
	}

    void nextStatement()
    {
        timestamp = Time.time + 20.0f;
		int start = Random.Range(0, statements.Length);
		manager.activeStatement = start;
		question = statements[start].getIssue();
		category = statements[start].getCategory() + " issue";
		positive = statements[start].getPositive();
		negative = statements[start].getNegative();

        updateSun(true);
	}
	
	void addScoreAndLoadNext(int score)
	{
		manager.addScore(score);
		
		manager.populationEngine.applyAnswer (0, manager.activeStatement, (score > 0 ? true : false));
		print("Your score is now " + manager.getScore());
		if (manager.getScore() > 5)
		{
			Application.LoadLevel("menu");
		}
		else
		{
			//Application.LoadLevel("gamescene");
			nextStatement();
		}
	}
	
	void OnGUI()
	{
		float h = Screen.height;
		float w = Screen.width;
		
		hSliderValue = GUI.HorizontalSlider(
			new Rect(w / 2 - w*0.2f, h - h * 0.29f, w*0.4f, 20.0f), 
			hSliderValue, -1.0f, 1.0f);
		
		// Style for question
		GUIStyle style = new GUIStyle(GUI.skin.box);
		style.alignment = TextAnchor.UpperLeft;
		style.contentOffset = new Vector2(8.0f,20.0f);
		style.padding.right = 14;
		style.wordWrap = true;
		style.fontSize = 20;
		GUI.Box(new Rect(w / 2.0f - w * 0.25f, h - h * 0.25f, w * 0.5f, h * 0.22f), question, style);
		GUI.Box(new Rect(w / 2.0f - w * 0.25f, h - h * 0.25f, w * 0.5f, 20), category);
		
		// Style for time
		style = new GUIStyle(GUI.skin.box);
		style.fontSize = 30;
		if ((timestamp - Time.time) < 4)
		{
			GUI.Box(new Rect(w / 2.0f - 20, 20, 40, 40), ((int)(timestamp - Time.time)).ToString(), style);
		}
		
		// Style for buttons
		style = new GUIStyle(GUI.skin.button);
		style.wordWrap = true;
		style.fontSize = 20;
		style.alignment = TextAnchor.UpperLeft;
		
		
		if (timeOut) { return; }
		
		if (GUI.Button(new Rect(w * 0.025f, h / 2, w * 0.2f, h * 0.4f), positive,style))
		{
			addScoreAndLoadNext(1);
		}
		
		if (GUI.Button(new Rect(w - w * 0.225f, h / 2, w * 0.2f, h * 0.4f), negative,style))
		{
			addScoreAndLoadNext(-1);
		}
	}

    private void updateSun(bool force)
    {
        if (hSliderValue != prevHSliderValue || force)
        {
            float newScale  = (float)manager.populationEngine.getDistanceFrom(hSliderValue, manager.activeStatement, 0);
            prevHSliderValue = hSliderValue;
            sun.transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }
}
