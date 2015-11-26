using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Home : MonoBehaviour {
	public Text text;

	public RectTransform panel;
	public Button[] bttn;
	public RectTransform center;
	public Button selectedButton;
	public GameObject shopPanel;
	private float[] distance;
	private bool dragging = false;
	private int bttnDistance;
	private int minButtonNum;

	public GameObject selectButton;
	void Start() {
		PlayerPrefs.DeleteAll();
		text.text = PlayerPrefs.GetInt ("coins").ToString();
		int bttnLength = bttn.Length;
		distance = new float[bttnLength];
		bttnDistance = (int)Mathf.Abs (bttn[1].GetComponent<RectTransform>().anchoredPosition.x - bttn[0].GetComponent<RectTransform>().anchoredPosition.x);
		PlayerPrefs.SetString ("PinkGuy", "true");
		foreach(Button button in bttn) {
			string[] splitString = button.name.Split('-');
			string itemName = splitString[0]; //name of item
			//Debug.Log (splitString[1]); //price
			if(PlayerPrefs.GetString (itemName) == "true") {
				button.GetComponent<Image>().color = Color.white;
			} else {
				button.GetComponent<Image>().color = new Color(0.1f, 0.1f, 0.1f);
			}
		}
	}
	void Update() {
		for(int i = 0; i < bttn.Length; i++) {
			distance[i] = Mathf.Abs(center.transform.position.x - bttn[i].transform.position.x);
		}
		float minDistance = Mathf.Min (distance);
		for(int a = 0; a < bttn.Length; a++) {
			if(minDistance == distance[a]) {
				selectedButton = bttn[a];
				minButtonNum = a;
			}
		}
		if(!dragging) {
			LerpToBttn(minButtonNum * -bttnDistance);
		}

		if(shopPanel.activeSelf == true) {
		string[] splitString = selectedButton.name.Split('-');
		if(PlayerPrefs.GetString (splitString[0]) == "true") {
			selectButton.GetComponentInChildren<Text>().text = "Select";
		} else {
			selectButton.GetComponentInChildren<Text>().text = "Buy";
		}
		}
	}

	public void BuyOrSelect() {
		string[] splitString = selectedButton.name.Split('-');
		if(PlayerPrefs.GetString (splitString[0]) == "true") { //select
			PlayerPrefs.SetString ("selected" + splitString[0], "true");
			shopPanel.SetActive(false);
		} else { //buy
			int coins = PlayerPrefs.GetInt ("coins");
			if(splitString[1] == "Free") {
				splitString[1] = "0";
			}
			coins = coins - int.Parse (splitString[1]);
			if(coins >= 0) {
				PlayerPrefs.SetString (splitString[0], "true");
				PlayerPrefs.SetString ("selected" + splitString[0], "true");
				selectedButton.GetComponent<Image>().color = Color.white;
				PlayerPrefs.SetInt ("coins", coins);
				text.text = PlayerPrefs.GetInt ("coins").ToString();
				shopPanel.SetActive(false);
			}
		}
	}

	public void openShop() {
		shopPanel.SetActive (true);
	}

	void LerpToBttn(int position) {
		float newX = Mathf.Lerp (panel.anchoredPosition.x, position, Time.deltaTime * 10f);
		Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);
		panel.anchoredPosition = newPosition;
	}

	public void StartDrag() {
		dragging = true;
	}

	public void EndDrag() {
		dragging = false;
	}

	public void LevelSelect() {
		Application.LoadLevel("LevelSelector");
	}

}
