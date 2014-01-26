using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GroupModel {

public interface GameObjectMutator {
	void Mutate(GameObject thing);
}

public class PopulationManager {

	List<GameObject> people = new List<GameObject>(0);
	System.Random random = new System.Random ();

	// Can be changed dynamically
	public GameObjectMutator mutator;

	public GameObject[] LoseRandom(int num) {
		if (num > people.Count) {
			num = people.Count;
		}

		GameObject[] randomPeople = new GameObject[num];

		for (int i = 0; i < num; i++) {
			int pos = random.Next(0, people.Count);

			GameObject dude = people[pos];
			people.RemoveAt(pos);
			randomPeople[i] = dude;
		}

		return randomPeople;
	}

	public int GetCount() {
		return people.Count;
	}

	public void AddPeople(IEnumerable<GameObject> dudes) {
		foreach (GameObject dude in dudes) {
			// Set dude to correct color or whatever
			mutator.Mutate(dude);
		}

		people.AddRange (dudes);
	}

	public void DestroyIt() {
		foreach (GameObject dude in people) {
			UnityEngine.Object.Destroy (dude);
		}

		people.Clear ();
	}
}

}
