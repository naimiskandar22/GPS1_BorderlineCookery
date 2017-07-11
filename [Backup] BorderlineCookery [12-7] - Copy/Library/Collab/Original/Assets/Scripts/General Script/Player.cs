using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //For SceneManager
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
	private GameObject tempIngredient;
	public GameObject mainCamera;
	public GameObject merchant;
	public Text timer;

	public PlayerInventory inventory;

	public int health;
	float speed = 3.0f;
	static public float cookingTimer = 0.0f; // To be access by timer
	float cookingDuration = 3.0f;
	private float holdTime = 0.0f;
	public float requiredTime = 1.0f;

	bool facingLeft = false;
	static bool isHarvesting = false;
	//static bool isRecipeComplete = false;
	static public bool isCooking = false;
	static public bool isTrading = false;
	static public bool isPopUp = false; //for detecting pop ups

	//For throwing mechanic
	public bool isShooting = false;
	public float bulletspeed = 20f;
	public GameObject hudCanvas;

	//CookingMenu UI(Naim)
	public GameObject cookingMenu;
	public GameObject addedFood;

	//Functions start here
	// Update is called once per frame
	//========================================================================================================================================//
	void Update () 
	{
		//Player's Controls
		//If there are pop ups, if player is cooking, trading, harvesting
		//No inputs will be received
		if(isPopUp == false && isCooking == false)
		{
			//Temporary Up Down Movement
			if(Input.GetKey(KeyCode.W))
			{
				transform.Translate(Vector3.up * Time.deltaTime * speed);
			}
			if(Input.GetKey(KeyCode.S))
			{
				transform.Translate(Vector3.down * Time.deltaTime * speed);
			}

			//Basic Controls Left Right
			if(Input.GetKey(KeyCode.A))
			{
				if(!isShooting)
				{
					transform.Translate(Vector3.left * Time.deltaTime * speed);
					this.GetComponent<Animator>().Play("PlayerLeft");
					facingLeft = true;
				}
			}
			else if(Input.GetKey(KeyCode.D))
			{
				if(!isShooting)
				{
					transform.Translate(Vector3.right * Time.deltaTime * speed);
					this.GetComponent<Animator>().Play("PlayerRight");
					facingLeft = false;
				}
			}
			else
			{
				if(!isShooting)
				{
					if(facingLeft == true)
					{
						this.GetComponent<Animator>().Play("PlayerIdleLeft");
					}
					else if(facingLeft == false)
					{
						this.GetComponent<Animator>().Play("PlayerIdleRight");
					}
				}
			}

			//Interactions
			//Key: E
			//Ingredients, Merchant
			if(Input.GetKeyDown(KeyCode.E))
			{
				//Debug.Log("Interact button pressed");

				//Trading
				if(isTrading)
				{
					SceneManager.LoadScene("TradingScene", LoadSceneMode.Additive);
					(mainCamera.GetComponent(typeof(AudioListener)) as AudioListener).enabled = false;
					isPopUp = true;
				}
			}
			else if(Input.GetKey(KeyCode.E))
			{
//				if(isHarvesting)
//				{
//					holdTime += Time.deltaTime;
//
//					if (holdTime >= requiredTime)
//					{
//						for (int i = 0; i < GetComponent<PlayerInventory> ().inventoryList.Length; i++) 
//						{
//							if ( GetComponent<PlayerInventory>().inventoryList[i].GetComponent<Ingredient>().myName == tempIngredient.GetComponent<DropIngredients>().myName) 
//							{
//								GetComponent<PlayerInventory> ().inventoryList [i].GetComponent<Ingredient>().quantity += 1;
//								Debug.Log(GetComponent<PlayerInventory>().inventoryList[i].GetComponent<Ingredient>().myName);
//								Debug.Log(GetComponent<PlayerInventory>().inventoryList[i].GetComponent<Ingredient>().quantity);
//							}
//						}
//						holdTime = 0f;
//					}
//				}
			}
			//Reset hold time for harvesting
			else if(Input.GetKeyUp (KeyCode.E))
			{
				holdTime = 0.0f;
			}

			//Open up Recipe Menu
			//Key: J
			//Press again to close recipe menu
			if(Input.GetKeyDown(KeyCode.J))
			{
				//Debug.Log("Cook button pressed");
				//SceneManager.LoadScene("RecipeScene", LoadSceneMode.Additive);
				//(mainCamera.GetComponent(typeof(AudioListener)) as AudioListener).enabled = false;
				if(cookingMenu.activeInHierarchy == false)
				{
					isPopUp = true;
					cookingMenu.SetActive(true);
				}
			}

			//Implementation of food throwing
			//Key: SPACE
			if(Input.GetKey(KeyCode.Space))
			{
				if(this.GetComponent<LaunchArcRenderer>().upwards == true) 
				{
					this.GetComponent<LaunchArcRenderer>().angle += Time.deltaTime * 50;
				}
				else
				{
					this.GetComponent<LaunchArcRenderer>().angle -= Time.deltaTime * 50;
				}
			}
			if(Input.GetKeyDown(KeyCode.Space))
			{
				isShooting = true;

				for(int i = 0; i < GetComponent<LaunchArcRenderer>().resolution; i++)
				{
					GetComponent<LaunchArcRenderer>().arcpoint[i]= Instantiate(GetComponent<LaunchArcRenderer>().arc, transform.position, Quaternion.identity);
				}
			}
			if(Input.GetKeyUp(KeyCode.Space))
			{
				if(GetComponent<FoodList>().foodList.Count > 0)
				{
					Vector3 dir = Quaternion.AngleAxis(GetComponent<LaunchArcRenderer>().angle, Vector3.forward) * Vector3.right;
					GameObject newbullet = Instantiate(GetComponent<FoodList>().foodList[0], transform.position + new Vector3(1,1), Quaternion.identity);
					GetComponent<FoodList>().BulletCollide(newbullet);
					Destroy(GetComponent<FoodList>().foodList[0]);
					GetComponent<FoodList>().foodList.RemoveAt(0);
					newbullet.GetComponent<Rigidbody2D>().AddForce(dir * bulletspeed);
					GetComponent<FoodList>().DeactivateUIImage();
				}

				for(int i = 0; i < GetComponent<LaunchArcRenderer>().resolution; i++)
				{
					Destroy(GetComponent<LaunchArcRenderer>().arcpoint[i]);
				}

				GetComponent<LaunchArcRenderer>().angle = 0f;

				isShooting = false;
			}

			//Cycling through cooked dishes
			//Key: Mouse
			if(!isShooting)
			{
				if(Input.GetMouseButtonDown(0))
				{
					GameObject temp = GetComponent<FoodList>().foodList[0];
					GetComponent<FoodList>().foodList.RemoveAt(0);
					GetComponent<FoodList>().foodList.Add(temp);
					GetComponent<FoodList>().UpdateList();
				}
				if(Input.GetMouseButtonDown(1))
				{
					GameObject temp = GetComponent<FoodList>().foodList[GetComponent<FoodList>().foodList.Count - 1];
					GetComponent<FoodList>().foodList.RemoveAt(GetComponent<FoodList>().foodList.Count - 1);
					GetComponent<FoodList>().foodList.Insert(0, temp);
					GetComponent<FoodList>().UpdateList();
				}
			}
		}

		//Detect if player is cooking
		//Cooking Timer
		if(isCooking)
		{
			GetComponent<SpriteRenderer>().color = Color.yellow;

			cookingTimer += Time.deltaTime;

			if(cookingTimer > cookingDuration)
			{
				for(int i = 0; i < GetComponent<PlayerInventory>().recipeList.Length; i++)
				{
					if(GetComponent<PlayerInventory>().recipeList[i] == addedFood)
					{
						for(int j = 0; j < GetComponent<PlayerInventory>().recipeList[i].GetComponent<FoodRecipe>().ingredientName.Length; j++)
						{
							string str1 = GetComponent<PlayerInventory>().recipeList[i].GetComponent<FoodRecipe>().ingredientName[j];
							int ing2 = GetComponent<PlayerInventory>().recipeList[i].GetComponent<FoodRecipe>().ingredientQuantity[j];

							for(int k = 0; k < GetComponent<PlayerInventory>().inventoryList.Length; k++)
							{
								if(GetComponent<PlayerInventory>().inventoryList[k].GetComponent<Ingredient>().myName == str1 + "(Clone)")
								{
									GetComponent<PlayerInventory>().inventoryList[k].GetComponent<Ingredient>().quantity -= ing2;
								}
							}
						}
					}
				}
				cookingTimer = 0.0f;
				isCooking = false;
				GetComponent<SpriteRenderer>().color = Color.white;
				GameObject newbullet = Instantiate(addedFood);
				GetComponent<FoodList>().foodList.Add(newbullet);
				newbullet.GetComponent<Bullet>().ResetStats();
				Debug.Log(GetComponent<FoodList>().foodList.Count + "string");
				GetComponent<FoodList>().UpdateList();
			}

			//Cancel Cooking button
			if(Input.GetKeyDown(KeyCode.Space))
			{
				cookingTimer = 0.0f;
				isCooking = false;
			}
		}

			if(isCooking == false)
			{
				GetComponent<SpriteRenderer>().color = Color.white;
			}
		}

	//Player taking damages?
	//========================================================================================================================================//
	public void TakeDamage(int damage)
	{
		health -= damage;
		if (health <= 0) 
		{
			Debug.Log ("Player is Dead");
		}
	}

	//While player stay in collide with a collider
	//Note: even if player is in collide with a collider,
	// but if player is standing still, it will not detect any collision
	//========================================================================================================================================//
	void OnTriggerStay2D(Collider2D other)
	{
		//Colliding with ingredients
		/*if(other.gameObject.GetComponent<Mushroom>() != null && isHarvesting)
		{
			other.transform.position = transform.position + new Vector3(0.0f,1.0f,0.0f);
			other.transform.parent = transform;
			ingredient = other.gameObject;
			isRecipeComplete = true;
		}*/
		//Colliding with merchant
		if(other.gameObject.tag == "Merchant")
		{
			isTrading = true;
		}
		else if(other.gameObject.tag == "Ingredients")
		{
			isHarvesting = true;
			other.GetComponent<DropIngredients> ().RandNumGen ();
			tempIngredient = other.gameObject;
		}
	}

	//Is triggered when player collide with a collider for the first moment
	//========================================================================================================================================//
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Food")
		{
			if(GetComponent<FoodList>().foodList.Count < GetComponent<FoodList>().maxLength)
			{
				GetComponent<FoodList>().foodList.Add(other.gameObject);
				other.GetComponent<Bullet>().ResetStats();
				Debug.Log(GetComponent<FoodList>().foodList.Count + "string");
				GetComponent<FoodList>().UpdateList();
			}
		}
	}

	//Is trigger when pplayer left a collider'
	//========================================================================================================================================//
	void OnTriggerExit2D(Collider2D other)
	{
		//Debug.Log("Left Collision");

		if(other.gameObject.tag == "Merchant")
		{
			isTrading = false;
		}
		else if(other.gameObject.tag == "Ingredients")
		{
			isHarvesting = false;
			tempIngredient = null;
		}
	}
}
