using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarvestLog : MonoBehaviour {

	public Text harvestText;
	private string myName;
	private int dropq;

	// Use this for initialization
	void Start () {
		myName = Player.dropName;
		harvestText = gameObject.GetComponent<Text>();
		harvestText.text = "Harvested: ";
	}
	
	// Update is called once per frame
	void Update () {
		if (Player.hasDropped == true) {
			myName = Player.dropName;
			dropq = Player.dropQuantity;
			string str1 = "(Clone)";
			harvestText = gameObject.GetComponent<Text> ();
			harvestText.text = "Harvested: " + myName.Replace(str1,"") + " X" + dropq;
			Player.hasDropped = false;
		}
	}
}
