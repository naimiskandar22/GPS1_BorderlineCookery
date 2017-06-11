using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	//private bool isHarvesting = false;
	private bool onIngredient = false;
	private float holdTime = 0f;
	private GameObject temp;

	public float requiredTime = 1.0f;
	public PlayerInventory inventory;

	void Update()
	{
		
		if (Input.GetKey (KeyCode.E))
		{
			if (onIngredient) 
			{
				holdTime += Time.deltaTime;

				if (holdTime >= requiredTime)
				{
					holdTime = 0f;
					for (int i = 0; i < GetComponent<PlayerInventory> ().inventoryList.Length; i++) 
					{
						if ( GetComponent<PlayerInventory>().inventoryList[i].GetComponent<Ingredient>().myName == temp.GetComponent<DropIngredients>().myName) 
						{
							GetComponent<PlayerInventory> ().inventoryList [i].GetComponent<Ingredient>().quantity += 1;
							Debug.Log(GetComponent<PlayerInventory>().inventoryList[i].GetComponent<Ingredient>().myName);
							Debug.Log(GetComponent<PlayerInventory>().inventoryList[i].GetComponent<Ingredient>().quantity);
						}
					}
				}
			}
		}
		else if (Input.GetKeyUp (KeyCode.E))
		{
			//isHarvesting = false;
			holdTime = 0f;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Ingredients"))
		{
			onIngredient = true;
			other.GetComponent<DropIngredients> ().RandNumGen ();
			temp = other.gameObject;
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.CompareTag ("Ingredients"))
		{
			onIngredient = false;
			holdTime = 0f;
			temp = null;
		}

	}
}
