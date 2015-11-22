using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class LevelSelector : MonoBehaviour {
	public GameObject[] buttons;
	public GameObject ratingContainer;
	public GameObject[] stars;
	public Sprite threeStar;
	public Sprite twoStar;
	public Sprite oneStar;
	public Sprite noStar;
	public Sprite unknown;
	// Use this for initialization
	void Start () {
		buttons = GameObject.FindGameObjectsWithTag("Button");
		Debug.Log (PlayerPrefs.GetInt ("nextLevel").ToString());
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
			} else if(PlayerPrefs.GetInt ("nextLevel").ToString() == button.GetComponentInChildren<Text>().text) {
				button.GetComponent<Image>().sprite = noStar;
				button.GetComponent<Button>().onClick.AddListener(() => { loadLevel ();});
			} else {
				button.GetComponent<Image>().sprite = unknown;
				Destroy(button.transform.GetChild(0).gameObject);
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
			PlayerPrefs.SetInt("PassedLevelStars", 0);
		}
	}

	public void Next() {
		Application.LoadLevel(PlayerPrefs.GetInt ("nextLevel"));
	}

	public void LevelSelect() {
		Destroy (ratingContainer);
	}

	public void Restart() {
		Application.LoadLevel (PlayerPrefs.GetInt ("nextLevel") - 1);
	}

	public void loadLevel() {
		Application.LoadLevel (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text);
	}
}
