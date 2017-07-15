using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarvestLog : MonoBehaviour {

	public Text harvestText;
	public Image harvestImage;
	private string myName;
	private int dropq;

	// Use this for initialization
	void Start () {
		myName = PlayerScript.Instance.dropName;
		harvestText = gameObject.GetComponent<Text>();
		harvestText.text = "Harvested: ";
		harvestImage.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerScript.hasDropped == true) 
		{
			harvestImage.enabled = true;
			myName = PlayerScript.Instance.dropName;
			dropq = PlayerScript.Instance.dropQuantity;
			string str1 = "(Clone)";
			harvestText = gameObject.GetComponent<Text> ();
			harvestText.text = "Harvested:      " + myName.Replace(str1,"") + " X" + dropq;
			harvestImage.overrideSprite = PlayerScript.dropImage;
			PlayerScript.hasDropped = false;
		}
	}
}
