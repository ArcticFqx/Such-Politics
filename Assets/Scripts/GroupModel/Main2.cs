using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GroupModel;

public class Main2 : MonoBehaviour {

    public GameObject circle;

    float[][] flow;
    int flowIndex = 0;

    float flowDelay = 1.0f;
    float flowTime = 2.0f;

	// Use this for initialization
	void Start () {
        double[] popSpread = {.4, .4, .2};
        List<GroupModel.GameObjectMutator> objectMutators = new List<GroupModel.GameObjectMutator>();
        objectMutators.Add(new ColorMutator (Color.red));
        objectMutators.Add(new ColorMutator (Color.blue));
        objectMutators.Add(new ColorMutator (Color.green));

        IPopulationModel population = new DistrubatedPopulationModel();
        ((DistrubatedPopulationModel)population).generatePopulation(10, popSpread, objectMutators, circle);

        Camera cam = Camera.main;

        int x = 0;
        int y = 0;
        for (int i = 0; i < population.getPopulation().Count; i++)
        {
            if (y >= 4)
            {
                y = 0;
                x++;
            }

            population.getPopulation()[i].transform.position = new Vector3(x, y, cam.nearClipPlane);
            y++;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
