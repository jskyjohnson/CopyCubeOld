using UnityEngine;
using System.Collections;

public class Snowball : MonoBehaviour {
	public string direction;
	public float bulletSpeed;
	public GameObject energyParticle;
	bool active;
	Vector3 localPosition;
	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3(0f, 0f, 0f);
		active = false;
		StartCoroutine(buildAndShoot());
		if(name == "Snowball(Clone)") {
			StartCoroutine(decay ());
		}
	}

	IEnumerator buildAndShoot() {
		if(name != "Snowball") {
			while(transform.localScale.x < 0.3f) {
				Vector3 newSize = transform.localScale;
				newSize.x += 0.01f;
				newSize.y += 0.01f;
				newSize.z += 0.01f;
				transform.localScale = newSize;
				if(Random.Range (0f, 1f) < 0.3f) {
					Instantiate (energyParticle, transform.position, Quaternion.identity);
				}
				yield return new WaitForSeconds(0.02f);
			}
			gameObject.AddComponent<BoxCollider>();
			gameObject.AddComponent<Rigidbody>();
			gameObject.GetComponent<Rigidbody>().useGravity = false;
			GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-360f, 360f), Random.Range(-360f, 360f), Random.Range(-360f, 360f)));
			gameObject.GetComponent<BoxCollider>().isTrigger = true;
			active = true;
			yield return 0;
		}
	}

	void Update() {
		if(active) {
			if(!GameObject.Find ("Canvas").GetComponent<GameManager>().paused) {
				if(direction == "+x") {
					GetComponent<Rigidbody>().velocity = new Vector3(bulletSpeed, 0f, 0f);
				} else if (direction == "-x") {
					GetComponent<Rigidbody>().velocity = new Vector3(-bulletSpeed, 0f, 0f);
				} else if (direction == "+z") {
					GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, bulletSpeed);
				} else if (direction == "-z") {
					GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, -bulletSpeed);
				}
			} else {
				GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
			}
		}
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.name == "Player" || coll.name == "Clone(Clone)" || coll.name == "Cube" || coll.name == "IceCube") {
			Destroy(gameObject);
		}
		if(coll.name == "Gate") {
			if(!coll.GetComponent<Gate>().open) {
				Destroy(gameObject);
			}
		}
	}

	IEnumerator decay() {
		yield return new WaitForSeconds(8f);
		Destroy(gameObject);
	}
}
