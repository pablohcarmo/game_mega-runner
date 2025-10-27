using UnityEngine;
using System.Collections;

public class spawnPoint : MonoBehaviour {

	public	GameObject	prefabBarreira;
	public	Transform[]	posicoes;
	public	float		spawnRate;
	private	float 		tempTime;

	void Update () {

		tempTime += Time.deltaTime;
		if (tempTime >= spawnRate) 
		{
			tempTime = 0;
			GameObject tempPrefab = Instantiate (prefabBarreira) as GameObject;

			int rand = Random.Range (0,	100);
			if (rand < 50)
			{
				tempPrefab.transform.position = posicoes [0].position;
			}
			else
			{
				tempPrefab.transform.position = posicoes [1].position;
			}
		}
	}
}
