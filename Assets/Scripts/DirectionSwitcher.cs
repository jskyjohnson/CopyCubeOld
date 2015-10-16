using UnityEngine;
using System.Collections;

public class DirectionSwitcher : MonoBehaviour {
	public string direction;
	void OnTriggerEnter(Collider coll) {
		if(coll.name == "Player") {
		if(direction == "+x") {
			Player.direction = "+x";
		} else if(direction == "+z") {
			Player.direction = "+z";
		} else if(direction == "-x") {
			Player.direction = "-x";
		} else if (direction == "-z") {
			Player.direction = "-z";
		}
		coll.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		}
	}
}
