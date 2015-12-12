using UnityEngine;
using System.Collections;

public class Inverter : MonoBehaviour {
	bool active;
	void Start() {
		active = true;
	}
	public void OnTriggerEnter(Collider coll) {
		if(coll.name == "Player" && active) {
			StartCoroutine(activate());
			if(!coll.gameObject.GetComponent<Player>().inverted) {
				Physics.gravity = new Vector3(0, 50f, 0);
				coll.gameObject.GetComponent<Player>().inverted = true;
			} else {
				Physics.gravity = new Vector3(0, -50f, 0);
				coll.gameObject.GetComponent<Player>().inverted = false;
			}
		}
	}

	IEnumerator activate() {
		active = false;
		Color initialColor = GetComponent<SkinnedMeshRenderer>().material.color;
		Color inActiveColor = GetComponent<SkinnedMeshRenderer>().material.color;
		inActiveColor.r = 0f;
		inActiveColor.g = 0f;
		inActiveColor.b = 0f;
		GetComponent<SkinnedMeshRenderer>().material.color = inActiveColor;
		yield return new WaitForSeconds(3f);
		GetComponent<SkinnedMeshRenderer>().material.color = initialColor;
		active = true;
	}
}
