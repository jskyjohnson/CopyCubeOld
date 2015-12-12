using UnityEngine;
using System.Collections;

public class FakeHomePlayer : MonoBehaviour {
	public string direction;
	public float speed;
	// Use this for initialization
	void Start () {
		speed = 5f;
		Physics.gravity = new Vector3(0, -50f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (direction == "+x")
		{
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
			GetComponent<Rigidbody>().velocity = new Vector3(speed, GetComponent<Rigidbody>().velocity.y, 0f);
			Vector3 currentPosition = transform.position;
			currentPosition.z = Mathf.Round(transform.position.z);
			transform.position = currentPosition;
		}
		else if (direction == "+z")
		{
			// transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0f, 0f));
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			GetComponent<Rigidbody>().velocity = new Vector3(0f, GetComponent<Rigidbody>().velocity.y, speed);
			Vector3 currentPosition = transform.position;
			currentPosition.x = Mathf.Round(transform.position.x);
			transform.position = currentPosition;
		}
	}
}
