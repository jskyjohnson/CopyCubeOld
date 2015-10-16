using UnityEngine;
using System.Collections;

public class FallingBlock : MonoBehaviour {
	void OnCollisionEnter(Collision coll) {
		if(coll.gameObject.name == "Player") {
			Debug.Log ("called");
			gameObject.GetComponent<Rigidbody>().isKinematic = false;
		}
	}
}
