using UnityEngine;
using System.Collections;

public class ExplosionParticle : MonoBehaviour {
	public GameObject clone;
	public GameObject player;
	// Use this for initialization
	void Start () {
		Physics.IgnoreCollision(GetComponent<Collider>(), GetComponentInParent<Collider>());
		Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>());
		GetComponent<Rigidbody>().velocity = new Vector3(Random.Range (-4f, 4f), Random.Range (15f, 20f), Random.Range(-4f, 4f));
		if(name == "ExplosionParticle(Clone)") {
			StartCoroutine(decay ());
		}
	}

	IEnumerator decay() {
		yield return new WaitForSeconds(3f);
		Destroy(gameObject);
	}
}
