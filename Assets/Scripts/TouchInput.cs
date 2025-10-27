using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TouchInput	 : MonoBehaviour {

	private	Camera	cam;

	void Start () {
		cam = GetComponent<Camera> ();
	}
	
	void Update () {
		switch(SceneManager.GetActiveScene().name) {
		case "first Screen":
			if (Input.GetButtonDown ("Cancel")) {
				Application.Quit ();
			}
			break;

		case "gameOverScreen":
			if (Input.GetButtonDown ("Cancel")) {
				SceneManager.LoadScene ("firstScreen");
			}
			break;
		}

		if (Input.touchCount > 0) {
			foreach(Touch touch in Input.touches) {
				Ray ray = cam.ScreenPointToRay(touch.position);
				RaycastHit hit;

				if(Physics.Raycast(ray, out hit)) {
					GameObject objeto = hit.transform.gameObject;

					switch(touch.phase) {
					case TouchPhase.Began:
						objeto.SendMessage("OnTouchDown", SendMessageOptions.DontRequireReceiver);
						break;
					
					case TouchPhase.Ended:
						objeto.SendMessage("OnTouchUp", SendMessageOptions.DontRequireReceiver);
						break;

					case TouchPhase.Moved:
						objeto.SendMessage("OnTouchUp", SendMessageOptions.DontRequireReceiver);
						break;
					}
				}
			}
		}
	}
}
