using UnityEngine;
using System.Collections;

public class moveOffsetBG : MonoBehaviour {

	private	Material	currentMaterial;
	public	float		speed;
	private	float		offset;

	void Start () {

		currentMaterial = GetComponent<Renderer> ().material;
	}

	// Update is called once per frame
	void Update () {

		offset += 0.001f;
		currentMaterial.SetTextureOffset("_MainTex", new Vector2(offset * speed, 0));
	}
}
