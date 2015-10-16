using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public static string direction;
	public static string respawnDirection;
	public static Vector3 respawnLocation;
	public GameObject explosionParticle;
	public float speed;
	public int jumpsAvailable;
	public bool dontMove;
	public PhysicMaterial sticky;
	public PhysicMaterial unsticky;

	private GameObject[] taggedGameObjects;
	void Start () {
		dontMove = false;
		jumpsAvailable = 1;
		direction = "+x";
		respawnLocation = new Vector3(0f, 4f, 0f);
		respawnDirection = direction;
		Physics.gravity = new Vector3(0, -50f, 0);
		taggedGameObjects = GameObject.FindGameObjectsWithTag("Platform"); 
		foreach(GameObject platform in taggedGameObjects) {
			Material mat = platform.GetComponentInChildren<SkinnedMeshRenderer>().material;
			Color newColor = mat.color;
			newColor.a = 0f;
			platform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = newColor;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.started) {
			RaycastHit hit = new RaycastHit ();
			if(direction == "+x") {
		  		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
				if(!dontMove) {
					GetComponent<Rigidbody>().velocity = new Vector3(speed, GetComponent<Rigidbody>().velocity.y, 0f);
				}
				Vector3 currentPosition = transform.position;
		  		currentPosition.z = Mathf.Round (transform.position.z);
		 		 transform.position = currentPosition;
			} else if(direction == "-x") {
		    	GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
	     		if(!dontMove) {
					GetComponent<Rigidbody>().velocity = new Vector3(-speed, GetComponent<Rigidbody>().velocity.y, 0f);
				}
				Vector3 currentPosition = transform.position;
				currentPosition.z = Mathf.Round (transform.position.z);
				transform.position = currentPosition;
			} else if(direction == "+z") {
		   		// transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0f, 0f));
		    	GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
				if(!dontMove) {
				GetComponent<Rigidbody>().velocity = new Vector3(0f, GetComponent<Rigidbody>().velocity.y, speed);
				}
				Vector3 currentPosition = transform.position;
				currentPosition.x = Mathf.Round (transform.position.x);
				transform.position = currentPosition;
			} else if (direction == "-z") {
		   		// transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0f, 0f));
		    	GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
				if(!dontMove) {
					Debug.Log ("adding force to -z direction");
					GetComponent<Rigidbody>().velocity = new Vector3(0f, GetComponent<Rigidbody>().velocity.y, -speed);
				}
				Vector3 currentPosition = transform.position;
				currentPosition.x = Mathf.Round (transform.position.x);
				transform.position = currentPosition;
			}
			if(Input.GetKeyDown(KeyCode.UpArrow) && jumpsAvailable >= 1) {
				jumpsAvailable --;
				GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 20f, GetComponent<Rigidbody>().velocity.z);
			}
			//check which blocks are close and fade in
			// loop through each tagged object, remembering nearest one found
			taggedGameObjects = GameObject.FindGameObjectsWithTag("Platform"); 
			foreach (GameObject platform in taggedGameObjects) {
				Vector3 objectPos = platform.transform.position;
				float distanceSqr = (objectPos - transform.position).sqrMagnitude;
				if (distanceSqr < 53f) {
					StartCoroutine(FadeIn (platform, 1f));
				}
			}


		} else {
			//GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
		}
	}

	public void jump() {
		if(jumpsAvailable >= 1) {
			jumpsAvailable --;
			GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 20f, GetComponent<Rigidbody>().velocity.z);
		}
	}

	public IEnumerator FadeIn (GameObject platform, float duration)
	{
		Material mat = platform.GetComponentInChildren<SkinnedMeshRenderer>().material;
		if(platform.name == "End") {
			while(mat.color.a < 0.3f)
			{
				Color newColor = mat.color;
				newColor.a += Time.deltaTime / duration;
				platform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = newColor;
				yield return null;
			} 
		} else if (platform.name == "Checkpoint"){
			while(mat.color.a < 0.5f)
			{
				Color newColor = mat.color;
				newColor.a += Time.deltaTime / duration;
				platform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = newColor;
				yield return null;
			} 
		} else {
			while(mat.color.a < 1f)
			{
				Color newColor = mat.color;
				newColor.a += Time.deltaTime / duration;
				platform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = newColor;
				yield return null;
			} 
		}
	}

	public void explode() {
		int i = 0;
		while(i < 7) {
			i++;
			GameObject newParticle = (GameObject)Instantiate(explosionParticle, transform.position, Quaternion.identity);
		}
	}

	void OnCollisionEnter(Collision coll) {
		if(coll.gameObject.name == "Cube" || coll.gameObject.name == "Clone(Clone)") {
			RaycastHit hit = new RaycastHit ();
			bool thingsAround = false;
			if(direction == "+x" && Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z), Vector3.right, out hit, 1.1f)) {
				if(hit.collider.gameObject.name == "Cube" || hit.collider.gameObject.name == "Clone(Clone)") {
					Debug.Log ("something in +x direction");
					//dontMove = true;
					GetComponent<Collider>().material = unsticky;
					thingsAround = true;
				}
			} else if(direction == "-x" && Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z), Vector3.left, out hit, 1.1f)) {
				if(hit.collider.gameObject.name == "Cube" || hit.collider.gameObject.name == "Clone(Clone)") {
					Debug.Log(hit.collider.gameObject.name);
					Debug.Log ("something in -x direction");
					//dontMove = true;
					GetComponent<Collider>().material = unsticky;
					thingsAround = true;
				}
			} else if(direction == "+z" && Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z), Vector3.forward, out hit, 1.1f)) {
				if(hit.collider.gameObject.name == "Cube" || hit.collider.gameObject.name == "Clone(Clone)") {
					Debug.Log ("something in +z direction");
					//dontMove = true;
					GetComponent<Collider>().material = unsticky;
					thingsAround = true;
				}
			} else if(direction == "-z" && Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z), Vector3.back, out hit, 1.1f)) {
				if(hit.collider.gameObject.name == "Cube" || hit.collider.gameObject.name == "Clone(Clone)") {
					Debug.Log ("something in -z direction");
					//dontMove = true;
					GetComponent<Collider>().material = unsticky;
					thingsAround = true;
				}
			}
			if(transform.position.y > coll.gameObject.transform.position.y + 0.3f) {
				jumpsAvailable = 1;
				dontMove = false;
				if(thingsAround == false) {
					GetComponent<Collider>().material = sticky;
				}
			}
		}
	}
}
