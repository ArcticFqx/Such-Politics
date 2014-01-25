using UnityEngine;
using System.Collections;

namespace GroupModel {

public class Main : MonoBehaviour {

	public GameObject circle;

	PopulationCollections popcol;

	float[][] flow;
	int flowIndex = 0;

	float flowDelay = 1.0f;
	float flowTime = 2.0f;

	// Use this for initialization
	void Start () {
		popcol = new PopulationCollections ();
		PopulationManager manager1 = new PopulationManager ();
		PopulationManager manager2 = new PopulationManager ();
		PopulationManager manager3 = new PopulationManager ();

		manager1.mutator = new ColorMutator (Color.red);
		manager2.mutator = new ColorMutator (Color.blue);
		manager3.mutator = new ColorMutator (Color.green);

		System.Collections.Generic.List<GameObject> objects = new System.Collections.Generic.List<GameObject> ();

		Camera cam = Camera.main;

		for (int y = 0; y < 4; y++) {
			for (int x = 0; x < 6; x++) {
				GameObject obj = (GameObject) Instantiate (circle);

				obj.transform.position = new Vector3(x, y, cam.nearClipPlane);

				objects.Add(obj);
			}
		}

		manager1.AddPeople (objects.GetRange(0,8));
		manager2.AddPeople (objects.GetRange (8, 8));
		manager3.AddPeople (objects.GetRange (16, 8));

		popcol.AddPopulation (manager1);
		popcol.AddPopulation (manager2);
		popcol.AddPopulation (manager3);

		flow = new float[3][];

		flow [0] = new float[3];
		flow [0] [0] = -0.5f;
		flow [0] [1] = 1.0f;
		flow [0] [2] = 1.0f;

		flow [1] = new float[3];
		flow [1] [0] = 1.0f;
		flow [1] [1] = -0.5f;
		flow [1] [2] = 1.0f;

		flow [2] = new float[3];
		flow [2] [0] = 1.0f;
		flow [2] [1] = 1.0f;
		flow [2] [2] = -0.5f;


		InvokeRepeating ("Flow", flowDelay, flowTime);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void Flow() {
		flowIndex = (flowIndex + 1) % flow.Length;

		popcol.PerformPopulationMigration(flow[flowIndex]);
	}
}

}