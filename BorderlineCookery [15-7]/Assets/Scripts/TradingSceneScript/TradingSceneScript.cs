using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TradingSceneScript : MonoBehaviour {

	public GameObject plant;

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
		{
			Debug.Log("Escape button pressed");
			UnloadScene();
		}
	}

	public void UnloadScene()
	{
		SceneManager.UnloadSceneAsync("TradingScene");
		Player.isPopUp = false;
	}

	public void TradeConfirmed()
	{
		UnloadScene();

		//Old version:
		//to show trading 
		/*if(Player.ingredient != null)
		{
			UnloadScene();
			Destroy(Player.ingredient);
			Player.isRecipeComplete = false;
			var newPlant = Instantiate (plant, Player.ingredient.transform.position, Player.ingredient.transform.rotation);
			Player.ingredient = newPlant;
			Player.isRecipeComplete = true;
		}*/
	}
}
