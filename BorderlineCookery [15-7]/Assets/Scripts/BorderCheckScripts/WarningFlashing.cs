using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningFlashing : MonoBehaviour {

	bool isEnd;

	// Update is called once per frame
	void Update () 
	{
		if (!isEnd) 
		{
			StartCoroutine (Flash ());
		}
	}

	IEnumerator Flash()
	{
		yield return new WaitForSeconds (0.5f);
		isEnd = true;
		GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		yield return new WaitForSeconds (0.5f);
		isEnd = false;
		GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
	}
}
