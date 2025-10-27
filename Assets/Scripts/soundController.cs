using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public enum soundFX {
	JUMP,
	SLIDE,
}

public class soundController : MonoBehaviour {

	public	Sprite			Mute, Unmute;
	public	Button			muteButton;
	public	bool			mute;

	[Header("Sound Effects")]
	new private	AudioSource	audio;
	public		AudioClip	jump;
	public		AudioClip	slide;

	private static soundController instance = null;
	public static soundController Instance {
		get { return instance; }
	}

	void Start() {
		audio = GetComponent<AudioSource> ();
		instance = this;

		if (PlayerPrefs.GetInt ("Muted") == 0) {
			mute = false;
			audio.mute = false;
			muteButton.image.sprite = Unmute;
		} else if (PlayerPrefs.GetInt ("Muted") == 1) {
			mute = true;
			audio.mute = true;
			muteButton.image.sprite = Mute;
		}
	}

	void Update() {
		if (PlayerPrefs.GetInt ("Muted") == 0) {
			mute = false;
			muteButton.image.sprite = Unmute;
		} else if (PlayerPrefs.GetInt ("Muted") == 1) {
			mute = true;
			muteButton.image.sprite = Mute;
		}
	}

	public void MuteButton() {
		if (!mute && !audio.mute) {
			PlayerPrefs.SetInt ("Muted", 1);
			mute = true;
			audio.mute = true;
			if (SceneManager.GetActiveScene ().name == "gameScreen") {
				muteButton.image.sprite = Mute;
			}
		} else if (mute && audio.mute) {
			PlayerPrefs.SetInt ("Muted", 0);
			mute = false;
			audio.mute = false;
			if (SceneManager.GetActiveScene ().name == "gameScreen") {
				muteButton.image.sprite = Unmute;
			}
		}
	}

	public static void playSound(soundFX currentSound) {
		switch (currentSound) {
		case soundFX.JUMP:
			instance.GetComponent<AudioSource>().PlayOneShot (instance.jump);
			break;

		case soundFX.SLIDE:
			instance.GetComponent<AudioSource>().PlayOneShot (instance.slide);
			break;
		}
	}
}