using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {
	public string direction;
	private bool finishedMoving;
	private bool collided;
	// Use this for initialization
	void Start () {
	}

	void OnCollisionEnter(Collision coll) {
		if(coll.gameObject.name == "Player") {
			coll.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
			GetComponent<Rigidbody>().isKinematic = false;
			direction = Player.direction;
		//	collided = true;
		}
		if(coll.gameObject.name == "Cube") {
		//	finishedMoving = true;
		}
	}
	// Update is called once per frame
	void Update () {
		if(collided && !finishedMoving) {
		//	GameObject.Find ("Player").GetComponent<Player>().speed = 0f;
		}
		if(direction == "+x") {
			gameObject.GetComponent<Rigidbody>().velocity = new Vector3(5f, 0f, 0f);
		} else if(direction == "-x") {
			gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-5f, 0f, 0f);
		} else if(direction == "+z") {
			gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 5f);
		} else if(direction == "-z") {
			gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, -5f);
		} else {
			gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
		}
	}
}
