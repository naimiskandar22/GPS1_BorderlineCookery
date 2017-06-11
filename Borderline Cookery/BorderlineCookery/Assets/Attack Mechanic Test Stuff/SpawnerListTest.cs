using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerListTest : MonoBehaviour {

	public GameObject bad;
	public float timeNow;
	float num = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (num <= 2) {
			if (Time.time > timeNow) {
				Instantiate (bad, transform.position, transform.rotation);
				num += 1;
			}
		}
	}
}
