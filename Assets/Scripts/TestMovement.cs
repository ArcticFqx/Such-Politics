using UnityEngine;
using System.Collections;

public class TestMovement : MonoBehaviour {

    float timer;
	// Use this for initialization
	void Start () {
        timer = Mathf.PI/2;
	}

	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;
        timer += dt;
        float sin = Mathf.Sin(timer);
        transform.Translate(new Vector3(sin * dt * 5,0,0));
	}
}
