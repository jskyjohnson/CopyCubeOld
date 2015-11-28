using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
	public float rotSpeed;
	public bool frozen;
	private float currentAngle;
	private float distance;
	void Start () {
		rotSpeed = 30.0f;
		distance = Vector3.Distance(transform.position, target.position);
	}

	// Update is called once per frame
	void Update () 
	{
		if (target && !frozen)
		{
			//handle movement of camera;
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
			Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	}

	public void rotateTo(float degrees) {
		StartCoroutine(rotateCoroutine(degrees));
	}

	IEnumerator rotateCoroutine(float degrees) {
		// if degrees is 120 and eulerangle is 30, move 90, dont move 270 //////45 0 
		if((360f - degrees) + transform.rotation.eulerAngles.y < degrees - transform.rotation.eulerAngles.y) {
			if(degrees < 357f) {
				while(transform.rotation.eulerAngles.y <= degrees) {
					Debug.Log ("rotating");
					transform.RotateAround (target.position, Vector3.up, rotSpeed * Time.deltaTime);
					var desiredPosition = (transform.position - target.position).normalized * distance + target.position;
					transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * 0.5f);
					yield return 0;
				}
			} else {
				while(transform.rotation.eulerAngles.y <= degrees - 3f) {
					Debug.Log ("rotating");
					transform.RotateAround (target.position, Vector3.up, rotSpeed * Time.deltaTime);
					var desiredPosition = (transform.position - target.position).normalized * distance + target.position;
					transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * 0.5f);
					yield return 0;
				}
			}
		} else {
			if(degrees > 3f) {
				while(transform.rotation.eulerAngles.y >= degrees) {
					Debug.Log ("rotating");
					transform.RotateAround (target.position, Vector3.up, -rotSpeed * Time.deltaTime);
					var desiredPosition = (transform.position - target.position).normalized * distance + target.position;
					transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * 0.5f);
					yield return 0;
				}
			} else {
				while(transform.rotation.eulerAngles.y >= degrees + 3f) {
					Debug.Log ("rotating");
					transform.RotateAround (target.position, Vector3.up, -rotSpeed * Time.deltaTime);
					var desiredPosition = (transform.position - target.position).normalized * distance + target.position;
					transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * 0.5f);
					yield return 0;
				}
			}
		}
	}
}