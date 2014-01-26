using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    
    // Example:
    // To access the manager from any script, do
    // GameObject.FindObjectOfType<GameManager>();

    public AudioClip audioClip;
    AudioSource source;
    int score;

	void Start () 
    {
        score = 0;
        source = gameObject.AddComponent<AudioSource>();
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

    public void playSong()
    {
        source.clip = audioClip;
        source.Play();
        source.volume = 1.0f;
    }
}
