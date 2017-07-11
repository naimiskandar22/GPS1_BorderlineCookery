using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderChecker : MonoBehaviour {

	[Header("Game Objects")]
	public Transform[] hero;
	public GameObject warningSign;

	[Header("Distance")]
	public float maximumDistance;

	bool signPresent = false;
	public static bool withinRange = true;
	float playerPos;
	float heroPos;
	public float distanceCalculation;

	//float scrollSpeed = 15.0f;
	//public float scrollFactor = 1.0f;

	void Awake()
	{
		heroPos = hero[0].position.x;
		distanceCalculation = Mathf.Abs(heroPos - transform.position.x);
	}

	// Update is called once per frame
	void Update () 
	{
		for(int i = 0; i < hero.Length; i++)
		{
			if(hero[i] != null)
			{
				if (hero [0] != null) 
				{
					if (hero [i].position.x < hero [0].position.x) 
					{
						Transform temp = hero [0];
						hero [0] = hero [i];
						hero [i] = temp;
					}
					//				if(hero[i].position.x < hero[i-1].position.x)
					//				{
					//					heroPos = hero[i].position.x;
					//				}
					//				else
					//				{
					//					heroPos = hero[i-1].position.x;
					//				}
				} 
				else 
				{
					hero [0] = hero [i];
				}
			}
		}

		if(hero[0] != null) heroPos = hero[0].position.x;

		distanceCalculation = Mathf.Abs(heroPos - transform.position.x);

		//Comparing Distance
		if (distanceCalculation < maximumDistance) 
		{
			withinRange = true;
		}
		else 
		{
			withinRange = false;
		}

		//Creating and destroying warning sign
		if(withinRange == false && signPresent == false)
		{
			GameObject sign = Instantiate (warningSign, transform.position + new Vector3(0f, 3f, 0f), transform.rotation);
			sign.transform.parent = transform;
			signPresent = true;
			CameraZoom.isZoomIn = true;
		}
		else if(withinRange && signPresent)
		{
			Destroy(GameObject.Find("WarningSign(Clone)"));
			signPresent = false;
			CameraZoom.isZoomOut = true;
		}

		/*for(int i = 0; i < hero.Length; i++)
		{
			if (distanceCalculation < 0) 
			{
				distanceCalculation = distanceCalculation * -1.0f;
			}

			if (distanceCalculation < maximumDistance) 
			{
				withinrange = true;
			}

			if (distanceCalculation < maximumDistance && signPresent) 
			{
				Destroy(GameObject.FindWithTag("Finish"));
				signPresent = false;
			}
		}

		if(!withinrange && !signPresent)
		{
			GameObject sign = Instantiate (warningSign, player.position + new Vector3(0f, 1.5f, 0f), player.rotation);
			sign.transform.parent = transform;
			signPresent = true;
		}
*/

//		if (Input.GetKey (KeyCode.A)) 
//		{
//			transform.Translate (new Vector3 (scrollSpeed, 0f, 0f) * Time.deltaTime * scrollFactor);
//		}
//		if (Input.GetKey (KeyCode.D)) 
//		{
//			transform.Translate (new Vector3 (-scrollSpeed, 0f, 0f) * Time.deltaTime * scrollFactor);
//		}
	}


}
