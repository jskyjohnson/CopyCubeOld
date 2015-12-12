using UnityEngine;
using System.Collections;

public class ChangeDirectionButton : MonoBehaviour {
	public GameObject[] posXGameObjects;
	public GameObject[] negXGameObjects;
	public GameObject[] posZGameObjects;
	public GameObject[] negZGameObjects;
	public string state;
	public GameObject directionSwitcher;
	public Vector3 initialPosition;
	public bool canActivate;
	void Start() {
		canActivate = true;
		initialPosition = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
		transform.position = initialPosition;
		changePosition();
		directionSwitcher.GetComponent<DirectionSwitcher>().direction = state;
	}

	void OnTriggerEnter(Collider coll) {
		if(coll.name == "Player" && canActivate) {
			canActivate = false;
			StartCoroutine(delayStateChange());
			GameObject.Find("Canvas").GetComponent<GameManager>().resetPlayer();
		}
	}

	IEnumerator delayStateChange() {
		changeState();
		directionSwitcher.GetComponent<DirectionSwitcher>().direction = state;
		yield return new WaitForSeconds(0.2f);
		canActivate = true;
	}

	public void changePosition() {
		if(state == "+x") {
			transform.position = new Vector3(initialPosition.x + 0.3f, initialPosition.y, initialPosition.z);
			transform.rotation = Quaternion.Euler(new Vector3(270f, 0f, 0f));
		} else if(state == "+z") {
			transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z + 0.3f);
			transform.rotation = Quaternion.Euler(new Vector3(270f, 90f, 0f));
		} else if(state == "-x") {
			transform.position = new Vector3(initialPosition.x - 0.3f, initialPosition.y, initialPosition.z);
			transform.rotation = Quaternion.Euler(new Vector3(270f, 0f, 0f));
		} else if(state == "-z") {
			transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z - 0.3f);
			transform.rotation = Quaternion.Euler(new Vector3(270f, 90f, 0f));
		}
	}

	public void changeState() {
		if(state == "+x") {
			state = "-z";
			changePosition();
			if(negZGameObjects.Length == 0) {
				changeState();
			}
		} else if(state == "+z") {
			state = "+x";
			changePosition();
			if(posXGameObjects.Length == 0) {
				changeState();
			}
		} else if(state == "-x") {
			state = "+z";
			changePosition();
			if(posZGameObjects.Length == 0) {
				changeState();
			}
		} else if(state == "-z") {
			state = "-x";
			changePosition();
			if(negXGameObjects.Length == 0) {
				changeState();
			}
		}
		Debug.Log (state + " is the new state.");
	}
}
