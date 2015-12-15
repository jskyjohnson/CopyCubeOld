using UnityEngine;
using System.Collections;
//using Soomla;
//using Soomla.Store;

public class Initializer : MonoBehaviour {
	void Start () {

		PlayerPrefs.SetInt ("coins", 2000);
		PlayerPrefs.SetString ("PinkGuy", "true");
		if(PlayerPrefs.GetString("selected") == "") {
			PlayerPrefs.SetString ("selected", "PinkGuy");
		}
		if(PlayerPrefs.GetString("firstTime") != "false") {
			PlayerPrefs.SetFloat("musicLevel", 100f);
			PlayerPrefs.SetFloat("soundEffect", 100f);
		}
		PlayerPrefs.SetString("firstTime", "false");
		Application.targetFrameRate = 60;
		if (this.gameObject.name == "Initializer") {
		//	SoomlaStore.Initialize (new AppAssets ());
		//	StoreEvents.OnItemPurchased += ShopScript.onItemPurchased;
		} else if (this.gameObject.name != "Initializer") {
			DontDestroyOnLoad (this.gameObject);
		}
	}
	void Update() {
		StartCoroutine(loadStart());
	}
	IEnumerator loadStart() {
		yield return new WaitForSeconds(2.0f);
		Application.LoadLevel ("Home");
	}
}
