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

    public int numStatements = 24;
    private int statementCounter = 0;
	
	void Start () {
		timestamp = Time.time + 20.0f;
		
		manager = GameObject.FindObjectOfType<GameManager>();
		
		manager.populationEngine = new GroupModel.PopulationCollections ();

        sun = GameObject.Find("Sun");

		List<GameObjectMutator> mutators = new List<GameObjectMutator> ();
		mutators.Add (new ColorMutator (Color.red));
		mutators.Add (new ColorMutator (Color.blue));
		mutators.Add (new ColorMutator (Color.green));
		
		int rows = 20;
		int cols = 20;
		manager.populationEngine.generatePopulation (rows*cols, new double[]{0.33, 0.33, 0.34}, mutators, baseObject);
		
		List<GameObject> objects = manager.populationEngine.getPopulation ();
		
		Camera cam = Camera.main;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                GameObject obj = objects[y * cols + x];

                obj.transform.position = new Vector3(x * .255f - 2.38f, y * .255f - 1.2f, cam.nearClipPlane);
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
			//Application.LoadLevel("gamescene");
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
		if (statementCounter == numStatements - 1)
		{
            manager.source.Stop();
			Application.LoadLevel("gameover");
		}
		else
		{
			nextStatement();
            statementCounter++;
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
        GUI.backgroundColor = Color.red;
		if (GUI.Button(new Rect(w * 0.025f, h / 2, w * 0.2f, h * 0.4f), negative,style))
		{
			addScoreAndLoadNext(-1);
            manager.soundEffects.clip = manager.negativeButtonClip;
            manager.soundEffects.Play();
		}

        GUI.backgroundColor = Color.green;
		if (GUI.Button(new Rect(w - w * 0.225f, h / 2, w * 0.2f, h * 0.4f), positive,style))
		{
			addScoreAndLoadNext(1);
            manager.soundEffects.clip = manager.positiveButtonClip;
            manager.soundEffects.Play();
		}

		drawStats ();
	}
	
	private void drawStats() {
		float h = Screen.height;
		float w = Screen.width;
		
		int numPlayers = 3;
		
		float rowHeight = 0.075f;
		float rowWidth = 0.25f;
		
		float imageWidth = 0.13f;
		
		float totalHeight = numPlayers * rowHeight;
		
		float statPos_x = 0.725f;
		float statPos_y = 0.1f;
		
		// Style for statBox
		GUIStyle statStyle = new GUIStyle(GUI.skin.box);
		
		GUI.backgroundColor = new Color (0.8f, 0.8f, 0.8f, 0.5f);
		GUI.Box(new Rect(w * statPos_x, h * statPos_y, w * rowWidth, (float)totalHeight * h), "", statStyle);
		
		// Style for row
		GUIStyle style = new GUIStyle ();
		//style.wordWrap = true;
		style.fontSize = (int)(rowHeight * h) - 1;
		style.normal.textColor = Color.white;
		
		for (int player = 0; player < numPlayers; player++) {
			string playerText;
			if (player == 0) {
				playerText = "You";
				style.normal.textColor = Color.red;
			} else if (player == 1) {
				playerText = "Rep";
				style.normal.textColor = Color.blue;
			} else {
				playerText = "Neu";
				style.normal.textColor = Color.green;
			}
			
			GUI.Box (new Rect(w*statPos_x, h*(statPos_y + player*rowHeight), h*rowHeight, w*(imageWidth)), 
			         playerText, style);
			GUI.Box (new Rect(w*(statPos_x + imageWidth), h*(statPos_y + player*rowHeight), h*rowHeight, w*(rowWidth - imageWidth)), 
			         ((float)manager.populationEngine.getPopularity(player) * 100).ToString("F1") + "%", style);
		}
	}

    private void updateSun(bool force)
    {
        if (hSliderValue != prevHSliderValue || force)
        {
            float newScale  = (float)manager.populationEngine.getDistanceFrom(hSliderValue, manager.activeStatement, 0);
            prevHSliderValue = hSliderValue;
            Debug.Log(newScale);
            sun.transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }
}
