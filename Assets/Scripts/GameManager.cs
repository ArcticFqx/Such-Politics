using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    
    // Example:
    // To access the manager from any script, do
    // GameObject.FindObjectOfType<GameManager>();

    int score;

	void Start () 
    {
        print("imaginary loading screen just ran");
        // Initialize all your stuff here
        score = 0;
	}

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void addScore(int score)
    {
        this.score += score;
    }

    public int getScore()
    {
        return score;
    }
}
