using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	public bool pressed;
	public Material openMaterial;
	public Material closedMaterial;
	public GameObject[] gates;
	public GameObject player;
	private Vector3 position;
	// Update is called once per frame
	void Start () {
		position = new Vector3(Mathf.Round (transform.position.x), Mathf.Round (transform.position.y), Mathf.Round(transform.position.z));
		pressed = false;
		transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
	}
	void Update () {
		/*
		if(position.Equals(new Vector3(Mathf.Round (player.transform.position.x), Mathf.Round (player.transform.position.y), Mathf.Round(player.transform.position.z)))) {
			pressed = true;
		} else if (GameObject.FindGameObjectsWithTag("clone").Length > 0) {
			foreach(GameObject item in GameObject.FindGameObjectsWithTag("clone")) {
				if(position.Equals(item.transform.position)) {
					pressed = true;
				}
			}
		} else {
			pressed = false;
		}

		if(pressed) {
			GetComponent<SkinnedMeshRenderer>().material = closedMaterial;
			foreach(GameObject item in gates) {
				item.GetComponent<Gate>().openGate();
			}
		} else {
			GetComponent<SkinnedMeshRenderer>().material = openMaterial;
			foreach(GameObject item in gates) {
				item.GetComponent<Gate>().closeGate();
			}
		}*/

		RaycastHit hit = new RaycastHit ();
		if(Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.20f, transform.position.z), Vector3.up, out hit, 1.2f)) {
			Debug.Log (hit.collider.gameObject.name);
			if(hit.collider.gameObject.name == "Player" || hit.collider.gameObject.name == "Clone(Clone)" && !pressed) {
				GetComponent<SkinnedMeshRenderer>().material = closedMaterial;
				foreach(GameObject item in gates) {
					item.GetComponent<Gate>().openGate();
				}
				pressed = true;
			}
		} else if(pressed){
			pressed = false;
			GetComponent<SkinnedMeshRenderer>().material = openMaterial;
			foreach(GameObject item in gates) {
				item.GetComponent<Gate>().closeGate();
			}
		}
	}

	void OnTriggerEnter(Collider coll) {
		/*
		if(coll.name == "Player" || coll.tag == "clone") {
			Debug.Log("button was collided with");
			GetComponent<SkinnedMeshRenderer>().material = closedMaterial;
			foreach(GameObject item in gates) {
				item.GetComponent<Gate>().openGate();
			}
		}*/
	}
	void OnTriggerExit(Collider coll) {
		/*
		if(coll.name == "Player" || coll.tag == "clone") {
			GetComponent<SkinnedMeshRenderer>().material = openMaterial;
			foreach(GameObject item in gates) {
				item.GetComponent<Gate>().closeGate();
			}
		}*/
	}

	public void unpresss() {
		GetComponent<SkinnedMeshRenderer>().material = openMaterial;
		foreach(GameObject item in gates) {
			item.GetComponent<Gate>().closeGate();
		}
	}
}
