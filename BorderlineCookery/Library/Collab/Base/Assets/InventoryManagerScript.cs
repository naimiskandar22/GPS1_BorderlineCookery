using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IngredientObject
{
	public enum IngredientType
	{
		FRUIT = 0,
		MEAT,
		HERB,
		VEGE,
	}

	public string ingredientName;
	public IngredientType ingredientType;
	public int ingredientLevel = 0;
	public int quantity;
}

[System.Serializable]
public class RecipeObject
{
	public enum RecipeType
	{
		HEAL = 0,
		BUFF,
		DEBUFF,
		LURE,
	}

	public string recipeName;
	public RecipeType recipeType;
	public string[] recipeIngredient;
	public int[] ingredientQuantity;
	public string dishDescription;
}

public class InventoryManagerScript : MonoBehaviour {

	public IngredientObject[] ingredientList;
	public RecipeObject[] recipeList;

	public static InventoryManagerScript instance = null;

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
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
