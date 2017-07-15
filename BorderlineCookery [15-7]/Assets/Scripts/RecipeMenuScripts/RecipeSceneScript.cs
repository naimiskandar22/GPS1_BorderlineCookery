using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RecipeSceneScript : MonoBehaviour {

	public Text timer;
	GameObject player;
	bool isRecipeSelected = false;
	string selectedRecipeName = "";
	Button[] recipeList;

	void Start()
	{
		recipeList = this.GetComponentsInChildren<Button>();
		//btn = this.GetComponentInChildren<Button>();
		//Button[] buttons = this.GetComponentsInChildren<Button>();

		//For some reason AddListener has to be put in start. not update
		//yet it works like update
		//Getting inputs for recipes
		for(int i = 0; i < recipeList.Length; i++)
		{
			if(isRecipeSelected == false)
			{
				recipeList[i].onClick.AddListener(delegate{RecipeSelected();});
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.J))
		{
			Debug.Log("Escape button pressed");
			UnloadScene();
		}
	}

	public void UnloadScene()
	{
		SceneManager.UnloadSceneAsync("RecipeScene");
		Player.isPopUp = false;
	}

	public void RecipeSelected()
	{
		UnloadScene();
		isRecipeSelected = true;

		//Getting and ssetting the name of the pressed button
		var selectedRecipe = EventSystem.current.currentSelectedGameObject;
		if (selectedRecipe != null)
		{
			selectedRecipeName = selectedRecipe.name;
			Debug.Log("Clicked on : "+ selectedRecipeName);
		}
		else
		{
			Debug.Log("currentSelectedGameObject is null");
		}

		//Run script fpr selected recipe based on name
		if(selectedRecipeName == "Recipe01")
		{
			
		}
		else if(selectedRecipeName == "Recipe02")
		{
			
		}
		else if(selectedRecipeName == "Recipe03")
		{

		}
		else if(selectedRecipeName == "Recipe04")
		{

		}
	}
}
