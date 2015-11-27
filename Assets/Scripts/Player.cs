using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Player : MonoBehaviour {
	public GameObject canvas;
	public static string direction;
	public static string respawnDirection;
	public static Vector3 respawnLocation;
	public GameObject explosionParticle;
	public float speed;
	public float customFadeDistance;
	public bool dontMove;
	public PhysicMaterial sticky;
	public PhysicMaterial unsticky;
	private bool thingsAround = false;

	//possibleSkins
	public Material blueGuy;
	public Material blueGuyFiller;
	public Material whiteGuy;
	public Material whiteGuyFiller;
	public Material blackGuy;
	public Material blackGuyFiller;
	public Material iceSkin;
	public Material iceSkinFiller;
	public Material gold;
	public Material goldFiller;
	public Material smile;
	public Material smileFiller;
	public Material ghostSkin;
	public Material ghostSkinFiller;
	public Material smoke;
	public Material smokeFiller;
	public Material radioactive;
	public Material radioactiveFiller;
	public Material drake;
	public Material drakeFiller;

	Touch touch;
	bool canRespawn;

	private GameObject[] taggedGameObjects;
	void Start () {
		Debug.Log (PlayerPrefs.GetString ("selected"));
		if(PlayerPrefs.GetString ("selected") == "BlueGuy") {
			GetComponent<MeshRenderer>().material = blueGuy;
			GameObject.Find ("playerFiller").GetComponent<MeshRenderer>().material = blueGuyFiller;
			GameObject.Find ("Clone").GetComponent<MeshRenderer>().material = blueGuy;
			GameObject.Find ("ExplosionParticle").GetComponent<MeshRenderer>().material = blueGuy;
		} else if (PlayerPrefs.GetString ("selected") == "WhiteGuy") {
			GetComponent<MeshRenderer>().material = whiteGuy;
			GameObject.Find ("playerFiller").GetComponent<MeshRenderer>().material = whiteGuyFiller;
			GameObject.Find ("Clone").GetComponent<MeshRenderer>().material = whiteGuy;
			GameObject.Find ("ExplosionParticle").GetComponent<MeshRenderer>().material = whiteGuy;
		} else if (PlayerPrefs.GetString ("selected") == "BlackGuy") {
			GetComponent<MeshRenderer>().material = blackGuy;
			GameObject.Find ("playerFiller").GetComponent<MeshRenderer>().material = blackGuyFiller;
			GameObject.Find ("Clone").GetComponent<MeshRenderer>().material = blackGuy;
			GameObject.Find ("ExplosionParticle").GetComponent<MeshRenderer>().material = blackGuy;
		} else if (PlayerPrefs.GetString ("selected") == "Gold") {
			GetComponent<MeshRenderer>().material = gold;
			GameObject.Find ("playerFiller").GetComponent<MeshRenderer>().material = goldFiller;
			GameObject.Find ("Clone").GetComponent<MeshRenderer>().material = gold;
			GameObject.Find ("ExplosionParticle").GetComponent<MeshRenderer>().material = gold;
		} else if(PlayerPrefs.GetString ("selected") == "Ice") {
			GetComponent<MeshRenderer>().material = iceSkin;
			GameObject.Find ("playerFiller").GetComponent<MeshRenderer>().material = iceSkinFiller;
			GameObject.Find ("Clone").GetComponent<MeshRenderer>().material = iceSkin;
			GameObject.Find ("ExplosionParticle").GetComponent<MeshRenderer>().material = iceSkin;
		} else if(PlayerPrefs.GetString ("selected") == "Smile") {
			GetComponent<MeshRenderer>().material = smile;
			GameObject.Find ("playerFiller").GetComponent<MeshRenderer>().material = smileFiller;
			GameObject.Find ("Clone").GetComponent<MeshRenderer>().material = smile;
			GameObject.Find ("ExplosionParticle").GetComponent<MeshRenderer>().material = smile;
		} else if(PlayerPrefs.GetString ("selected") == "Ghost") {
			GetComponent<MeshRenderer>().material = ghostSkin;
			GameObject.Find ("playerFiller").GetComponent<MeshRenderer>().material = ghostSkinFiller;
			GameObject.Find ("Clone").GetComponent<MeshRenderer>().material = ghostSkin;
			GameObject.Find ("ExplosionParticle").GetComponent<MeshRenderer>().material = ghostSkin;
		} else if(PlayerPrefs.GetString ("selected") == "Radioactive") {
			GetComponent<MeshRenderer>().material = radioactive;
			GameObject.Find ("playerFiller").GetComponent<MeshRenderer>().material = radioactiveFiller;
			GameObject.Find ("Clone").GetComponent<MeshRenderer>().material = radioactive;
			GameObject.Find ("ExplosionParticle").GetComponent<MeshRenderer>().material = radioactive;
		}else if(PlayerPrefs.GetString ("selected") == "Drake") {
			GetComponent<MeshRenderer>().material = drake;
			GameObject.Find ("playerFiller").GetComponent<MeshRenderer>().material = drakeFiller;
			GameObject.Find ("Clone").GetComponent<MeshRenderer>().material = drake;
			GameObject.Find ("ExplosionParticle").GetComponent<MeshRenderer>().material = gold;
		}
		canRespawn = true;
		dontMove = false;
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

	bool IsGrounded () {
		RaycastHit[] hits;
		hits = Physics.SphereCastAll(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.25f, Vector3.down, 0.8f);
		foreach(RaycastHit bang in hits) {
			if(bang.collider.gameObject.name == "Cube" || bang.collider.gameObject.name == "Clone(Clone)" || bang.collider.gameObject.name == "IceCube") {
				dontMove = false;
				Debug.Log ("there is a cube or clone under");
				if(thingsAround == false) {
					GetComponent<Collider>().material = sticky;
				}
				return true;
			}
		}
		return false;
	}
	// Update is called once per frame
	void Update () {
		if(GameManager.started) {
			RaycastHit hit;
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
		    	GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
				if(!dontMove) {
					GetComponent<Rigidbody>().velocity = new Vector3(0f, GetComponent<Rigidbody>().velocity.y, -speed);
				}
				Vector3 currentPosition = transform.position;
				currentPosition.x = Mathf.Round (transform.position.x);
				transform.position = currentPosition;
			}
			if(Input.GetKeyDown(KeyCode.UpArrow)) {
				if(IsGrounded()) {
					GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 20f, GetComponent<Rigidbody>().velocity.z);
				}
			}

			if(Input.GetKeyDown(KeyCode.Space)) {
				canvas.GetComponent<GameManager>().respawn();
			}

			foreach (Touch touch in Input.touches) {
				if (touch.phase == TouchPhase.Began && touch.position.x < Screen.width/2 && canRespawn) {
					canvas.GetComponent<GameManager>().respawn();
					canRespawn = false;
					StartCoroutine(resetTouch());
				}
				else if (touch.phase == TouchPhase.Began && touch.position.x > Screen.width/2) {
					if(IsGrounded()) {
						GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 20f, GetComponent<Rigidbody>().velocity.z);
					}
				} 
			}
			//check which blocks are close and fade in
			// loop through each tagged object, remembering nearest one found
			taggedGameObjects = GameObject.FindGameObjectsWithTag("Platform"); 
			foreach (GameObject platform in taggedGameObjects) {
				Vector3 objectPos = platform.transform.position;
				float distanceSqr = (objectPos - transform.position).sqrMagnitude;
				if(customFadeDistance != 0) {
					if (distanceSqr < customFadeDistance) {
						StartCoroutine(FadeIn (platform, 0.23f));
					}
				} else if (distanceSqr < 53f) {
					StartCoroutine(FadeIn (platform, 0.23f));
				}
			}
			if(direction == "+x" && Physics.SphereCast(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.24f, Vector3.right, out hit, 0.4f)) {
				if(hit.collider.gameObject.name == "Cube" || hit.collider.gameObject.name == "IceCube" || hit.collider.gameObject.name == "Clone(Clone)") {
					Debug.Log ("something in +x direction");
					//dontMove = true;
					GetComponent<Collider>().material = unsticky;
					thingsAround = true;
					speed = 0f;
				} else {
						speed = 5f;
				}
			} else if(direction == "-x" && Physics.SphereCast(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.24f, Vector3.left, out hit, 0.4f)) {
				if(hit.collider.gameObject.name == "Cube" || hit.collider.gameObject.name == "IceCube" || hit.collider.gameObject.name == "Clone(Clone)") {
					Debug.Log(hit.collider.gameObject.name);
					Debug.Log ("something in -x direction");
					//dontMove = true;
					GetComponent<Collider>().material = unsticky;
					thingsAround = true;
					speed = 0f;
				} else {
						speed = 5f;
				}
			} else if(direction == "+z" && Physics.SphereCast(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.24f, Vector3.forward, out hit, 0.4f)) {
				if(hit.collider.gameObject.name == "Cube" || hit.collider.gameObject.name == "IceCube" || hit.collider.gameObject.name == "Clone(Clone)") {
					Debug.Log ("something in +z direction");
					//dontMove = true;
					GetComponent<Collider>().material = unsticky;
					thingsAround = true;
					speed = 0f;
				} else {
					speed = 5f;
				}
			} else if(direction == "-z" && Physics.SphereCast(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.24f, Vector3.back, out hit, 0.4f)) {
				if(hit.collider.gameObject.name == "Cube" || hit.collider.gameObject.name == "IceCube" || hit.collider.gameObject.name == "Clone(Clone)") {
					Debug.Log ("something in -z direction");
					//dontMove = true;
					GetComponent<Collider>().material = unsticky;
					thingsAround = true;
					speed = 0f;
				} else {
					speed = 5f;
				}
			}  else {
				if(Physics.SphereCast(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.24f, Vector3.up, out hit, 0.8f)) {
					GetComponent<Collider>().material = unsticky;
					thingsAround = true;
					speed = 5f;
				} else {
					GetComponent<Collider>().material = sticky;
					thingsAround = false;
					speed = 5f;
				}
			}
		} else {
			//GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
		}
	}

	IEnumerator resetTouch() {
		yield return new WaitForSeconds(0.3f);
		canRespawn = true;
	}

	public void jump() {
		if(IsGrounded()) {
			GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 20f, GetComponent<Rigidbody>().velocity.z);
		}
	}

	public IEnumerator FadeIn (GameObject platform, float duration)
	{
		Material mat = platform.GetComponentInChildren<SkinnedMeshRenderer>().material;
		platform.tag = "Untagged";
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
		} else if (platform.name == "Gate" || platform.name == "Button" || platform.name == "Portal") {
			while(mat.color.a < 0.76f)
			{
				Color newColor = mat.color;
				newColor.a += Time.deltaTime / duration;
				platform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = newColor;
				yield return null;
			}
		} else if (platform.name == "IceCube") {
			while(mat.color.a < 0.4f)
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

	public void die() {
		canvas.GetComponent<GameManager>().die();
	}
	
	public void explode() {
		int i = 0;
		while(i < 7) {
			i++;
			GameObject newParticle = (GameObject)Instantiate(explosionParticle, transform.position, Quaternion.identity);
		}
	}

	void OnTriggerEnter(Collider coll) {
		if(coll.name == "Snowball(Clone)" || coll.name == "Snowman" || coll.name == "Pylon") {
			canvas.GetComponent<GameManager>().die();
		}
	}
}
