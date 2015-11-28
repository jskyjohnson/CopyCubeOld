using UnityEngine;
using System.Collections;

public class Pylon : MonoBehaviour {
	public GameObject snowball;
	public string direction;
	public bool moving;
	public bool smart;
	public float moveSpeed;
	public GameObject body;
	float currentSpeed;
	bool shooting;
	float bulletSpeed;
	bool loading;
	// Use this for initialization
	void Start () {
		bulletSpeed = 14f;
		moveSpeed = 0.5f;
		shooting = true;
		RaycastHit hit;
		if(moving || smart) {
			if(direction == "+x" && Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Vector3.right, out hit, 14f)) {
				if(hit.collider.name == "Player") {
					Debug.Log ("setting shooting to true +x");
					shooting = true;
				} else {
					shooting = false;
				}
			} else if (direction == "-x" && Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Vector3.left, out hit, 14f)) {
				if(hit.collider.name == "Player") {
					Debug.Log ("setting shooting to true -x");
					shooting = true;
				} else {
					shooting = false;
				}
			} else if (direction == "+z" && Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Vector3.forward, out hit, 14f)) {
				Debug.Log (hit.collider.name);
				if(hit.collider.name == "Player") {
					Debug.Log ("setting shooting to true +z");
					shooting = true;
				} else {
					shooting = false;
				}
			} else if (direction == "-z" && Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Vector3.back, out hit, 14f)) {
				if(hit.collider.name == "Player") {
					Debug.Log ("setting shooting to true -z");
					shooting = true;
				} else {
					shooting = false;
				}
			} else {
				shooting = false;
			}
		}
		StartCoroutine(ShootCoroutine());
	}
	
	// Update is called once per frame
	void Update () {
		if(moving && !shooting && !loading && !GameObject.Find ("Canvas").GetComponent<GameManager>().paused) {
			if(direction == "+x") {
				GetComponent<Rigidbody>().velocity = new Vector3(moveSpeed, 0f, 0f);
			} else if (direction == "-x") {
				GetComponent<Rigidbody>().velocity = new Vector3(-moveSpeed, 0f, 0f);
			} else if (direction == "+z") {
				GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, moveSpeed);
			} else if (direction == "-z") {
				GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, -moveSpeed);
			}
		} else {
			GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
		}
		RaycastHit hit;
		if(moving || smart) {
			if(direction == "+x" && Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Vector3.right, out hit, 7f)) {
				if(hit.collider.name == "Player") {
					Debug.Log ("setting shooting to true +x");
					shooting = true;
				} else {
					shooting = false;
				}
			} else if (direction == "-x" && Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Vector3.left, out hit, 7f)) {
				if(hit.collider.name == "Player") {
					Debug.Log ("setting shooting to true -x");
					shooting = true;
				} else {
					shooting = false;
				}
			} else if (direction == "+z" && Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Vector3.forward, out hit, 7f)) {
				Debug.Log (hit.collider.name);
				if(hit.collider.name == "Player") {
					Debug.Log ("setting shooting to true +z");
					shooting = true;
				} else {
					shooting = false;
				}
			} else if (direction == "-z" && Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Vector3.back, out hit, 7f)) {
				if(hit.collider.name == "Player") {
					Debug.Log ("setting shooting to true -z");
					shooting = true;
				} else {
					shooting = false;
				}
			} else {
				shooting = false;
			}
		}
	}
	
	IEnumerator ShootCoroutine() {
		while(true) {
			if(shooting && !GameObject.Find ("Canvas").GetComponent<GameManager>().paused) {
				Shoot ();
				loading = true;
				yield return new WaitForSeconds(2.0f);
				loading = false;
			}
			yield return 0;
		}
	}
	
	IEnumerator chargeGun() {
		Material mat = body.GetComponent<SkinnedMeshRenderer>().material;
		Color currentColor = mat.color;
		Color targetColor = new Color(1f, 1f, 1f);
		while(currentColor != targetColor) {
			mat = body.GetComponent<SkinnedMeshRenderer>().material;
			currentColor = mat.color;
			mat.color = Color.Lerp (currentColor, targetColor, 0.5f);
			body.GetComponent<SkinnedMeshRenderer>().material = mat;
			yield return new WaitForSeconds(0.05f);
		}
		StartCoroutine(dechargeGun());
	}
	
	IEnumerator dechargeGun() {
		Material mat = body.GetComponent<SkinnedMeshRenderer>().material;
		Color currentColor = mat.color;
		Color targetColor = new Color(0.57f, 1f, 1f);
		while(currentColor != targetColor) {
			mat = body.GetComponent<SkinnedMeshRenderer>().material;
			currentColor = mat.color;
			mat.color = Color.Lerp (currentColor, targetColor, 0.5f);
			body.GetComponent<SkinnedMeshRenderer>().material = mat;
			yield return new WaitForSeconds(0.01f);
		}
	}
	
	void Shoot() {
		GameObject bullet = (GameObject)Instantiate (snowball, snowball.transform.position, Quaternion.identity);
		bullet.GetComponent<Snowball>().direction = direction;
		bullet.GetComponent<Snowball>().bulletSpeed = bulletSpeed;
		StartCoroutine(chargeGun());
	}
}
