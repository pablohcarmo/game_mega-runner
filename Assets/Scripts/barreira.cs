using UnityEngine;
using System.Collections;

public class barreira : MonoBehaviour {

	public 	float		Speed;
	private	Rigidbody2D barreiraRB;

	public	float		limiteX;
	private	Transform	posBarreira;
	private	bool		pontuado;

	void Start () {

		barreiraRB  = GetComponent<Rigidbody2D> ();
		posBarreira = GetComponent<Transform> (); 
		pontuado = false;
	}

	void Update () {
		barreiraRB.velocity = new Vector2 (Speed, 0);

		float xBarreira = posBarreira.position.x;

		if (xBarreira <= limiteX) {
			Destroy(this.gameObject);
		}

		if (!pontuado) {
			float xPlayer = GameObject.Find("MegaMan").gameObject.transform.position.x;
			if ( xPlayer > xBarreira) {
				megaMan.points += 1;
				pontuado = true;
			}
		}
	}
}