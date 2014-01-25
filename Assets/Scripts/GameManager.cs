using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    
    // Example:
    // To access score, do
    // GameObject.FindObjectOfType<GameManager>().score
    public int score;

	void Start () 
    {
        print("imaginary loading screen just ran");
        // Initialize all your stuff here

	}

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
