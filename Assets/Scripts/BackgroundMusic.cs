using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {
	public AudioClip homeSource;
	public AudioClip tutorialSource;
	public AudioClip nightSkySource;
	public AudioClip snowSource;
	public AudioClip iceSource;
	public AudioClip spaceSource;
	public AudioClip candySource;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}

	void OnLevelWasLoaded() {
		Debug.Log("." + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + ".");
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "0") {
			if(PlayerPrefs.GetString("track") != "tutorialSource") {
				GetComponent<AudioSource>().clip = tutorialSource;
				GetComponent<AudioSource>().Play();
				PlayerPrefs.SetString("track", "tutorialSource");
			}
		} else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Home") {
			Debug.Log(PlayerPrefs.GetString("track"));
				GetComponent<AudioSource>().clip = homeSource;
				GetComponent<AudioSource>().Play();
				PlayerPrefs.SetString("track", "homeSource");
		} else if(int.Parse(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name) <= 8) {
			if(PlayerPrefs.GetString("track") != "nightSkySource") {
				GetComponent<AudioSource>().clip = nightSkySource;
				GetComponent<AudioSource>().Play();
				PlayerPrefs.SetString("track", "nightSkySource");
			}
		} else if(int.Parse(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name) <= 16) {
			Debug.Log(PlayerPrefs.GetString("track"));
			if(PlayerPrefs.GetString("track") != "snowSource") {
				GetComponent<AudioSource>().clip = snowSource;
				GetComponent<AudioSource>().Play();
				PlayerPrefs.SetString("track", "snowSource");
			}
		} else if(int.Parse(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name) <= 24) {
			Debug.Log(PlayerPrefs.GetString("track"));
			if(PlayerPrefs.GetString("track") != "iceSource") {
				GetComponent<AudioSource>().clip = iceSource;
				GetComponent<AudioSource>().Play();
				PlayerPrefs.SetString("track", "iceSource");
			}
		} else if(int.Parse(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name) <= 32) {
			Debug.Log(PlayerPrefs.GetString("track"));
			if(PlayerPrefs.GetString("track") != "spaceSource") {
				GetComponent<AudioSource>().clip = spaceSource;
				GetComponent<AudioSource>().Play();
				PlayerPrefs.SetString("track", "spaceSource");
			}
		} else if(int.Parse(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name) <= 40) {
			Debug.Log(PlayerPrefs.GetString("track"));
			if(PlayerPrefs.GetString("track") != "candySource") {
				GetComponent<AudioSource>().clip = candySource;
				GetComponent<AudioSource>().Play();
				PlayerPrefs.SetString("track", "candySource");
			}
		}
	}
}
