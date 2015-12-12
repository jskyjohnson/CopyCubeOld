using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ChartboostSDK;
public class LevelSelector : MonoBehaviour {
	public GameObject[] buttons;
	public GameObject ratingContainer;
	public GameObject[] stars;
	public Sprite threeStar;
	public Sprite twoStar;
	public Sprite oneStar;
	public Sprite noStar;
	public Sprite unknown;
	public Text compliment;
	public GameObject scrollmap;
	public Text coinsAdded;
	// Use this for initialization
	void Start () {
		Chartboost.cacheInterstitial (CBLocation.Default);
		GameManager.showAdOnRightCondition();
		buttons = GameObject.FindGameObjectsWithTag("Button");
		foreach(GameObject button in buttons) {
			if(PlayerPrefs.GetInt("levelStar" + button.GetComponentInChildren<Text>().text) == 3) {
				button.GetComponent<Image>().sprite = threeStar;
				button.GetComponent<Button>().onClick.AddListener(() => { loadLevel ();});
			} else if (PlayerPrefs.GetInt("levelStar" + button.GetComponentInChildren<Text>().text) == 2) {
				button.GetComponent<Image>().sprite = twoStar;
				button.GetComponent<Button>().onClick.AddListener(() => { loadLevel ();});
			} else if (PlayerPrefs.GetInt("levelStar" + button.GetComponentInChildren<Text>().text) == 1) {
				button.GetComponent<Image>().sprite = oneStar;
				button.GetComponent<Button>().onClick.AddListener(() => { loadLevel ();});
			} else if(PlayerPrefs.GetInt ("permaNextLevel").ToString() == button.GetComponentInChildren<Text>().text) {
				button.GetComponent<Image>().sprite = noStar;
				button.GetComponent<Button>().onClick.AddListener(() => { loadLevel ();});
			} else {
				button.GetComponent<Image>().sprite = unknown;
				if(button.transform.GetChild(0).gameObject.GetComponent<Text>().text == "0") {
					button.GetComponent<Image>().sprite = noStar;
					button.GetComponent<Button>().onClick.AddListener(() => { loadLevel ();});
				} else {
				//button.GetComponent<Button>().onClick.AddListener(() => { loadLevel ();}); //debugging purposes
					Destroy(button.transform.GetChild(0).gameObject);
				}
			}
		}
		if(PlayerPrefs.GetInt("PassedLevelStars") == 0) {
			Destroy (ratingContainer);
		} else {
			int i = 3 - PlayerPrefs.GetInt("PassedLevelStars");
			foreach(GameObject star in stars) {
				if(i > 0) {
					Destroy (star);
					i--;
				} else {
					star.tag = "Stars";
				}
			}
			int b = 0;
			foreach(GameObject star in GameObject.FindGameObjectsWithTag("Stars")) {
				if (GameObject.FindGameObjectsWithTag("Stars").Length == 2) {
					if(b == 0) {
						star.GetComponent<RectTransform>().localPosition = new Vector3(-35f, -50f, 0f);
					} else if (b == 1) {
						star.GetComponent<RectTransform>().localPosition = new Vector3(35f, -50f, 0f);
					}
					b++;
				} else if (GameObject.FindGameObjectsWithTag("Stars").Length == 1) {
					star.GetComponent<RectTransform>().localPosition = new Vector3(0f, -50f, 0f);
				}
			}
			if(PlayerPrefs.GetInt("PassedLevelStars") == 3) {
				compliment.text = "SUPERSTAR!";
				coinsAdded.text = "+30";
				PlayerPrefs.SetInt ("coins", PlayerPrefs.GetInt ("coins") + 30);
				StartCoroutine(BlinkText());
			} else if (PlayerPrefs.GetInt("PassedLevelStars") == 2) {
				compliment.text = "Good Job!";
				coinsAdded.text = "+15";
				PlayerPrefs.SetInt ("coins", PlayerPrefs.GetInt ("coins") + 15);
			} else if (PlayerPrefs.GetInt("PassedLevelStars") == 1) {
				compliment.text = "Well Done";
				coinsAdded.text = "+10";
				PlayerPrefs.SetInt ("coins", PlayerPrefs.GetInt ("coins") + 10);
			}
			PlayerPrefs.SetInt("PassedLevelStars", 0);
		}
	}
	public void Home() {
		Application.LoadLevel ("Home");
	}

	public void Next() {
		Application.LoadLevel(PlayerPrefs.GetInt ("nextLevel").ToString());
	}

	public void LevelSelect() {
		Destroy (ratingContainer);
	}

	public void Restart() {
		Application.LoadLevel ((PlayerPrefs.GetInt ("nextLevel") - 1).ToString ());
	}

	public void loadLevel() {
		Application.LoadLevel (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text);
	}

	public IEnumerator BlinkText(){
		//blink it forever. You can set a terminating condition depending upon your requirement
		while(true){
			//set the Text's text to blank
			//display blank text for 0.5 seconds
			compliment.color = new Color(0.9f, 0.2f, 0.2f);
			yield return new WaitForSeconds(0.2f);
			//display “I AM FLASHING TEXT” for the next 0.5 seconds
			compliment.color = new Color (1f, 1f, 1f);
			yield return new WaitForSeconds(0.2f);
			compliment.color = new Color (0.9f, 0.9f, 0.2f);
			yield return new WaitForSeconds(0.2f);
		}
	}
}
