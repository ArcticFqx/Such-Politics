using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    
    // Example:
    // To access the manager from any script, do
    // GameObject.FindObjectOfType<GameManager>();

    public AudioClip audioClip;
    public AudioClip intro;
    public AudioClip positiveButtonClip;
    public AudioClip negativeButtonClip;

    public float soundEffectVolum = .8f;

    AudioSource source;
    AudioSource soundEffects;
    int score;

    public int activeStatement;
    public IPopulationModel populationEngine;

	public void init ()     {
        source.clip = intro;
        source.Play();
        score = 0;

        soundEffects.loop = false;
        soundEffects.volume = soundEffectVolum;
    }

	void Start ()
    {
        source = gameObject.AddComponent<AudioSource>();
        init();
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
        source.Stop();
        source.clip = audioClip;
        source.Play();
        source.volume = 1.0f;
    }
}
