using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	public GameObject[] tempList;
	public GameObject[] inventoryList;

	// Use this for initialization
	void Start ()
	{
		tempList = Resources.LoadAll<GameObject>("Ingredients");

		inventoryList = new GameObject[tempList.Length];

		for (int i = 0; i < tempList.Length; i++) 
		{
			GameObject temp = Instantiate (tempList [i]);
			inventoryList [i] = temp;
			inventoryList [i].GetComponent<Ingredient> ().myName = temp.GetComponent<Ingredient> ().myName;
		}
	}
}