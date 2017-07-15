using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{

	public GameObject[] tempList;
	public GameObject[] inventoryList;
	public GameObject[] recipeList;

	public static PlayerInventory instance = null;

	void Awake()
	{

		if (instance == null) {
			instance = this;
		}
		else if (instance != this)
		{
			Destroy (gameObject);
		}
	}

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

		for (int i = 0; i < tempList.Length; i++) 
		{
			tempList [i] = null;
		}

		tempList = Resources.LoadAll<GameObject>("Dishes");

		recipeList = new GameObject[tempList.Length];

		for (int i = 0; i < tempList.Length; i++) 
		{
			GameObject temp = Instantiate (tempList [i]);
			temp.GetComponent<Bullet>().ResetStats();
			recipeList [i] = temp;
			//recipeList [i].GetComponent<FoodRecipe>().myName = temp.GetComponent<FoodRecipe>().myName;
		}
	}
}