using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

	public float zoomSpeed;
	public float timeInterval;

	//To be called by border checker
	static public bool isZoomIn = false;
	static public bool isZoomOut = false;

	private float timer = 0.0f;
	private float currentZoom;

	void Start()
	{
		currentZoom = Camera.main.orthographicSize;
	}

	//Two if statements are doing basically the same thing
	//But i cant figure out a way to simplify them yet
	void Update()
	{
		if(isZoomIn)
		{
			timer += Time.deltaTime;

			if(timer >= timeInterval)
			{
				currentZoom -= zoomSpeed;
				timer = 0.0f;
				Camera.main.orthographicSize = currentZoom;
			}

			if(Camera.main.orthographicSize <= 3.6f)
			{
				Camera.main.orthographicSize = 3.6f;
				isZoomIn = false;
			}
		}
		else if(isZoomOut)
		{
			timer += Time.deltaTime;

			if(timer >= timeInterval)
			{
				currentZoom += zoomSpeed;
				timer = 0.0f;
				Camera.main.orthographicSize = currentZoom;
			}

			if(Camera.main.orthographicSize >= 5.0f)
			{
				Camera.main.orthographicSize = 5.0f;
				isZoomOut = false;
			}
		}
	}
}
