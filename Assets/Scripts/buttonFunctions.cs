using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class buttonFunctions : MonoBehaviour {

	[Header("Splash Scene")]
	public	bool 		splashScene;
	public	GameObject	timeBar;
	private	float		tempTime;
	private	float		duration = 1.5f;

	[Header("Movement")]
	public	float		speed;
	public	float		smooth;

	[Header("Cam N Screen")]
	public	Camera		cam;
	public	GameObject	screen1, screen2;

	[Header("Settings")]
	public	bool		gameOverScreen;

	private	float		startTime;
	private	float		JourneyLenght;
	public  Transform	posA;
	public  Transform	posB;
	private string		idMovimento;

	void Start(){
		if (gameOverScreen) {
			cam.GetComponent<Transform> ();
			posA = screen1.transform;
			posB = screen2.transform;
			idMovimento = "";
		}
	}

	void FixedUpdate () {
		if (splashScene) {
			if (duration > 0) {
				tempTime += Time.deltaTime;
				float percentual = (tempTime / duration) * 70;
				float tamanhoBarra = 70 - percentual;
				if (tamanhoBarra < 0) {
					tamanhoBarra = 0;
				}
				timeBar.transform.localScale = new Vector3 (tamanhoBarra, 70, 70);

				if (tempTime >= duration) {
					SceneManager.LoadScene ("firstScreen");
				}
			}
		}
	}

	public void	OnTouchDown(string comando) {
		switch (comando) {
		case "PlayGame":
			SceneManager.LoadScene ("gameScreen");
			break;

		case "GoToMenu":
			SceneManager.LoadScene ("firstScreen");
			break;

		case "Credits":
			movimentoAtoB();
			break;

		case "CreditsClosed":
			movimentoBtoA();
			break;
		}
	}

	void Update(){
		if (gameOverScreen) {
			if (idMovimento != "") {
				float distancia = (Time.time - startTime) * 7f;
				float journey = distancia / JourneyLenght;
		
				switch (idMovimento) {
				case "AB":
					cam.transform.position = Vector3.Lerp (posA.position, posB.position, journey);		 
					if (cam.transform.position == posB.position) {
						idMovimento = "";
					}
					break;
			

				case "BA":
					cam.transform.position = Vector3.Lerp (posB.position, posA.position, journey);
					if (cam.transform.position == posA.position) {
						idMovimento = "";
					}
					break;
				}
			}
		}
	}


	void movimentoAtoB() {
		idMovimento = "AB";
		startTime = Time.time;
		JourneyLenght = Vector3.Distance (posA.position, posB.position);
	}

	void movimentoBtoA() {
		idMovimento = "BA";
		startTime = Time.time;
		JourneyLenght = Vector3.Distance (posB.position, posA.position);
	}
}