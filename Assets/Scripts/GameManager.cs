using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ChartboostSDK;
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

	public GameObject resumeButton;
	public GameObject restartButton;
	public GameObject menuButton;
	public GameObject shareButton;
	private float timePlayed;
	private float adtimePlayed;
	private bool playerCanDie;
	// Use this for initialization
	void Start () {
		Chartboost.cacheInterstitial (CBLocation.Default);
		showAdOnRightCondition();
		playerFilter = GameObject.Find ("playerFilter");
		paused = false;
		started = false;
		player.SetActive(false);
		timePlayed = 0f;
		playerCanDie = true;
		pausePanel.SetActive(true);
		resumeButton = GameObject.Find("Resume");
		restartButton = GameObject.Find("Restart");
		menuButton = GameObject.Find("Menu");
		shareButton = GameObject.Find("Share");
		resumeButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, 75f, 0f);
		restartButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, 0f, 0f);
		menuButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, -75f, 0f);
		buttonContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -546f);
		Destroy(shareButton);
		pausePanel.SetActive(false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timePlayed += Time.deltaTime;
	}

	public void endGameTime() {
		PlayerPrefs.SetFloat ("timePlayed", PlayerPrefs.GetFloat ("timePlayed") + timePlayed);
		PlayerPrefs.SetFloat ("adTimePlayed", PlayerPrefs.GetFloat ("adTimePlayed") + timePlayed);
		timePlayed = 0f;
	}
	public static void showAdOnRightCondition() {
		Debug.Log("ad timed played: " + PlayerPrefs.GetFloat ("adTimePlayed"));
		if(PlayerPrefs.GetFloat ("adTimePlayed") > 90f) {
			ChartboostExample.runAd();
			PlayerPrefs.SetFloat ("adTimePlayed", 0f);
		}
	}

	public void pauseGame() {
		PlayerPrefs.SetFloat ("timePlayed", PlayerPrefs.GetFloat ("timePlayed") + timePlayed);
		PlayerPrefs.SetFloat ("adTimePlayed", PlayerPrefs.GetFloat ("adTimePlayed") + timePlayed);
		timePlayed = 0f;
		if(paused == false) {
			player.GetComponent<Rigidbody>().isKinematic = true;
			paused = true;
			showAdOnRightCondition();
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
		if(playerCanDie) {
			Vector3 newPos = new Vector3(Mathf.Round (player.transform.position.x), Mathf.Round (player.transform.position.y), Mathf.Round (player.transform.position.z));
			if(newPos != Player.respawnLocation) {
				player.GetComponent<Player>().explode();
				GameObject newClone = (GameObject)Instantiate (clone, newPos, Quaternion.identity);
			}
			player.transform.position = Player.respawnLocation;
			player.GetComponent<Player>().inverted = Player.respawnInverted;
			Player.direction = Player.respawnDirection;
			if(Player.respawnInverted) {
				Physics.gravity = new Vector3(0, 50f, 0);
			} else {
				Physics.gravity = new Vector3(0, -50f, 0);
			}
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
			PlayerPrefs.SetInt("timesRespawned", PlayerPrefs.GetInt("timesRespawned") + 1);
			StartCoroutine(resetDeath());
			StartCoroutine(freezeCamera(0.25f));
		}
	}

	public void resetPlayer() {
		if(playerCanDie) {
			Vector3 newPos = new Vector3(Mathf.Round (player.transform.position.x), Mathf.Round (player.transform.position.y), Mathf.Round (player.transform.position.z));
			player.transform.position = Player.respawnLocation;
			player.GetComponent<Player>().inverted = Player.respawnInverted;
			Player.direction = Player.respawnDirection;
			if(Player.respawnInverted) {
				Physics.gravity = new Vector3(0, 50f, 0);
			} else {
				Physics.gravity = new Vector3(0, -50f, 0);
			}
			player.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
			player.GetComponent<Player>().dontMove = false;
			player.GetComponent<Collider>().material = player.GetComponent<Player>().sticky;
			player.transform.rotation = Quaternion.Euler(Vector3.zero);
			if(Player.direction == "+x" || Player.direction == "-x") {
				player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
			} else if(Player.direction == "+z" || Player.direction == "-z") {
				player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			}
			StartCoroutine(resetDeath());
			StartCoroutine(freezeCamera(0.25f));
		}
	}

	public void die() {
		if(playerCanDie) {
			player.GetComponent<Player>().explode();
			player.GetComponent<Player>().inverted = Player.respawnInverted;
			if(Player.respawnInverted) {
				Physics.gravity = new Vector3(0, 50f, 0);
			} else {
				Physics.gravity = new Vector3(0, -50f, 0);
			}
			player.transform.position = Player.respawnLocation;
			Player.direction = Player.respawnDirection;
			player.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
			player.GetComponent<Player>().dontMove = false;
			player.GetComponent<Collider>().material = player.GetComponent<Player>().sticky;
			player.transform.rotation = Quaternion.Euler(Vector3.zero);
			addOneToCloneCount();
			PlayerPrefs.SetInt("timesDied", PlayerPrefs.GetInt("timesDied") + 1);
			StartCoroutine(freezeCamera(1f));
			StartCoroutine(resetDeath());
		}
	}

	IEnumerator resetDeath() {
		playerCanDie = false;
		yield return new WaitForSeconds(0.5f);
		playerCanDie = true;
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

	IEnumerator freezeCamera(float time) {
		Camera.main.GetComponent<CameraController>().frozen = true;
		player.GetComponent<Rigidbody>().isKinematic = true;
		yield return new WaitForSeconds(time);
		Camera.main.GetComponent<CameraController>().frozen = false;
		player.GetComponent<Rigidbody>().isKinematic = false;
	}
}
