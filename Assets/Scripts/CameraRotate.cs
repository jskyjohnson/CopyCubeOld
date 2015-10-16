using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {
	public float angle;
	public GameObject camera;
	void OnTriggerEnter(Collider coll) {
		camera.GetComponent<CameraController>().rotateTo(0f);
	}
}
