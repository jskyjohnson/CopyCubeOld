using UnityEngine;
using System.Collections;

public class Unsticky : MonoBehaviour {
	private float time;
	private bool exited;
	public GameObject player;
	public PhysicMaterial unsticky;
	void OnCollisionEnter(Collision coll) {
		player.GetComponent<Collider>().material = unsticky;
	}
}
