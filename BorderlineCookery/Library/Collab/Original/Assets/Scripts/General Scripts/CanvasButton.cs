using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasButton : MonoBehaviour {

	public Canvas canvas;
	public KeyCode key;

	void Start () 
	{
		canvas.enabled = false;
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			canvas.enabled = false;
		}
	}

	void Update () 
	{
		if (Input.GetKeyDown (key)) 
		{
			canvas.enabled = true;
		}
	}

}
