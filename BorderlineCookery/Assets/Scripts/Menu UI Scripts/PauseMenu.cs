using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Scene Manager

public class PauseMenu : MonoBehaviour {

	public string mainMenu; // Return to Main Menu

	static public bool isPaused;
	public GameObject pauseMenuCanvas;

	void Start()
	{
		isPaused = false;
	}

	void Update()
	{
		if (isPaused == true) 
		{
			pauseMenuCanvas.SetActive (true);
			Time.timeScale = 0f;
		} 
		else 
		{
			pauseMenuCanvas.SetActive (false);
			Time.timeScale = 1f;
		}

		if(Input.GetKeyDown(KeyCode.Escape) && PlayerScript.Instance.currPlayerState != PlayerScript.PlayerState.IN_RECIPE_BOOK)
		{
			isPaused = !isPaused;
		}
	}

	public void resumeGame()
	{
		isPaused = false;
	}

	public void backToMainMenu()
	{
		SceneManager.LoadScene(mainMenu);
	}

	public void exitGame()
	{
		Debug.Log("Sayonara!");
		Application.Quit();
	}
}
