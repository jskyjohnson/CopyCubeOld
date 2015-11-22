using UnityEngine;
using System.Collections;

public class EnergyParticle : MonoBehaviour {
	void Start() {
		if(name == "EnergyParticle(Clone)") {
			GetComponent<Rigidbody>().AddTorque(Random.Range(-360f, 360f), Random.Range(-360f, 360f), Random.Range(-360f, 360f));
			GetComponent<Rigidbody>().velocity = new Vector3(Random.Range (-0.5f, 0.5f), Random.Range (-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
			StartCoroutine(decay ());
		}
	}
	IEnumerator decay() {
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}
