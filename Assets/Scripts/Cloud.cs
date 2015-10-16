using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {
	public Vector3 spawnRange;
	public float variationZ;
	public float variationY;
	public float variationX;
	public float timeInterval;

	private bool left = true;
	void Start () {
		gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(2f, 0.5f), 0f, 0f);
		//transform.localScale = new Vector3(0.25f, Random.Range (1.5f, 2f), Random.Range(0.5f, 1f));
		if(name == "Cloud") {
			StartCoroutine(createClouds());
		}
	}

	IEnumerator createClouds() {
		while(true) {
			if(left) {
				Instantiate (gameObject, new Vector3(Random.Range(spawnRange.x + variationX, spawnRange.x - variationX), Random.Range(spawnRange.y + variationY, spawnRange.y - variationY), Random.Range(spawnRange.z + variationZ, 0f)), transform.rotation);
				left = false;
			} else {
				Instantiate (gameObject, new Vector3(Random.Range(spawnRange.x + variationX, spawnRange.x - variationX), Random.Range(spawnRange.y + variationY, spawnRange.y - variationY), Random.Range(0f, spawnRange.z - variationZ)), transform.rotation);
				left = true;
			}
			yield return new WaitForSeconds(timeInterval);
		}
	}
}
