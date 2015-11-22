using UnityEngine;
using System.Collections;

public class Weakspot : MonoBehaviour {
	public GameObject parent;
	public GameObject explosionParticle;
	Transform[] parentBodyChildren;
	void OnTriggerEnter(Collider coll) {
		if(coll.gameObject.name == "Player") {
			Debug.Log ("called");
			coll.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(coll.gameObject.GetComponent<Rigidbody>().velocity.x, 20f, coll.gameObject.GetComponent<Rigidbody>().velocity.z);
			StartCoroutine(die());
		}
	}

	IEnumerator die() {
		Destroy(parent.GetComponent<BoxCollider>());
		foreach(Transform child in parent.transform) {
			child.gameObject.AddComponent<Rigidbody>();
			child.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range (-5f, 5f), Random.Range (-5f, 5f), Random.Range (-5f, 5f));
		}
		yield return new WaitForSeconds(1.5f);
		Destroy (parent);
	}
}
