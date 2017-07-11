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
	float resetTimer;
	float resetDuration;
	public BushType type;
	public int drop;
	public string myName;
	public int dropq;

	// Use this for initialization
	void Start () {
		resetTimer = 0f;
		resetDuration = 2f;
	}

	// Update is called once per frame
	void Update () {

		if(canHarvest == false)
		{
			resetTimer += Time.deltaTime;
			GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
			if(resetTimer >= resetDuration)
			{
				canHarvest = true;
				resetTimer = 0f;
				GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			}
		}

		if(type == BushType.VEGE)
		{
			if (drop == 0) 
			{
				myName = "Bread(Clone)";
			}
			else if (drop == 1) 
			{
				myName = "Cabbage(Clone)";
			}
			else if (drop == 2) 
			{
				myName = "Carrot(Clone)";
			}
			else if (drop == 3) 
			{
				myName = "Celery(Clone)";
			}
			else if (drop == 4) 
			{
				myName = "Cilantro(Clone)";
			}
			else if (drop == 5) 
			{
				myName = "Coriander(Clone)";
			}
			else if (drop == 6) 
			{
				myName = "Garlic(Clone)";
			}
			else if (drop == 7) 
			{
				myName = "Lettuce(Clone)";
			}
			else if (drop == 8) 
			{
				myName = "Onions(Clone)";
			}
			else if (drop == 9) 
			{
				myName = "Potato(Clone)";
			}
			else if (drop == 10) 
			{
				myName = "Radish(Clone)";
			}
			else if (drop == 11) 
			{
				myName = "Spinach(Clone)";
			}
		}
		else if(type == BushType.MEAT)
		{
			if (drop == 0) 
			{
				myName = "Eggs(Clone)";
			}
			else if (drop == 1) 
			{
				myName = "Chicken(Clone)";
			}
			else if (drop == 2) 
			{
				myName = "Beef(Clone)";
			}
			else if (drop == 3) 
			{
				myName = "Pork(Clone)";
			}
			else if (drop == 4) 
			{
				myName = "OrcMeat(Clone)";
			}
		}
		else if(type == BushType.HERB)
		{
			if (drop == 0) 
			{
				myName = "Chamomile(Clone)";
			}
			else if (drop == 1) 
			{
				myName = "Chilli(Clone)";
			}
			else if (drop == 2) 
			{
				myName = "Oregano(Clone)";
			}
			else if (drop == 3) 
			{
				myName = "Parsley(Clone)";
			}
			else if (drop == 4) 
			{
				myName = "Pepper(Clone)";
			}
			else if (drop == 5) 
			{
				myName = "Saffron(Clone)";
			}
			else if (drop == 6) 
			{
				myName = "Sage(Clone)";
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
