using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class buttons : MonoBehaviour {

	private	megaMan		megaMan;
	public	bool		carregarMegaMan;
	public	string		comando;

	void Start () {

		if (carregarMegaMan) {
			megaMan = FindObjectOfType (typeof(megaMan)) as megaMan;
		}
	}
	public void	OnTouchDown() {
		switch (comando)
		{
			case "Jump":
				if(carregarMegaMan && megaMan.grounded) {
					megaMan.Jump();
				}
			break;
		
			case "Slide":
				if(carregarMegaMan && megaMan.grounded && !megaMan.slide) {
					megaMan.Slide();
				}
			break;

			case "Start":
			SceneManager.LoadScene ("game");
			break;

		}
	}
	
	public void	OnTouchUp() {
	}
	
	public void	OnTouchMoved() {
	}
	
	public void	OnTouchStay() {
	}
	
	public void	OnTouchCanceled() {
	}
}
