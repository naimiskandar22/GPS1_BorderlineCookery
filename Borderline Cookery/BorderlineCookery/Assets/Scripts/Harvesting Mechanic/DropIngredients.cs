using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropIngredients : MonoBehaviour {

	public int drop;
	public string myName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (drop == 0) 
		{
			myName = "Vege(Clone)";
		}
		else if (drop == 1) 
		{
			myName = "Meat(Clone)";
		}
		else if (drop == 2) 
		{
			myName = "Fruit(Clone)";
		}
		else if (drop == 3) 
		{
			myName = "Herb(Clone)";
		}
	}

	public void RandNumGen()
	{
		drop = Random.Range (0, 3);
	}
}
