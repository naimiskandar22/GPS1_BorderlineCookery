using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CookButton : MonoBehaviour, ISelectHandler {

	public GameObject timerCanvas;

	GameObject player;

	List<string> txt;
	List<int> availableIng;
	List<int> requiredIng;
	string tempname;
	bool canCook;

	void Start() 
	{
		player = GameObject.FindGameObjectWithTag("Player").gameObject;
		txt = new List<string>();
		availableIng = new List<int>();
		requiredIng = new List<int>();
		canCook = true;
		Button btn = this.gameObject.GetComponent<Button>();
		btn.onClick.AddListener(CheckIngredients);
	}

	public void OnSelect(BaseEventData eventData)
	{
		for(int i = 0; i < 5; i++)
		{
			this.transform.parent.parent.parent.transform.Find("DisplayRecipe").Find("RecipeIngredient" + i).GetComponent<Text>().text = " ";
		}

		txt.Clear();
		availableIng.Clear();
		requiredIng.Clear();

		this.transform.parent.parent.parent.transform.Find("DisplayRecipe").Find("RecipeBonus").GetComponent<Text>().text = this.transform.Find("Recipe0").GetComponent<Text>().text;

		tempname = this.transform.Find("Recipe0").GetComponent<Text>().text;

		for(int i = 0; i < player.GetComponent<PlayerInventory>().recipeList.Length; i++)
		{
			if(player.GetComponent<PlayerInventory>().recipeList[i].GetComponent<FoodRecipe>().myName == tempname + "(Clone)")
			{
				for(int j = 0; j < player.GetComponent<PlayerInventory>().recipeList[i].GetComponent<FoodRecipe>().ingredientName.Length; j++)
				{
					string str1 = player.GetComponent<PlayerInventory>().recipeList[i].GetComponent<FoodRecipe>().ingredientName[j];

					for(int k = 0; k < player.GetComponent<PlayerInventory>().inventoryList.Length; k++)
					{
						if(player.GetComponent<PlayerInventory>().inventoryList[k].GetComponent<Ingredient>().myName == str1 + "(Clone)")
						{
							int ing1 = player.GetComponent<PlayerInventory>().inventoryList[k].GetComponent<Ingredient>().quantity;
							availableIng.Add(ing1);
						}
					}

					int ing2 = player.GetComponent<PlayerInventory>().recipeList[i].GetComponent<FoodRecipe>().ingredientQuantity[j];
					txt.Add(str1);
					requiredIng.Add(ing2);
				}
			}
		}

		for(int i = 0; i < txt.Count; i++)
		{
			this.transform.parent.parent.parent.transform.Find("DisplayRecipe").Find("RecipeIngredient" + i).GetComponent<Text>().text = txt[i] + " " + availableIng[i] + "/" + requiredIng[i];
		}
	}

	public void CheckIngredients()
	{
//		for(int i = 0; i < txt.Count; i++)
//		{
//			if(availableIng[i] < requiredIng[i])
//			{
//				canCook = false;
//			}
//		}

		if(canCook)
		{
			for(int i = 0; i < player.GetComponent<PlayerInventory>().recipeList.Length; i++)
			{
				if(player.GetComponent<PlayerInventory>().recipeList[i].GetComponent<FoodRecipe>().myName == tempname + "(Clone)")
				{
					player.GetComponent<Player>().addedFood = player.GetComponent<PlayerInventory>().recipeList[i];
					Player.isCooking = true;

					//Start Count Down timer
					if(timerCanvas.activeInHierarchy == false)
					{
						timerCanvas.SetActive(true);
					}

					//Close Down Recipe Menu after selected a dish
					Player.isPopUp = false;
					this.gameObject.transform.parent.parent.parent.parent.gameObject.SetActive(false);
				}
			}
		}
	}

}
