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
    private AudioSource songsource;
    private AudioSource FXSource;

    public AudioClip UITick;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
        AudioSource[] sources = GetComponents<AudioSource>();
        songsource = sources[0];
        FXSource = sources[1];
	}
    public void UIClick()
    {
        FXSource.clip = UITick;
        FXSource.PlayOneShot(UITick);
    }
	void OnLevelWasLoaded() {
		Debug.Log("." + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + ".");
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "0") {
			if(PlayerPrefs.GetString("track") != "tutorialSource") {
                songsource.clip = tutorialSource;
				songsource.Play();
				PlayerPrefs.SetString("track", "tutorialSource");
			}
		} else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Home") {
			Debug.Log(PlayerPrefs.GetString("track"));
				songsource.clip = homeSource;
				songsource.Play();
				PlayerPrefs.SetString("track", "homeSource");
		} else if(int.Parse(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name) <= 8) {
			if(PlayerPrefs.GetString("track") != "nightSkySource") {
				songsource.clip = nightSkySource;
				songsource.Play();
				PlayerPrefs.SetString("track", "nightSkySource");
			}
		} else if(int.Parse(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name) <= 16) {
			Debug.Log(PlayerPrefs.GetString("track"));
			if(PlayerPrefs.GetString("track") != "snowSource") {
				songsource.clip = snowSource;
				songsource.Play();
				PlayerPrefs.SetString("track", "snowSource");
			}
		} else if(int.Parse(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name) <= 24) {
			Debug.Log(PlayerPrefs.GetString("track"));
			if(PlayerPrefs.GetString("track") != "iceSource") {
				songsource.clip = iceSource;
				songsource.Play();
				PlayerPrefs.SetString("track", "iceSource");
			}
		} else if(int.Parse(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name) <= 32) {
			Debug.Log(PlayerPrefs.GetString("track"));
			if(PlayerPrefs.GetString("track") != "spaceSource") {
				songsource.clip = spaceSource;
				songsource.Play();
				PlayerPrefs.SetString("track", "spaceSource");
			}
		} else if(int.Parse(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name) <= 40) {
			Debug.Log(PlayerPrefs.GetString("track"));
			if(PlayerPrefs.GetString("track") != "candySource") {
				songsource.clip = candySource;
				songsource.Play();
				PlayerPrefs.SetString("track", "candySource");
			}
		}
	}
}
