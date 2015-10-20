using UnityEngine;
using System.Collections;

public class FadeInOut : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(wait ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator wait() {
		yield return new WaitForSeconds(Random.Range (1f, 8f));
		StartCoroutine(FadeIn(gameObject, Random.Range (3f, 8f)));
	}

	IEnumerator FadeIn (GameObject item, float duration)
	{
		Material mat = item.GetComponent<MeshRenderer>().material;
		while(mat.color.a < 0.4f)
			{
				Color newColor = mat.color;
				newColor.a += Time.deltaTime / duration;
				item.GetComponent<MeshRenderer>().material.color = newColor;
				yield return null;
			}
		StartCoroutine(FadeOut (gameObject, Random.Range (3f, 8f)));
	}
	
	IEnumerator FadeOut (GameObject item, float duration)
	{
		Material mat = item.GetComponent<MeshRenderer>().material;
		while(mat.color.a > 0f)
		{
			Color newColor = mat.color;
			newColor.a -= Time.deltaTime / duration;
			item.GetComponent<MeshRenderer>().material.color = newColor;
			yield return null;
		}
		StartCoroutine(wait ());
	}
}
