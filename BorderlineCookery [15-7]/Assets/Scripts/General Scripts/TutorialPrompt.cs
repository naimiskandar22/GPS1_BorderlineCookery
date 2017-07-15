using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPrompt : MonoBehaviour {

	public enum TutorialState
	{
		MOVEMENT,
		HARVESTING,
		COOKING,
		RECIPEBOOK,
		COMBAT,
		NONE
	}

	public TutorialState curState;
	public TutorialState preState;
	[Header("Heroes")]
	public GameObject Mage;
	public GameObject Warrior;
	public GameObject Ranger;
	public GameObject MageTxt;
	public GameObject WarriorTxt;
	public GameObject RangerTxt;
	[Header("Player")]
	public GameObject Movement;
	public GameObject Harvest;
	public GameObject Think;
	public GameObject ThinkTxt;
	public GameObject Cooking;
	[Header("Cookbook")]
	public GameObject CookingPause;
	public GameObject CookBook;
	public GameObject Throwing;
	public FoodList _foodlist;
	public GameObject Wall;
	public GameObject CombatPause;

	[Header("Time")]
	public float timeDelay = 0.0f;
	public float timeDelay2 = 0.0f;

	private Transform target;
	private bool anyButton = false;
	private bool anyButton2 = false;
	private bool moveDone = false;
	private bool harvestDone = false;
	private bool cookingDone = false;
	private bool recipeDone = false;
	private bool harvestedOne = false;
	public bool startMove = false;

	// Use this for initialization
	void Start () 
	{
		curState = TutorialState.NONE;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (curState == TutorialState.NONE) 
		{
			if (moveDone != true) 
			{
				preState = TutorialState.NONE;
				curState = TutorialState.MOVEMENT;
			} 
			else if (moveDone == true && harvestDone != true) 
			{
				preState = TutorialState.MOVEMENT;
				curState = TutorialState.HARVESTING;
			} 
			else if (harvestDone == true && cookingDone != true) 
			{
				preState = TutorialState.HARVESTING;
				curState = TutorialState.COOKING;
			} 
			else if (cookingDone == true && recipeDone != true) 
			{
				preState = TutorialState.COOKING;
				curState = TutorialState.RECIPEBOOK;
			} 
			else if (recipeDone == true) 
			{
				preState = TutorialState.RECIPEBOOK;
				curState = TutorialState.COMBAT;
			}

		}
		else if (curState == TutorialState.MOVEMENT) 
		{
			timeDelay += Time.deltaTime;
			if (timeDelay >= 3.0f) 
			{
				Mage.SetActive (true);
				MageTxt.GetComponent<Text>().text = "Hey we are hungry!";
				if (timeDelay >= 6.0f) 
				{
					Mage.SetActive (false);
					if (timeDelay >= 6.5f) 
					{
						Warrior.SetActive (true);
						WarriorTxt.GetComponent<Text>().text = "Go get us some food!";
						if (timeDelay >= 9.5f) 
						{
							Warrior.SetActive (false);
							if (timeDelay >= 10.0f) 
							{
								Ranger.SetActive (true);
								RangerTxt.GetComponent<Text>().text = "And make it quick!";
								if (timeDelay >= 13.0f) 
								{
									Ranger.SetActive (false);
									if (timeDelay >= 13.5f) 
									{
										Movement.SetActive (true);
										if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow)) 
										{
											timeDelay2 += Time.deltaTime;
										}

									}
								}
							}
						}
					}
				}
			}
			if (timeDelay2 >= 3.0f) 
			{
				Movement.SetActive (false);
				timeDelay = 0.0f;
				moveDone = true;
				curState = TutorialState.NONE;
			}
			 
		}
		else if (curState == TutorialState.HARVESTING) 
		{
			
			timeDelay += Time.deltaTime;
			if (timeDelay >= 1.0f) 
			{
				Think.SetActive (true);
				ThinkTxt.GetComponent<Text> ().text = "I should get some ingredients first...";
				if (timeDelay >= 5.0f) 
				{
					Think.SetActive (false);
					Harvest.SetActive (true);
					if (Input.GetKey(KeyCode.E))
					{
						harvestedOne = true;
					}
					if (timeDelay >= 10.0f && harvestedOne == true) 
					{
						Warrior.SetActive (false);
						timeDelay = 0.0f;
						harvestDone = true;
						Harvest.SetActive (false);
						curState = TutorialState.NONE;
					}
					if (timeDelay >= 30.0f && harvestedOne == false) 
					{
						Warrior.SetActive (true);
						WarriorTxt.GetComponent<Text> ().text = "To the left dummy!";
					}
				}
			}
		}
		else if (curState == TutorialState.COOKING) 
		{
			timeDelay += Time.deltaTime;
			if (timeDelay >= 1.0f) 
			{
				Ranger.SetActive (true);
				RangerTxt.GetComponent<Text>().text = "Hey! HURRY UP!";
				if (timeDelay >= 4.0f) 
				{
					Ranger.SetActive (false);
					Think.SetActive (true);
					ThinkTxt.GetComponent<Text> ().text = "Ack! They are getting impatient!";
					if (timeDelay >= 7.0f) 
					{
						Think.SetActive (false);
						if (timeDelay >= 7.5f) 
						{
							Cooking.SetActive (true);
							if (Input.GetKeyDown (KeyCode.F) || Input.GetKeyDown (KeyCode.J)) 
							{
								Cooking.SetActive (false);
								timeDelay = 0.0f;
								cookingDone = true;
								curState = TutorialState.NONE;
							}
						}

					}
				}
			}
		}
		else if (curState == TutorialState.RECIPEBOOK) 
		{
			
			if (_foodlist.foodList.Count > 0) 
			{
				Think.SetActive (true);
				ThinkTxt.GetComponent<Text> ().text = "Okay, time to serve this.";
				timeDelay += Time.deltaTime;
				if (timeDelay >= 2.0f) 
				{
					Think.SetActive (false);
					Throwing.SetActive (true);
				} 
			} 
			else 
			{
				if (anyButton == false) 
				{
					//Time.timeScale = 0f;
					CookingPause.SetActive (true);
				} 
				else 
				{
					//Time.timeScale = 1f;
					CookingPause.SetActive (false);
				}
				if (Input.anyKeyDown) 
				{
					anyButton = true;
				}
			}
			if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Q) && timeDelay > 2.5f) 
			{
				Think.SetActive (false);
				timeDelay = 0.0f;
				Throwing.SetActive (false);
				recipeDone = true;
				curState = TutorialState.NONE;
			}

		}
		else if (curState == TutorialState.COMBAT) 
		{
			timeDelay += Time.deltaTime;
			startMove = true;
			if (timeDelay >= 5.0f) 
			{
				Warrior.SetActive (true);
				WarriorTxt.GetComponent<Text> ().text = "Alright! It's time for us to get going.";
				if (timeDelay >= 8.0f)
				{
					Warrior.SetActive (false);
					Mage.SetActive (true);
					MageTxt.GetComponent<Text>().text = "Indeed. We have been here for too long";
					if (timeDelay >= 11.0f) 
					{	
						
						Ranger.SetActive (true);
						RangerTxt.GetComponent<Text>().text = "Hmph! No thanks to someone";
						if (timeDelay >= 14.0f) 
						{
							Mage.SetActive (false);
							Ranger.SetActive (false);
							Wall.SetActive (false);

							if (timeDelay >= 15.0f) 
							{
								if (anyButton2 == false) 
								{
									Time.timeScale = 0f;
									CombatPause.SetActive (true);
								} 
								else 
								{
									Time.timeScale = 1f;
									CombatPause.SetActive (false);
								}
								if (Input.anyKeyDown) 
								{
									anyButton2 = true;
								}
							}
						}
					}
				}
			}
		}
	}
}
