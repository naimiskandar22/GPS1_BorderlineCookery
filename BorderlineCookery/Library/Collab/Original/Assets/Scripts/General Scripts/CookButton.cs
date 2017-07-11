using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CookButton : MonoBehaviour, ISelectHandler, IDeselectHandler {

	public GameObject timerCanvas;
	public InventoryManagerScript inventory;
	public GameObject Parent;

	GameObject player;

	List<string> txt;
	List<int> availableIng;
	List<int> requiredIng;
	string ingredientText;
	string tempname;
	string dishDescription;
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
		GetComponent<Outline>().enabled = true;

		ingredientText = "";
		txt.Clear();
		availableIng.Clear();
		requiredIng.Clear();

		tempname = this.transform.Find("Recipe0").GetComponent<Text>().text;

		for(int i = 0; i < inventory.recipeList.Length; i++)
		{
			if(inventory.recipeList[i].recipeName == tempname)
			{
				dishDescription = inventory.recipeList[i].dishDescription;
				for(int j = 0; j < inventory.recipeList[i].recipeIngredient.Length; j++)
				{
					string str1 = inventory.recipeList[i].recipeIngredient[j];

					for(int k = 0; k < inventory.ingredientList.Length; k++)
					{
						if(inventory.ingredientList[k].ingredientName == str1)
						{
							int ing1 = inventory.ingredientList[k].quantity;
							availableIng.Add(ing1);
						}
					}
					int ing2 = inventory.recipeList[i].ingredientQuantity[j];
					txt.Add(str1);
					requiredIng.Add(ing2);
				}
			}
		}

		for(int i = 0; i < txt.Count; i++)
		{
			if(i != 0) ingredientText += "\n";
			ingredientText += txt[i] + " " + availableIng[i] + "/" + requiredIng[i];
		}

		Parent.transform.Find("RecipeBonus").GetComponent<Text>().text = dishDescription;
		Parent.transform.Find("RecipeIngredient0").GetComponent<Text>().text = ingredientText;
	}

	public void OnDeselect(BaseEventData data)
	{
		GetComponent<Outline>().enabled = false;
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

		if(canCook && player.GetComponent<FoodList>().foodList.Count < player.GetComponent<FoodList>().maxLength)
		{
			for(int i = 0; i < inventory.recipeList.Length; i++)
			{
				if(inventory.recipeList[i].recipeName == tempname)
				{
					/*player.GetComponent<Player>().addedFood = inventory.recipeList[i].recipeName;
					Player.isCooking = true;

					//Start Count Down timer
					if(timerCanvas.activeInHierarchy == false)
					{
						timerCanvas.SetActive(true);
					}

					//Close Down Recipe Menu after selected a dish
					Player.isPopUp = false;
					Player.isIdle = true;
					Parent.transform.parent.gameObject.SetActive(false);*/
					PlayerScript.Instance.addedFood = inventory.recipeList[i].recipeName;
					PlayerScript.cookingEnd = false;
					PlayerScript.Instance.prevPlayerState = PlayerScript.Instance.currPlayerState;
					PlayerScript.Instance.currPlayerState = PlayerScript.PlayerState.COOKING;
				}
			}
		}
	}

}
