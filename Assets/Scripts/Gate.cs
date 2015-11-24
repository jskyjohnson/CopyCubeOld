using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour {
	public bool open;
	public Material openMaterial;
	public Material closedMaterial;
	// Use this for initialization

	public void OnCollisionEnter(Collision coll) {
		if(coll.gameObject.name == "Player" && !open) {
			coll.gameObject.GetComponent<Player>().die ();
		}
	}
	// Update is called once per frame
	public void openGate () {
		gameObject.GetComponent<SkinnedMeshRenderer>().material = openMaterial;
		open = true;
		GetComponent<BoxCollider>().isTrigger = true;
	}

	public void closeGate() {
		open = false;
		gameObject.GetComponent<SkinnedMeshRenderer>().material = closedMaterial;
		GetComponent<BoxCollider>().isTrigger = false;
	}

}
