using UnityEngine;
using System.Collections;
//using Soomla;
//using Soomla.Store;

public class Initializer : MonoBehaviour {
	void Start () {
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
