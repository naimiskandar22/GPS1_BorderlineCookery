using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Scene Manager

public class SettingsConfirm : MonoBehaviour {

	public string mainMenu;

	public static bool isConfirming;

	public GameObject confirmMenuCanvas;

	void Update () 
	{
		if (isConfirming == true) 
		{
			confirmMenuCanvas.SetActive (true);

			if(Input.GetKeyDown(KeyCode.Escape))
			{
				isConfirming = false;
			}
		} 
		else
		{
			confirmMenuCanvas.SetActive (false);
		}
	}

	public void backToMainMenu()
	{
		isConfirming = false;
		SceneManager.LoadScene(mainMenu);
	}

	public void cancelChanges()
	{
		isConfirming = false;
	}
}
