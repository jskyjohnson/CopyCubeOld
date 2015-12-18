using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Player : MonoBehaviour
{
    public GameObject canvas;
    public static string direction;
    public static string respawnDirection;
    public static Vector3 respawnLocation;
	public static bool respawnInverted;
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
    //Audiorelated
    private AudioSource source;
    public AudioClip jumpsound;
    public AudioClip killPlylon;
    public AudioClip killSnowman;
    public AudioClip respawnsound;
    public AudioClip spawnsound;
    public float jumpvol;
	Touch touch;
	bool canRespawn;
	bool delayIsGrounded;
	public bool isStuck;
	public bool immuneToStuck;
	public bool inverted;
	float timePassed;
	bool isTouchingObject;
	private GameObject[] taggedGameObjects;
	void Start () {
		timePassed = 0f;
		respawnInverted = false;
		inverted = false;
		isStuck = false;
		source = GetComponent<AudioSource>();
		source.PlayOneShot(spawnsound, 1f);

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
		isTouchingObject = false;
		taggedGameObjects = GameObject.FindGameObjectsWithTag("Platform");
		foreach(GameObject platform in taggedGameObjects) {
			Material mat = platform.GetComponentInChildren<SkinnedMeshRenderer>().material;
			Color newColor = mat.color;
			newColor.a = 0f;
			platform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = newColor;
		}
	}

    bool IsGrounded()
    {
		if(delayIsGrounded == true) {
			return true;
		}
        RaycastHit[] hits;
		if(!inverted) {
			hits = Physics.SphereCastAll(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.45f, Vector3.down, 0.5f);
    	    foreach (RaycastHit bang in hits) {
				Debug.Log (transform.position.y - 0.80f - bang.collider.transform.position.y);
       	    	if (bang.collider.gameObject.name == "Cube" || bang.collider.gameObject.name == "Clone(Clone)" || bang.collider.gameObject.name == "IceCube" && bang.collider.transform.position.y <= transform.position.y - 0.80f) {
                	dontMove = false;
               		if (thingsAround == false) {
                	    GetComponent<Collider>().material = sticky;
               		}
               		return true;
            	}
       		}
        	return false;
		} else {
			hits = Physics.SphereCastAll(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.45f, Vector3.up, 0.5f);
			foreach (RaycastHit bang in hits) {
				if (bang.collider.gameObject.name == "Cube" || bang.collider.gameObject.name == "Clone(Clone)" || bang.collider.gameObject.name == "IceCube" && bang.collider.transform.position.y >= transform.position.y + 0.80f) {
					dontMove = false;
					if (thingsAround == false) {
						GetComponent<Collider>().material = sticky;
					}
					return true;
				}
			}
			return false;
		}
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.started)
		Debug.Log("Game manager is started");
        {
            RaycastHit hit;
			Debug.Log("Direction is: " + direction);
            if (direction == "+x")
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
				Debug.Log("dontMove is: " + dontMove);
				if (!dontMove)
                {
					Debug.Log("setting velocity");
                    GetComponent<Rigidbody>().velocity = new Vector3(speed, GetComponent<Rigidbody>().velocity.y, 0f);
                }
                Vector3 currentPosition = transform.position;
                currentPosition.z = Mathf.Round(transform.position.z);
                transform.position = currentPosition;
            }
            else if (direction == "-x")
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
                if (!dontMove)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(-speed, GetComponent<Rigidbody>().velocity.y, 0f);
                }
                Vector3 currentPosition = transform.position;
                currentPosition.z = Mathf.Round(transform.position.z);
                transform.position = currentPosition;
            }
            else if (direction == "+z")
            {
                // transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0f, 0f));
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                if (!dontMove)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(0f, GetComponent<Rigidbody>().velocity.y, speed);
                }
                Vector3 currentPosition = transform.position;
                currentPosition.x = Mathf.Round(transform.position.x);
                transform.position = currentPosition;
            }
            else if (direction == "-z")
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                if (!dontMove)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(0f, GetComponent<Rigidbody>().velocity.y, -speed);
                }
                Vector3 currentPosition = transform.position;
                currentPosition.x = Mathf.Round(transform.position.x);
                transform.position = currentPosition;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
               jump ();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                source.PlayOneShot(respawnsound, 1f);
                Debug.Log("Playing Sound" + respawnsound);
                canvas.GetComponent<GameManager>().respawn();
            }

            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began && touch.position.x < Screen.width / 2)
                {
					source.PlayOneShot(respawnsound, 1f);
					Debug.Log("Playing Sound" + respawnsound);
					canvas.GetComponent<GameManager>().respawn();
                }
                else if (touch.phase == TouchPhase.Began && touch.position.x > Screen.width / 2)
                {
                    jump ();
                }
            }
            //check which blocks are close and fade in
            // loop through each tagged object, remembering nearest one found
            taggedGameObjects = GameObject.FindGameObjectsWithTag("Platform");
            foreach (GameObject platform in taggedGameObjects)
            {
                Vector3 objectPos = platform.transform.position;
                float distanceSqr = (objectPos - transform.position).sqrMagnitude;
                if (customFadeDistance != 0)
                {
                    if (distanceSqr < customFadeDistance)
                    {
                        StartCoroutine(FadeIn(platform, 0.23f));
                    }
                }
                else if (distanceSqr < 47f)
                {
                    StartCoroutine(FadeIn(platform, 0.23f));
                }
            }
            if (direction == "+x" && Physics.SphereCast(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.3f, Vector3.right, out hit, 0.4f))
            {
                if (hit.collider.gameObject.name == "Cube" || hit.collider.gameObject.name == "IceCube" || hit.collider.gameObject.name == "Clone(Clone)")
                {
                    Debug.Log("something in +x direction");
                    //dontMove = true;
                    GetComponent<Collider>().material = unsticky;
                    thingsAround = true;
                    speed = 0f;
                }
                else
                {
                    speed = 5f;
                }
            }
            else if (direction == "-x" && Physics.SphereCast(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.3f, Vector3.left, out hit, 0.4f))
            {
                if (hit.collider.gameObject.name == "Cube" || hit.collider.gameObject.name == "IceCube" || hit.collider.gameObject.name == "Clone(Clone)")
                {
                    Debug.Log(hit.collider.gameObject.name);
                    Debug.Log("something in -x direction");
                    //dontMove = true;
                    GetComponent<Collider>().material = unsticky;
                    thingsAround = true;
                    speed = 0f;
                }
                else
                {
                    speed = 5f;
                }
            }
            else if (direction == "+z" && Physics.SphereCast(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.3f, Vector3.forward, out hit, 0.4f))
            {
                if (hit.collider.gameObject.name == "Cube" || hit.collider.gameObject.name == "IceCube" || hit.collider.gameObject.name == "Clone(Clone)")
                {
                    Debug.Log("something in +z direction");
                    //dontMove = true;
                    GetComponent<Collider>().material = unsticky;
                    thingsAround = true;
                    speed = 0f;
                }
                else
                {
                    speed = 5f;
                }
            }
            else if (direction == "-z" && Physics.SphereCast(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.3f, Vector3.back, out hit, 0.4f))
            {
                if (hit.collider.gameObject.name == "Cube" || hit.collider.gameObject.name == "IceCube" || hit.collider.gameObject.name == "Clone(Clone)")
                {
                    Debug.Log("something in -z direction");
                    //dontMove = true;
                    GetComponent<Collider>().material = unsticky;
                    thingsAround = true;
                    speed = 0f;
                }
                else
                {
                    speed = 5f;
                }
            }
            else
            {
				if(!inverted) {
					if (Physics.SphereCast(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.24f, Vector3.up, out hit, 0.8f) && hit.collider.name != "Gate")
						{
               			     GetComponent<Collider>().material = unsticky;
							thingsAround = true;
							speed = 5f;
               		 } else {
                  	 	 GetComponent<Collider>().material = sticky;
                  	 	 thingsAround = false;
                  	 	 speed = 5f;
                	}
				} else {
					if (Physics.SphereCast(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.24f, Vector3.down, out hit, 0.8f) && hit.collider.name != "Gate")
					{
						GetComponent<Collider>().material = unsticky;
						thingsAround = true;
						speed = 5f;
					} else {
						GetComponent<Collider>().material = sticky;
						thingsAround = false;
						speed = 5f;
					}
				}
        	}
      	  }
    }

    IEnumerator resetTouch()
    {
        yield return new WaitForSeconds(0.3f);
        canRespawn = true;
    }

	IEnumerator StuckImmunity() {
		GetComponent<Rigidbody>().isKinematic = false;
		immuneToStuck = true;
		isStuck = false;
		yield return new WaitForSeconds(0.2f);
		immuneToStuck = false;
	}

    public void jump()
    {
		if(isStuck) {
			StartCoroutine(StuckImmunity());
		}
		Debug.Log (IsGrounded());
        if (IsGrounded()) //|| isTouchingObject)
        {
			source.PlayOneShot(jumpsound, jumpvol);
			PlayerPrefs.SetInt("timesJumped", PlayerPrefs.GetInt("timesJumped") + 1);
			Debug.Log("Playing Sound " + jumpsound);
			if(!inverted) {
           		GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 20f, GetComponent<Rigidbody>().velocity.z);
			} else {
				GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, -20f, GetComponent<Rigidbody>().velocity.z);
			}
			transform.Rotate(Vector3.zero);
        }
    }
    public IEnumerator FadeIn(GameObject platform, float duration)
    {
        Material mat = platform.GetComponentInChildren<SkinnedMeshRenderer>().material;
        platform.tag = "Untagged";
        if (platform.name == "End")
        {
            while (mat.color.a < 0.3f)
            {
                Color newColor = mat.color;
                newColor.a += Time.deltaTime / duration;
                platform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = newColor;
                yield return null;
            }
        }
        else if (platform.name == "Checkpoint" || platform.name == "Inverter")
        {
            while (mat.color.a < 0.5f)
            {
                Color newColor = mat.color;
                newColor.a += Time.deltaTime / duration;
                platform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = newColor;
                yield return null;
            }
        }
        else if (platform.name == "Gate" || platform.name == "Button" || platform.name == "Portal")
        {
            while (mat.color.a < 0.76f)
            {
                Color newColor = mat.color;
                newColor.a += Time.deltaTime / duration;
                platform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = newColor;
                yield return null;
            }
        }
		else if (platform.name == "IceCube" || platform.name == "IceSpike")
        {
            while (mat.color.a < 0.4f)
            {
                Color newColor = mat.color;
                newColor.a += Time.deltaTime / duration;
                platform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = newColor;
                yield return null;
            }
        }
        else
        {
            while (mat.color.a < 1f)
            {
                Color newColor = mat.color;
                newColor.a += Time.deltaTime / duration;
                platform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = newColor;
                yield return null;
            }
        }
    }

    public void die()
    {

        canvas.GetComponent<GameManager>().die();
    }

    public void explode()
    {
        int i = 0;
        while (i < 7)
        {
            i++;
            GameObject newParticle = (GameObject)Instantiate(explosionParticle, transform.position, Quaternion.identity);
        }
    }

	void OnCollisionStay(Collision coll) {
		timePassed += Time.deltaTime;
		if(coll.gameObject.name == "Cube" || coll.gameObject.name == "Clone(Clone)" && timePassed > 0.6f && coll.gameObject.transform.position.y < transform.position.y - 0.45f) {
			timePassed = 0f;
			isTouchingObject = true;
		}
	}

	void OnCollisionExit(Collision coll) {
		if(coll.gameObject.name == "Cube" || coll.gameObject.name == "Clone(Clone)") {
			timePassed = 0f;
			isTouchingObject = false;
		}
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.name == "Snowball(Clone)" || coll.name == "Snowman" || coll.name == "Pylon")
        {
            canvas.GetComponent<GameManager>().die();
        }
		if (coll.name == "Head")
		{
			coll.name = "Dead";
			if(coll.transform.parent.name == "Snowman")
			{
				source.PlayOneShot(killSnowman, 1f);
			} else if(coll.transform.parent.name == "Pylon")
			{
				source.PlayOneShot(killPlylon, 1f);
			}
		}
    }
}
