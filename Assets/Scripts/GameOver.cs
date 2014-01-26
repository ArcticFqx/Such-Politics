using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
        IPopulationModel engine = FindObjectOfType<GameManager>().populationEngine;
        
        if (engine.getPopularity(0)>0.5)
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
}
