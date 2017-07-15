using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Scene Manager

public class LevelSelect : MonoBehaviour {

	public string backMainMenu; // Back Button to Main Menu
	public string selectLevelOne; // Level One

	public void BackMainMenu()
	{
		SceneManager.LoadScene(backMainMenu);
	}

	public void SelectLevelOne()
	{
		SceneManager.LoadScene(selectLevelOne);
	}
}
