using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {
	public GameObject player;
	public GameObject clone;
	public static bool started;
	public GameObject startPanel;
	public GameObject startButton;
	public GameObject playerFilter;
	public GameObject clonesCount;
	public bool paused;
	public GameObject pausePanel;
	public GameObject buttonContainer;
	// Use this for initialization
	void Start () {
		playerFilter = GameObject.Find ("playerFilter");
		paused = false;
		started = false;
		player.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void pauseGame() {
		if(paused == false) {
			player.GetComponent<Rigidbody>().isKinematic = true;
			paused = true;
			StartCoroutine(pauseButtonsScrollUp());
		} else if (paused == true) {
			StopCoroutine(pauseButtonsScrollUp());
			buttonContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -546f);
			pausePanel.SetActive (false);
			player.GetComponent<Rigidbody>().isKinematic = false;
			paused = false;
		}
	}

	IEnumerator pauseButtonsScrollUp() {
		pausePanel.SetActive (true);
		while(buttonContainer.GetComponent<RectTransform>().anchoredPosition.y <= -0.1f) {
			Vector2 newPos = Vector2.Lerp (buttonContainer.GetComponent<RectTransform>().anchoredPosition, new Vector2(0f, 0f), 0.2f);
			buttonContainer.GetComponent<RectTransform>().anchoredPosition = newPos;
			yield return 0;
		}
	}

	public void loadMenu() {
		Application.LoadLevel ("LevelSelector");
	}

	public void reload() {
		Application.LoadLevel (Application.loadedLevelName);
		GameManager.started = false;
	}

	public void respawn() {
		Vector3 newPos = new Vector3(Mathf.Round (player.transform.position.x), Mathf.Round (player.transform.position.y), Mathf.Round (player.transform.position.z));
		if(newPos != Player.respawnLocation) {
			player.GetComponent<Player>().explode();
			GameObject newClone = (GameObject)Instantiate (clone, newPos, Quaternion.identity);
		}
		player.transform.position = Player.respawnLocation;
		Player.direction = Player.respawnDirection;
		player.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
		player.GetComponent<Player>().dontMove = false;
		player.GetComponent<Collider>().material = player.GetComponent<Player>().sticky;
		player.transform.rotation = Quaternion.Euler(Vector3.zero);
		if(Player.direction == "+x" || Player.direction == "-x") {
			player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
		} else if(Player.direction == "+z" || Player.direction == "-z") {
			player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		}
		addOneToCloneCount();
	}

	public void die() {
		player.GetComponent<Player>().explode();
		player.transform.position = Player.respawnLocation;
		Player.direction = Player.respawnDirection;
		player.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
		player.GetComponent<Player>().dontMove = false;
		player.GetComponent<Collider>().material = player.GetComponent<Player>().sticky;
		player.transform.rotation = Quaternion.Euler(Vector3.zero);
		addOneToCloneCount();
	}

	public void start() {
		player.SetActive(true);
		started = true;
		Destroy (GameObject.Find ("playerFiller"));
		Destroy (startPanel);
		Destroy (startButton);
	}

	public void clearClones() {
		Debug.Log ("whole function called");
		foreach(GameObject clone in GameObject.FindGameObjectsWithTag("clone")) {
			Destroy (clone);
			Debug.Log ("being called");
		}

		foreach(GameObject button in GameObject.FindGameObjectsWithTag("Platform")) {
			if(button.name == "Button") {
				button.GetComponent<GameButton>().unpresss();
			}
		}
		player.GetComponent<Player>().GetComponent<Player>().dontMove = false;
		player.GetComponent<Collider>().material = player.GetComponent<Player>().sticky;
	}

	public void addOneToCloneCount() {
		clonesCount.GetComponent<Text>().text = (int.Parse(clonesCount.GetComponent<Text>().text) + 1).ToString();
	}
}
