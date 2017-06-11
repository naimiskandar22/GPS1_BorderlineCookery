using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Scene Manager

public class Settings : MonoBehaviour {

	public string confirmBack; // Back Button to Main Menu

	public string cancelBack; // Cancel Button

	public void ConfirmBack()
	{
		SceneManager.LoadScene(confirmBack);
	}

	public void CancelBack()
	{
		SceneManager.LoadScene(cancelBack);
	}
}
