using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {

    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(6.0f);
        GameObject.Find("explosion").GetComponentsInChildren<ParticleEmitter>()[0].emit = true;
        GameObject.Find("explosion").GetComponentsInChildren<ParticleEmitter>()[1].emit = true;
        yield return new WaitForSeconds(3.0f);
        print("loading menu");
        Application.LoadLevel("menu");
    }

    void Start()
    {
        print("imaginary loading screen just ran");
        StartCoroutine(LoadMenu());
    }
}
