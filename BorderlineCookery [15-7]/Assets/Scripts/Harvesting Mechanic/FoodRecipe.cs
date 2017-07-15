using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodRecipe : MonoBehaviour {

	public string myName;
	public string[] ingredientName;
	public int[] ingredientQuantity;

	// Use this for initialization
	void Start () {
		myName = gameObject.name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
