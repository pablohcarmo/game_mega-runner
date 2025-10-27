using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAll : MonoBehaviour {

	void Update () {
		PlayerPrefs.GetInt ("Points");
		PlayerPrefs.GetInt ("Record");
	}

	public void DeleteRecord () {
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.GetInt ("Points");
		PlayerPrefs.GetInt ("Record");
	}
}
