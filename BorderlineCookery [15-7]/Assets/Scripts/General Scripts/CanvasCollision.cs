using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCollision : MonoBehaviour {

	public Canvas canvas;
	public Canvas canvas2;
	public bool useNo2 = false;

	void Start()
	{
		canvas.enabled = false;
		if (useNo2 == true) 
		{
			canvas2.enabled = false;
		}

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			canvas.enabled = true;
		}

	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			canvas.enabled = false;
		}
		if (other.gameObject.tag == "Hero" && useNo2 == true) 
		{
			canvas2.enabled = true;
		}
	}
}
