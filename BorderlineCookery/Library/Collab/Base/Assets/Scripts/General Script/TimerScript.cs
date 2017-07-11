using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

	float totalCookingTime = 3.0f;
	float cookingTimer;
	private Text temp;

	// Use this for initialization
	void Start () 
	{
		temp  = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
		cookingTimer = totalCookingTime - Player.cookingTimer;
		temp.text = "" + Mathf.Round(cookingTimer);
	}
}
