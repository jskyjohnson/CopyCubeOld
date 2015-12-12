using UnityEngine;
using System.Collections;

public class StickyGoo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider coll) {
		if(coll.name == "Player") {
			if(!coll.gameObject.GetComponent<Player>().isStuck && !coll.gameObject.GetComponent<Player>().immuneToStuck) {
				coll.gameObject.GetComponent<Player>().isStuck = true;
				coll.gameObject.GetComponent<Rigidbody>().isKinematic = true;
			}
		}
	}
}
