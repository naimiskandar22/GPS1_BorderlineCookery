using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointList : MonoBehaviour {

	public List<GameObject> waypointList = new List<GameObject> ();
	public List<GameObject> heroesAlive = new List<GameObject> ();


	void Start () 
	{
		foreach (GameObject heroes in GameObject.FindGameObjectsWithTag("Hero")) 
		{
			if (!heroesAlive.Contains (heroes)) 
			{
				heroesAlive.Add (heroes);
			}
		}

		foreach (Transform child in transform) 
		{
			if (!waypointList.Contains (child.gameObject)) 
			{
				waypointList.Add (child.gameObject);
			}
		}
	}
	

}
