using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class recorde : MonoBehaviour {

	public	Text	score;
	public	Text	record;

	void Start () {
		score.text = PlayerPrefs.GetInt ("Points").ToString ();
		record.text = PlayerPrefs.GetInt ("Record").ToString ();
	}
	
	void Update () {
		score.text = PlayerPrefs.GetInt ("Points").ToString ();
		record.text = PlayerPrefs.GetInt ("Record").ToString ();
	}
}
