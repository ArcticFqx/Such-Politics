using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {

    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(3.0f);
        Application.LoadLevel("menu");
    }

    void Start()
    {
        print("imaginary loading screen just ran");
        StartCoroutine(LoadMenu());
    }
}
