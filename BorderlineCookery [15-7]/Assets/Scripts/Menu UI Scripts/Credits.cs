using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Scene Manager

public class Credits : MonoBehaviour {

	public string backMainMenu; // Back Button to Main Menu

	public void BackMainMenu()
	{
		SceneManager.LoadScene(backMainMenu);
	}
		
}
