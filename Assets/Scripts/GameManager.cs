using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {
	public GameObject player;
	public GameObject clone;
	public static bool started;
	public GameObject startPanel;
	public GameObject startButton;
	// Use this for initialization
	void Awake() {
		Application.targetFrameRate = 60;
	}
	void Start () {
		started = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void respawn() {
		player.GetComponent<Player>().explode();
		Vector3 newPos = new Vector3(Mathf.Round (player.transform.position.x), Mathf.Round (player.transform.position.y), Mathf.Round (player.transform.position.z));
		if(newPos != Player.respawnLocation) {
		Instantiate (clone, newPos, Quaternion.identity);
		}
		player.transform.position = Player.respawnLocation;
		Player.direction = Player.respawnDirection;
		player.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		player.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
		player.GetComponent<Player>().dontMove = false;
		player.GetComponent<Collider>().material = player.GetComponent<Player>().sticky;
	}

	public void die() {
		player.GetComponent<Player>().explode();
		player.transform.position = Player.respawnLocation;
		Player.direction = Player.respawnDirection;
		player.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		player.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
		player.GetComponent<Player>().dontMove = false;
		player.GetComponent<Collider>().material = player.GetComponent<Player>().sticky;
	}

	public void start() {
		started = true;
		Destroy (startPanel);
		Destroy (startButton);
	}

	public void clearClones() {
		foreach(GameObject clone in GameObject.FindGameObjectsWithTag("clone")) {
			Destroy (clone);
		}
		player.GetComponent<Player>().GetComponent<Player>().dontMove = false;
	}
}
