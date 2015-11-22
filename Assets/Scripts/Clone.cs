using UnityEngine;
using System.Collections;

public class Clone : MonoBehaviour {
	void Start() {
		if(name == "Clone(Clone)") {
			tag = "clone";
		} else {
			transform.position = new Vector3(-3000f, -3000f, -3000f);
		}
	}

	void OnTriggerEnter(Collider coll) {
		if(coll.name == "ButtonChangeDirection" || coll.name == "DirectionSwitcher" || coll.name == "Gate" || coll.name == "Portal") {
			Destroy (gameObject);
		}
		if(coll.name == "Snowman") {
			Debug.Log ("fadeout called");
			StartCoroutine(FadeOut());
		}
	}

	public IEnumerator FadeOut ()
	{
		Material mat = gameObject.GetComponent<MeshRenderer>().material;
		while(mat.color.a > 0f) {
			Color newColor = mat.color;
			newColor.a -= Time.deltaTime * 1.4f;
			gameObject.GetComponent<MeshRenderer>().material.color = newColor;
			yield return null;
		} 
		Destroy (gameObject);
	}
}
