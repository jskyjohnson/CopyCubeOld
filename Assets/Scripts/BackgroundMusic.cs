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
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "0") {
			if(PlayerPrefs.GetString("track") != "tutorialSource") {
				GetComponent<AudioSource>().clip = tutorialSource;
				GetComponent<AudioSource>().Play();
				PlayerPrefs.SetString("track", "tutorialSource");
			}
		} else if(int.Parse(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name) <= 8) {
			if(PlayerPrefs.GetString("track") != "nightSkySource") {
				GetComponent<AudioSource>().clip = nightSkySource;
				GetComponent<AudioSource>().Play();
				PlayerPrefs.SetString("track", "nightSkySource");
			}
		}
	}
}
