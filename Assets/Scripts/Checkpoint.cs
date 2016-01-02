using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	void OnTriggerEnter(Collider coll) {
		if(coll.name == "Player") {
			Player.respawnDirection = Player.direction;
			Player.respawnLocation = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
			Player.respawnInverted = coll.gameObject.GetComponent<Player>().inverted;
			Destroy(GetComponent<Collider>());
			StartCoroutine(shrink());
		}
	}

	IEnumerator shrink() {
		while(transform.localScale.y > 0f) {
			transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - (Time.deltaTime * 14f), transform.localScale.z);
			yield return 0;
		}
		Destroy(GetComponent<SkinnedMeshRenderer>());
	}
}
