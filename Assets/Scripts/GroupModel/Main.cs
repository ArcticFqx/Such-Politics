using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GroupModel {

public class Main : MonoBehaviour {

	public GameObject circle;

	IPopulationModel popcol;

	int flowIndex = 0;

	float flowDelay = 1.0f;
	float flowTime = 2.0f;

	// Use this for initialization
	void Start () {
		popcol = new PopulationCollections ();
		List<GameObjectMutator> mutators = new List<GameObjectMutator> ();
		mutators.Add (new ColorMutator (Color.red));
		mutators.Add (new ColorMutator (Color.blue));
		mutators.Add (new ColorMutator (Color.green));

		int rows = 4;
		int cols = 6;
		popcol.generatePopulation (rows*cols, new double[]{0.33, 0.33, 0.34}, mutators, circle);
		
		List<Statement> statements = new List<Statement> ();
		statements.AddRange (Statement.getStatements ());

		popcol.setStatements (statements);
		
		List<GameObject> objects = popcol.getPopulation ();

		Camera cam = Camera.main;

		for (int y = 0; y < rows; y++) {
			for (int x = 0; x < cols; x++) {
				GameObject obj = objects[y*cols + x];

				obj.transform.position = new Vector3(x, y, cam.nearClipPlane);
			}
		}

		InvokeRepeating ("Flow", flowDelay, flowTime);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void Flow() {
		flowIndex = (flowIndex + 1) % 3;

		popcol.applyAnswer (0, flowIndex, true);
	}
}

}