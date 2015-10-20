using UnityEngine;
using System.Collections;

public class DeathBlock : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider coll) {
		if(coll.name == "Player") {
			coll.GetComponent<Player>().die();
		}
	}
}
