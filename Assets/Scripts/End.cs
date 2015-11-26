using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class End : MonoBehaviour {
	private Vector3 position;
	public string nextLevel;
	GameObject[] taggedGameObjects;
	float distance;
	bool flying;
	public int threeStarMax;
	public int twoStarMax;
	void Start() {
		position = transform.position;
		distance = 44f;
	}
	void OnTriggerEnter(Collider coll) {
		if(coll.gameObject.name == "Player") {
			coll.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
			GameManager.started = false;
			StartCoroutine(fly (coll.gameObject, nextLevel));
			StartCoroutine(fadeObjects());
			coll.gameObject.GetComponent<Rigidbody>().useGravity = false;
			coll.gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(10f, 0f, 0f));
		}
	}

	void Update() {
		if(flying) {
			distance++;
			Debug.Log("distance is increasing");
		}
	}

	IEnumerator fly(GameObject item, string nextLevel) {
		flying = true;
		taggedGameObjects = GameObject.FindGameObjectsWithTag("Platform"); 
		item.GetComponent<Rigidbody>().velocity = new Vector3(0f, 2f, 0f);
		yield return new WaitForSeconds(2f);
		item.GetComponent<Rigidbody>().velocity = new Vector3(0f, -1f, 0f);
		yield return new WaitForSeconds(0.5f);
		item.GetComponent<Rigidbody>().velocity = new Vector3(0f, 30f, 0f);
		yield return new WaitForSeconds(0.9f);
		Debug.Log ("setting next level to: " + (int.Parse(Application.loadedLevelName) + 1));
		PlayerPrefs.SetInt ("nextLevel", int.Parse(Application.loadedLevelName) + 1);
		if(PlayerPrefs.GetInt ("permaNextLevel") < PlayerPrefs.GetInt ("nextLevel")) {
			PlayerPrefs.SetInt ("permaNextLevel", PlayerPrefs.GetInt ("nextLevel"));
		}
		if(int.Parse(GameObject.Find ("Canvas").GetComponent<GameManager>().clonesCount.GetComponent<Text>().text) <= threeStarMax) {
			PlayerPrefs.SetInt("levelStar" + Application.loadedLevelName, 3);
			PlayerPrefs.SetInt("PassedLevelStars", 3);
		} else if (int.Parse(GameObject.Find ("Canvas").GetComponent<GameManager>().clonesCount.GetComponent<Text>().text) <= twoStarMax) {
			if(PlayerPrefs.GetInt("levelStar" + Application.loadedLevelName) < 2) {
				PlayerPrefs.SetInt("levelStar" + Application.loadedLevelName, 2);
			}
			PlayerPrefs.SetInt("PassedLevelStars", 2);
		} else {
			if(PlayerPrefs.GetInt("levelStar" + Application.loadedLevelName) < 1) {
				PlayerPrefs.SetInt("levelStar" + Application.loadedLevelName, 1);
			}
			PlayerPrefs.SetInt("PassedLevelStars", 1);
		}
		Application.LoadLevel ("LevelSelector");
	}

	IEnumerator fadeObjects() {
		while(true) {
			foreach (GameObject platform in taggedGameObjects) {
				Vector3 objectPos = platform.transform.position;
				float distanceSqr = (objectPos - transform.position).sqrMagnitude;
				if (distanceSqr < distance) {
					StartCoroutine(FadeIn (platform, 1f));
				}
			}
			yield return new WaitForSeconds(0.1f);
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
}
