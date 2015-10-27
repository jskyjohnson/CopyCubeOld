using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {
	public Vector3 bottomLeftSpawnPos;
	public Vector3 topRightSpawnPos;
	public int starCount;
	public Camera camera;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Rigidbody>().useGravity = false;
		if(name == ("Star")) {
			while (starCount > 0) {
				Instantiate (gameObject, new Vector3(Random.Range (bottomLeftSpawnPos.x, topRightSpawnPos.x), Random.Range (bottomLeftSpawnPos.y, topRightSpawnPos.y), Random.Range (bottomLeftSpawnPos.z, topRightSpawnPos.z)), camera.transform.rotation);
				starCount --;
			}
		} else {
			float randomNum = Random.Range (0.3f, 0.9f);
			transform.localScale = new Vector3(randomNum, randomNum, randomNum);
			StartCoroutine(moveAround());
		}
	}

	IEnumerator moveAround() {
		while(true) {
		float zforce;
		if(transform.position.z < 5f) {
			zforce = Random.Range (0f, 0.2f);
		} else {
			zforce = Random.Range (-0.2f, 0.2f);
		}
		gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range (-0.2f, 0.2f), zforce);
		yield return new WaitForSeconds(Random.Range (1f, 3f));
		}
	}
}
