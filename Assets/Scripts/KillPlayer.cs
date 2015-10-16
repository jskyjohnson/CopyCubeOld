using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {
	public GameObject GameManager;
	void OnCollisionEnter(Collision coll) {
		if(coll.gameObject.name == "Player") {
			GameManager.GetComponent<GameManager>().die();
		}
	}
}
