using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TextTrigger : MonoBehaviour {
	public GameObject text;
	public string textValue;
	// Use this for initialization
	void OnTriggerEnter(Collider coll) {
		if(coll.name == "Player") {
 			text.GetComponent<Text>().text = textValue;
			StartCoroutine(textCycle());
		}
	}
	
	IEnumerator textCycle() {
		text.SetActive(true);
		Color Scolor = text.GetComponent<Text>().color;
		Scolor.a = 0f;
		text.GetComponent<Text>().color = Scolor;
		while(text.GetComponent<Text>().color.a < 1.0f) {
			Color color = text.GetComponent<Text>().color;
			color.a = Mathf.MoveTowards(color.a, 1f, 0.08f);
			text.GetComponent<Text>().color = color;
			yield return new WaitForSeconds(0.008f);
		}
		Destroy (gameObject);
	}
}
