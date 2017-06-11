using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour {

	//Ingredient Variables
	public string myName;
	public int quantity;
	public int value;

	// Use this for initialization
	void Start ()
	{
		//Ingredient Variables
		myName = gameObject.name;
		quantity = 0;
	}
}
