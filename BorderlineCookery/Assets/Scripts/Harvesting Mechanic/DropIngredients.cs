using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropIngredients : MonoBehaviour {

	public enum BushType
	{
		FRUIT = 0,
		MEAT,
		MEAT_SPECIAL,
		HERB,
		HERB_SPECIAL,
		VEGE,
	}

	public Sprite spriteholder;
	public bool canHarvest;
//	float resetTimer;
//	float resetDuration;
	public BushType type;
	public int drop;
	public string myName;
	public int dropq;
	public int dropCount;

	// Use this for initialization
	void Start () {
		dropCount = Random.Range(2,4);
		canHarvest = true;
//		resetTimer = 0f;
//		resetDuration = 2f;
	}

	// Update is called once per frame
	void Update () {

		if (dropCount > 0) // <- Check for drop count left
		{
			if (canHarvest == false) 
			{
				GetComponent<SpriteRenderer> ().material.color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
			} 
			else 
			{
				canHarvest = true;
				GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			}
		}
		else
		{
			canHarvest = false;
			GetComponent<SpriteRenderer> ().material.color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
		}
//		if(canHarvest == false)
//		{
//			resetTimer += Time.deltaTime;
//			GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
//			if(resetTimer >= resetDuration)
//			{
//				canHarvest = true;
//				resetTimer = 0f;
//				GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
//			}
//		}

		if(type == BushType.VEGE)
		{
			if (drop == 0) 
			{
				myName = "Bread";
			}
			else if (drop == 1) 
			{
				myName = "Cabbage";
			}
			else if (drop == 2) 
			{
				myName = "Celery";
			}
			else if (drop == 3) 
			{
				myName = "Garlic";
			}
			else if (drop == 4) 
			{
				myName = "Lettuce";
			}
			else if (drop == 5) 
			{
				myName = "Onions";
			}
			else if (drop == 6) 
			{
				myName = "Radish";
			}
		}
		else if(type == BushType.MEAT)
		{
			if (drop == 0) 
			{
				myName = "Eggs";
			}
		}
		else if(type == BushType.MEAT_SPECIAL)
		{
			if (drop == 0) 
			{
				myName = "Beef";
			}
			else if (drop == 1) 
			{
				myName = "Pork";
			}
		}
		else if(type == BushType.HERB)
		{
			if (drop == 0) 
			{
				myName = "Pepper";
			}
			else if (drop == 1) 
			{
				myName = "Saffron";
			}
		}
		else if(type == BushType.HERB_SPECIAL)
		{
			if (drop == 0) 
			{
				myName = "Chilli";
			}
			else if (drop == 1) 
			{
				myName = "Parsley";
			}
		}
		else if(type == BushType.FRUIT)
		{
			if (drop == 0) 
			{
				myName = "Apple";
			}
			else if (drop == 1) 
			{
				myName = "Banana";
			}
			else if (drop == 2) 
			{
				myName = "Strawberry";
			}
		}
	}

	public void RandNumGen()
	{
		if(type == BushType.VEGE)
		{
			drop = Random.Range (0, 7);
			dropq = Random.Range(1,4);
		}
		else if(type == BushType.MEAT)
		{
			drop = 0;
			dropq = Random.Range(1,2);
		}
		else if(type == BushType.MEAT_SPECIAL)
		{
			drop = Random.Range (0, 1);
			dropq = 1;
		}
		else if(type == BushType.HERB)
		{
			drop = Random.Range (0, 1);
			dropq = Random.Range(1,3);
		}
		else if(type == BushType.HERB_SPECIAL)
		{
			drop = Random.Range (0, 1);
			dropq = 1;
		}
		else if(type == BushType.FRUIT)
		{
			drop = Random.Range (0, 2);
			dropq = Random.Range(1,4);
		}
	}
}
