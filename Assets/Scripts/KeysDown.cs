using UnityEngine;
using System.Collections;

public class KeysDown : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, dt * 2, 0));
        }
	}
}
