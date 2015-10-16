using UnityEngine;
using System.Collections;

public class Firefly : MonoBehaviour {
	public float flyRange;
	public GameObject player;
	private Vector3 initialPosition;
	public float minPlayerDistance;
	// Use this for initialization
	void Start () {
		minPlayerDistance = 4f;
		initialPosition = transform.position;
		StartCoroutine(moveAround());
	}

	void Update() {
		if(Vector3.Distance(player.transform.position, transform.position) < minPlayerDistance) {
			//Vector3.
		}
	}
	
	IEnumerator moveAround() {
		while(true) {
			float zforce;
			float yforce;
			float xforce;
			if(transform.position.z < initialPosition.z - flyRange) {
				zforce = Random.Range (1f, 2f);
			} else if(transform.position.z > initialPosition.z + flyRange){
				zforce = Random.Range (-2f, -1f);
			} else {
				zforce = Random.Range (-2f, 2f);
			}
			if(transform.position.x < initialPosition.x - flyRange) {
				xforce = Random.Range (1f, 2f);
			} else if(transform.position.x > initialPosition.x + flyRange){
				xforce = Random.Range (-2f, -1f);
			} else {
				xforce = Random.Range (-2f, 2f);
			}
			if(transform.position.y < initialPosition.y) {
				yforce = Random.Range (1f, 2f);
			} else if(transform.position.y > initialPosition.y + flyRange) {
				yforce = Random.Range (-0f, -1f);
			} else {
				yforce = Random.Range (-2f, 2f);
			}
			if(Random.Range (0f, 10f) > 7f) {
				gameObject.GetComponent<Rigidbody>().velocity = new Vector3(xforce, yforce, zforce);
			} else {
				gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
			}
			gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range (-70f, 70f), Random.Range (-70f, 70f), Random.Range (-70f, 70f)));
			yield return new WaitForSeconds(Random.Range (0.6f, 1f));
		}
	}
}
