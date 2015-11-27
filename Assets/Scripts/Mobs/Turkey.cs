using UnityEngine;
using System.Collections;

public class Turkey : MonoBehaviour {
	public GameObject snowball;
	public GameObject gun;
	public GameObject leg1;
	public GameObject leg2;
	Vector3 gunTip;
	public string direction;
	public bool moving;
	public float moveSpeed;
	float currentSpeed;
	bool shooting;
	float bulletSpeed;
	bool loading;
	private Vector3 leg1LocalPos;
	private Vector3 leg2LocalPos;
	Vector3 targetLeg1;
	Vector3 targetLeg2;

	// Use this for initialization
	void Start () {
		leg1LocalPos = leg1.gameObject.transform.localPosition;
		leg2LocalPos = leg2.gameObject.transform.localPosition;
		bulletSpeed = 7f;
		moveSpeed = 0.5f;
		gunTip = new Vector3(gun.transform.position.x, gun.transform.position.y, gun.transform.position.z);
		StartCoroutine(hoverAnim());
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
	}

	IEnumerator switchLegDirection() {
		while(direction == "-x" || direction == "+x") {
			if(moving && shooting == false) {
				targetLeg1.z = leg1LocalPos.z + 0.3f;
				targetLeg2.z = leg2LocalPos.z - 0.3f;
				yield return new WaitForSeconds(0.4f);
				targetLeg1.z = leg1LocalPos.z - 0.3f;
				targetLeg2.z = leg2LocalPos.z + 0.3f;
				yield return new WaitForSeconds(0.4f);
			}
		}
		while(direction == "-z" || direction == "+z") {
			if(moving && shooting == false) {
				targetLeg1.x = leg1LocalPos.x + 0.3f;
				targetLeg2.x = leg2LocalPos.x - 0.3f;
				yield return new WaitForSeconds(0.4f);
				targetLeg1.x = leg1LocalPos.x - 0.3f;
				targetLeg2.x = leg2LocalPos.x + 0.3f;
				yield return new WaitForSeconds(0.4f);
			}
		}
	}

	IEnumerator hoverAnim() {
		if(moving && shooting == false) {
			if(direction == "+x" || direction == "-x") {
				targetLeg1 = new Vector3(leg1LocalPos.x, leg1LocalPos.y, leg1LocalPos.z - 0.3f);
				targetLeg2 = new Vector3(leg2LocalPos.x, leg2LocalPos.y, leg2LocalPos.z + 0.3f);
				StartCoroutine(switchLegDirection());
				while(direction == "-x" || direction == "+x") {
					if(moving && shooting == false) {
						leg1.transform.localPosition = Vector3.Lerp (leg1.transform.localPosition, targetLeg1, 6f * Time.deltaTime);
						leg2.transform.localPosition = Vector3.Lerp (leg2.transform.localPosition, targetLeg2, 6f * Time.deltaTime);
						yield return 0;
					}
				}
			} else {
				Vector3 targetLeg1 = new Vector3(leg1LocalPos.x + 0.6f, leg1LocalPos.y, leg1LocalPos.z);
				Vector3 targetLeg2 = new Vector3(leg2LocalPos.x - 0.6f, leg2LocalPos.y, leg2LocalPos.z);
				StartCoroutine(switchLegDirection());
				while(direction == "+z" || direction == "-z") {
					if(moving && shooting == false) {
						leg1.transform.localPosition = Vector3.Lerp (leg1.transform.localPosition, targetLeg1, 6f * Time.deltaTime);
						leg2.transform.localPosition = Vector3.Lerp (leg2.transform.localPosition, targetLeg2, 6f * Time.deltaTime);
						leg2.transform.localPosition = leg2LocalPos;
						yield return 0;
					}
				}
			}
		}
	}
	
	IEnumerator ShootCoroutine() {
		while(true) {
			if(shooting && !GameObject.Find ("Canvas").GetComponent<GameManager>().paused) {
				Shoot ();
				loading = true;
				yield return new WaitForSeconds(2.5f);
				loading = false;
			}
			yield return 0;
		}
	}
	
	void Shoot() {
		GameObject bullet = (GameObject)Instantiate (snowball, snowball.transform.position, Quaternion.identity);
		bullet.GetComponent<Snowball>().direction = direction;
		bullet.GetComponent<Snowball>().bulletSpeed = bulletSpeed;
	}
}
