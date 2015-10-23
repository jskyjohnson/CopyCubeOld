using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {
	public GameObject linkedPortal;
	private Vector3 linkedPortalLocation;
	// Use this for initialization
	void Start () {
		linkedPortalLocation = new Vector3(linkedPortal.transform.position.x, Mathf.Round (linkedPortal.transform.position.y), linkedPortal.transform.position.z);
	}
	
	void OnTriggerEnter(Collider coll) {
		if(linkedPortal && coll.name == "Player") {
			coll.gameObject.transform.position = linkedPortalLocation;
		}
	}
}
