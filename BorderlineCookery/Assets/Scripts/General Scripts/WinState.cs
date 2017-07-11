using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinState : MonoBehaviour {

	public GameObject boss;
	public bool spawned = false;
	public bool killed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		boss = GameObject.Find ("Slime - King Type(Clone)");
		if (boss != null && spawned == false) 
		{
			spawned = true;
		} 
		else if (boss == null && spawned == true) 
		{
			killed = true;
		}

		if (killed == true) 
		{
			SceneManager.LoadScene ("Game Complete");
		}
	}
}
