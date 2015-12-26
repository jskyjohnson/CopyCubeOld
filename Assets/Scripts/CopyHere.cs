using UnityEngine;
using System.Collections;

public class CopyHere : MonoBehaviour {
	public GameObject[] indicators;
	public int number;
	public static int currentNumber = 0;
	private static bool can;
	void Start() {
		currentNumber = 0;
		if(number != 0) {
			gameObject.SetActive(false);	
		}
		can = true;
	}
	void OnTriggerEnter(Collider coll) {
		Debug.Log("currentNumber: " + currentNumber);
		Debug.Log("can: " + can);
		if(coll.name == "Player" && number == currentNumber && can) {
			Debug.Log("starting coroutine");
			can = false;
			StartCoroutine(refresh());
		}
	}

	IEnumerator refresh() {
		currentNumber ++;
		foreach(GameObject indicator in indicators) {
			if(indicator.GetComponent<CopyHere>().number == currentNumber) {
				indicator.SetActive(true);
			}
		}
		if(number == currentNumber - 1) {
			Destroy(gameObject.GetComponent<SkinnedMeshRenderer>());
			Destroy(gameObject.GetComponent<BoxCollider>());
			Destroy(transform.GetChild(0).gameObject);
		}
		yield return new WaitForSeconds(1f);
		gameObject.SetActive(false);	
		Debug.Log("setting can to true");
		can = true;
	}
}
