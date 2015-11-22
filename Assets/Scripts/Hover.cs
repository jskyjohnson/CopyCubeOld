using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour {
	void Start() {
		if(name == "Hover(Clone)") {
			gameObject.AddComponent<Rigidbody>();
			gameObject.GetComponent<Rigidbody>().useGravity = false;
			GetComponent<Rigidbody>().velocity = new Vector3(0f, -2f, 0f);
			StartCoroutine(decay ());
		}
	}
	IEnumerator decay() {
		yield return new WaitForSeconds(0.3f);
		Destroy(gameObject);
	}
}
