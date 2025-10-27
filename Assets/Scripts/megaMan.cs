using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class megaMan : MonoBehaviour {

	#pragma warning disable 0414
	private	soundController	soundController;
	private Rigidbody2D 	megaRB;
	private	Animator		anime;

	[Header("Settings")]
	public	float			forceJump;
	public  LayerMask		whatIsGround;

	public	Transform   	groundCheck;
	public	bool			grounded;

	[Header("Slide")]
	public	bool			slide;
	private	float	 		tempTime;
	public	float			timeSlidePlayer;

	[Header("Colliders")]
	public	GameObject[] colliders; // RUN = 0, SLIDE = 1, JUMP = 2
		
	[Header("Score")]
	public	Text			score;
	public	static	int		points;

	[Header("Touchscreen")]
	private	Vector2			startFingerPos;
	public	float			minSwipe = 100f;

	void Start () {
		soundController = FindObjectOfType (typeof(soundController)) as soundController;
		PlayerPrefs.GetInt ("Muted");

		megaRB = GetComponent<Rigidbody2D> ();
		anime = GetComponent<Animator> ();

		colliders [0].SetActive (true);
		colliders [1].SetActive (false);

		points = 0;
		PlayerPrefs.SetInt ("Points", points);
		slide = false;

		if (PlayerPrefs.GetInt ("Muted") == 1) {
			soundController.mute = true;
		} else if (PlayerPrefs.GetInt("Muted") == 0) {
			soundController.mute = false;
		}
	}
	
	void Update () {
		anime.SetBool ("grounded", grounded);
		anime.SetBool ("slide", slide);
		grounded = Physics2D.OverlapCircle (groundCheck.position, 0.02f, whatIsGround);
		score.text = points.ToString ();

		if (Input.touchCount > 0) {
			Touch touch = Input.touches [0];
			switch (touch.phase) {
			case TouchPhase.Began:
				startFingerPos = touch.position;
				break;

			case TouchPhase.Ended:
				float distVertical = (new Vector3 (0, touch.position.y, 0) - new Vector3 (0, startFingerPos.y, 0)).magnitude;
				if (distVertical >= minSwipe) {
					float swipeValue = Mathf.Sign (touch.position.y - startFingerPos.y);
					if (swipeValue > 0 && grounded && !slide) {
						Jump ();
					} else if (swipeValue < 0 && grounded && !slide) {
						Slide ();
					}
				}
				break;
			}
		}

#if UNITY_EDITOR
		if (Input.GetMouseButtonDown (0) && grounded) {
			Jump ();
		}

		if (Input.GetMouseButtonDown (1) && grounded && !slide) {
			Slide ();
		}
#endif

		if (slide) {
			tempTime += Time.deltaTime;
			if (tempTime >= timeSlidePlayer) {
				slide = false;
				colliders[1].SetActive(false);
				colliders [0].SetActive (true);
			}
		}
	}
	
	public void Jump() {
		soundController.playSound (soundFX.JUMP);
		megaRB.AddForce(new Vector2(0, forceJump));
		slide = false;
		StartCoroutine ("Jumping");
	}

	public void Slide() {
		soundController.playSound (soundFX.SLIDE);
		slide = true;
		tempTime = 0;

		colliders [0].SetActive (false);
		colliders [1].SetActive (true);
	}

	void OnTriggerEnter2D(Collider2D colisor) {
		if (colisor.tag == "Barreira") {
			PlayerPrefs.SetInt ("Points", points);
			if (points > PlayerPrefs.GetInt ("Record")) {
				PlayerPrefs.SetInt ("Record", points);
			}
			SceneManager.LoadScene ("gameOverScreen");
		}
	}

	IEnumerator Jumping() {
		colliders [0].SetActive (false);
		colliders [1].SetActive (false);
		colliders [2].SetActive (true);
		yield return new WaitForSeconds (1f);
		colliders[2].SetActive(false);
		colliders[0].SetActive(true);
	}
}