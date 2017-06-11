using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Scene Manager

public class MainMenu : MonoBehaviour {

	public string startLevel; // Starting Level

	public string gameSettings; // Level Selection

	public string gameCredits; // Credits

	public void NewGame()
	{
		SceneManager.LoadScene(startLevel);
	}

	public void Settings()
	{
		SceneManager.LoadScene(gameSettings);
	}

	public void Credits()
	{
		SceneManager.LoadScene(gameCredits);
	}

	public void ExitGame()
	{
		Debug.Log("Out We Go!");
		Application.Quit();
	}
		
}
