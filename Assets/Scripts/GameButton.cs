using UnityEngine;
using System.Collections;

public class GameButton : MonoBehaviour {
	public bool pressed;
	public bool invertButton;
	public Material openMaterial;
	public Material closedMaterial;
	public GameObject[] gates;
	public GameObject player;
	private bool targetHit;
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
		RaycastHit[] hits;
		hits = Physics.RaycastAll(new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z), Vector3.up, 1.2f);
		Debug.Log (hits.Length);
		foreach(RaycastHit hit in hits) {
			Debug.Log (hit.collider.name);
			if(hit.collider.gameObject.name == "Player" || hit.collider.gameObject.name == "Clone(Clone)") {
				targetHit = true;
			}
		}
		if(!pressed && targetHit) {
			GetComponent<SkinnedMeshRenderer>().material = closedMaterial;
			foreach(GameObject item in gates) {
				if(invertButton) {
					item.GetComponent<Gate>().closeGate();
				} else {
					item.GetComponent<Gate>().openGate();
				}
			}
			pressed = true;
		} else if(pressed && !targetHit){
			pressed = false;
			GetComponent<SkinnedMeshRenderer>().material = openMaterial;
			foreach(GameObject item in gates) {
				if(invertButton) {
					item.GetComponent<Gate>().openGate();
				} else {
					item.GetComponent<Gate>().closeGate();
				}
			}
		}
		targetHit = false;
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
