using UnityEngine;
using System.Collections;

namespace GroupModel {

public class ColorMutator : GameObjectMutator {
	
	private Color color;
	
	public ColorMutator(Color color) {
		this.color = color;
	}
	
	public void Mutate(GameObject dude) {
		dude.GetComponent<SpriteRenderer>().color = color;
	}
}

}