using UnityEngine;
using System.Collections;

public class Clone : MonoBehaviour {
	void Start() {
		if(name == "Clone(Clone)") {
			tag = "clone";
		}
	}
}
