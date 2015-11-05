using UnityEngine;
using System.Collections;

public class Clone : MonoBehaviour {
	void Start() {
		if(name == "Clone(Clone)") {
			tag = "clone";
		}
	}

	void OnTriggerEnter(Collider coll) {
		if(coll.name == "ButtonChangeDirection" || coll.name == "DirectionSwitcher") {
			Destroy (gameObject);
		} 
	}
}
