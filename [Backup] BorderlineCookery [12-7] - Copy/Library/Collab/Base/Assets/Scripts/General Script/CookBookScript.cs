using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookBookScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.transform.Find("CookingMenu").Find("Buttons").Find("Recipe0").Find("Button").GetComponent<Button>().Select();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.J))
		{
			//Debug.Log("Escape button pressed");
			Player.isPopUp = false;
			this.gameObject.SetActive(false);
		}
	}
}
