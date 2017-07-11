using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropIngredients : MonoBehaviour {

	public enum BushType
	{
		FRUIT = 0,
		MEAT,
		HERB,
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
	public int dropCount = 10;

	// Use this for initialization
	void Start () {
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
				myName = "Carrot";
			}
			else if (drop == 3) 
			{
				myName = "Celery";
			}
			else if (drop == 4) 
			{
				myName = "Cilantro";
			}
			else if (drop == 5) 
			{
				myName = "Coriander";
			}
			else if (drop == 6) 
			{
				myName = "Garlic";
			}
			else if (drop == 7) 
			{
				myName = "Lettuce";
			}
			else if (drop == 8) 
			{
				myName = "Onions";
			}
			else if (drop == 9) 
			{
				myName = "Potato";
			}
			else if (drop == 10) 
			{
				myName = "Radish";
			}
			else if (drop == 11) 
			{
				myName = "Spinach";
			}
		}
		else if(type == BushType.MEAT)
		{
			if (drop == 0) 
			{
				myName = "Eggs";
			}
			else if (drop == 1) 
			{
				myName = "Chicken";
			}
			else if (drop == 2) 
			{
				myName = "Beef";
			}
			else if (drop == 3) 
			{
				myName = "Pork";
			}
			else if (drop == 4) 
			{
				myName = "OrcMeat";
			}
		}
		else if(type == BushType.HERB)
		{
			if (drop == 0) 
			{
				myName = "Chamomile";
			}
			else if (drop == 1) 
			{
				myName = "Chilli";
			}
			else if (drop == 2) 
			{
				myName = "Oregano";
			}
			else if (drop == 3) 
			{
				myName = "Parsley";
			}
			else if (drop == 4) 
			{
				myName = "Pepper";
			}
			else if (drop == 5) 
			{
				myName = "Saffron";
			}
			else if (drop == 6) 
			{
				myName = "Sage";
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
			drop = Random.Range (0, 11);
			dropq = Random.Range(1,4);
		}
		else if(type == BushType.MEAT)
		{
			//Commented for tutorial
			//drop = Random.Range (0, 4);
			drop = 0;
			dropq = Random.Range(1,4);
		}
		else if(type == BushType.HERB)
		{
			drop = Random.Range (0, 6);
			dropq = Random.Range(1,4);
		}
		else if(type == BushType.FRUIT)
		{
			drop = Random.Range (0, 2);
			dropq = Random.Range(1,4);
		}
	}
}
