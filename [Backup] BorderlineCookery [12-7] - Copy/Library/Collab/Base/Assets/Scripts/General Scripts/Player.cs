using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //For SceneManager
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
	[Header("General")]
	public int health;
	float speed = 3.0f;
	private bool isPaused;
	private bool facingLeft;
	static public bool isIdle;
	static public bool isPopUp; 
	static public Animator animator;

	[Header("Harvesting")]
	//Harvesting
	static public bool isHarvesting;
	private bool onIngredient = false;
	private float holdTime = 0f;
	private GameObject temp;
	static public int dropQuantity;
	static public bool hasDropped = false;
	static public string dropName;
	public float requiredTime = 1.0f;
	public InventoryManagerScript inventory;

	[Header("Cooking Menu")]
	//CookingMenu UI(Naim)
	public GameObject cookingMenu;
	public string addedFood;
	public GameObject TimerCanvas;
	static public bool isCooking;

	[Header("Cooking")]
	static public float cookingTimer = 0.0f; // To be access by timer
	public float cookingDuration = 2.0f;

	[Header("Serving Dishes")]
	//For throwing mechanic
	static public bool isShooting;
	public float bulletangle;
	float bulletspeed = 20f;
	public GameObject hudCanvas;

	[Header("Merchant Trading")]
	public GameObject merchant;
	static public bool isTrading;

	[Header("Border Timer")]
	//Player Border Checker Timer
	public GameObject deathHud;
	static public float timeBeforeDeath = 5f;
	private float tempTimer;

	//Functions start here
	//========================================================================================================================================//
	void Start()
	{
		tempTimer = timeBeforeDeath;
		cookingTimer = 0;
		animator = this.GetComponent<Animator>();
		animator.Play("PlayerIdleRight");

		facingLeft = false;
		isIdle = true;
		isPopUp = false; 
		isHarvesting = false;
		isCooking = false;
		isShooting = false;
		isTrading = false;
	}

	// Update is called once per frame
	//========================================================================================================================================//
	void Update () 
	{
		//Get pause state from pause menu script
		isPaused = PauseMenu.isPaused;

		//Player's Controls
		//If there are pop ups, if player is cooking, trading, NO inputs will be received
		//Two seperated inputs, ONE: Interaction input, TWO: Remaining inputs
		//Interaction input has to be seperated because no other inputs are to received when harvesting
		if(!isPaused && !isPopUp && !isCooking && !isShooting && !isHarvesting)
		{
			//Temporary Up Down Movement
			/*if(Input.GetKey(KeyCode.W))
			{
				transform.Translate(Vector3.up * Time.deltaTime * speed);
			}
			if(Input.GetKey(KeyCode.S))
			{
				transform.Translate(Vector3.down * Time.deltaTime * speed);
			}*/

			//Basic Controls Left Right
			if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			{
				transform.Translate(Vector3.left * Time.deltaTime * speed);
				animator.Play("PlayerLeft");
				facingLeft = true;
				isIdle = false;
			}
			else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
				transform.Translate(Vector3.right * Time.deltaTime * speed);
				animator.Play("PlayerRight");
				facingLeft = false;
				isIdle = false;
			}
			else if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
			{
				isIdle = true;
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
					isIdle = false;
					cookingMenu.SetActive(true);

					animator.Play("PlayerBeforeCook");
				}
			}

			//Implementation of food throwing
			//Key: SPACE
			if(Input.GetKeyDown(KeyCode.Space))
			{
				isShooting = true;
				isIdle = false;
			}


			//Cycling through cooked dishes
			//Key: Mouse
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

		//Interactions
		//Key: E
		//Ingredients, Merchant
		if(!isPaused && !isPopUp && !isCooking && !isShooting)
		{
			if(Input.GetKey(KeyCode.E))
			{
				Debug.Log("Interact button pressed");

				if (onIngredient && temp.GetComponent<DropIngredients>().canHarvest) 
				{
					holdTime += Time.deltaTime;
					animator.Play("PlayerBeforeHarvest");
					isIdle = false;
					isHarvesting = true;

					if (holdTime >= requiredTime)
					{
						holdTime = 0f;
						animator.Play("PlayerAfterHarvest");
						temp.GetComponent<DropIngredients>().canHarvest = false;
						dropName = temp.GetComponent<DropIngredients>().myName;
						dropQuantity = temp.GetComponent<DropIngredients>().dropq;
//						for (int i = 0; i < GetComponent<PlayerInventory> ().inventoryList.Length; i++) 
//						{
//							if ( GetComponent<PlayerInventory>().inventoryList[i].GetComponent<Ingredient>().myName == dropName) 
//							{
//								GetComponent<PlayerInventory> ().inventoryList [i].GetComponent<Ingredient>().quantity += dropQuantity;
//								Debug.Log(GetComponent<PlayerInventory>().inventoryList[i].GetComponent<Ingredient>().myName);
//								Debug.Log(GetComponent<PlayerInventory>().inventoryList[i].GetComponent<Ingredient>().quantity);
//								hasDropped = true;
//							}
//						}

//						string str1 = "(Clone)";
//						dropName.Replace(str1, "");

						for(int i = 0; i < inventory.ingredientList.Length; i++)
						{
							if(inventory.ingredientList[i].ingredientName == dropName)
							{
								inventory.ingredientList[i].quantity += dropQuantity;
								Debug.Log(dropName + " " + dropQuantity);
								hasDropped = true;
							}
						}
					}
				}
				/*//Trading
					if(isTrading)
					{
						SceneManager.LoadScene("TradingScene", LoadSceneMode.Additive);
						(mainCamera.GetComponent(typeof(AudioListener)) as AudioListener).enabled = false;
						isPopUp = true;
						isIdle = true;
					}*/
			}
			//Reset hold time for harvesting when 'E' is released
			else if (Input.GetKeyUp (KeyCode.E))
			{
				if (onIngredient && temp.GetComponent<DropIngredients>().canHarvest) 
				{
					holdTime = 0f;
					animator.Play("PlayerAfterHarvest");
				}
			}
		}

		//Do actions when player is serving dishes
		if(isShooting)
		{
			if(Input.GetKey(KeyCode.Space))
			{
				animator.Play("PlayerBeforeThrow");

				if(this.GetComponent<LaunchArcRenderer>().upwards == true) 
				{
					this.GetComponent<LaunchArcRenderer>().angle += Time.deltaTime * 30;
				}
				else
				{
					this.GetComponent<LaunchArcRenderer>().angle -= Time.deltaTime * 30;
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
				animator.Play("PlayerThrow");

				if(GetComponent<FoodList>().foodList.Count > 0)
				{
					bulletangle = Mathf.Cos(GetComponent<LaunchArcRenderer>().angle) * bulletspeed + 10f;
					Vector3 dir = Quaternion.AngleAxis(GetComponent<LaunchArcRenderer>().angle, Vector3.forward) * Vector3.right;
					GameObject newbullet = Instantiate(GetComponent<FoodList>().foodList[0], transform.position + new Vector3(1.5f,1), Quaternion.identity);
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
			}
			if(isPaused)
			{
				for(int i = 0; i < GetComponent<LaunchArcRenderer>().resolution; i++)
				{
					Destroy(GetComponent<LaunchArcRenderer>().arcpoint[i]);
				}

				isIdle = true;
				isShooting = false;
			}
		}

		//Detect if player is cooking
		//Cooking Timer
		if(isCooking)
		{
			GetComponent<SpriteRenderer>().color = Color.yellow;

			cookingTimer += Time.deltaTime;

			if(cookingTimer >= cookingDuration)
			{
				for(int i = 0; i < inventory.recipeList.Length; i++)
				{
					if(inventory.recipeList[i].recipeName == addedFood)
					{
						for(int j = 0; j < inventory.recipeList[i].recipeIngredient.Length; j++)
						{
							string str1 = inventory.recipeList[i].recipeIngredient[j];
							int ing2 = inventory.recipeList[i].ingredientQuantity[j];

							for(int k = 0; k < inventory.ingredientList.Length; k++)
							{
								if(inventory.ingredientList[k].ingredientName == str1)
								{
									inventory.ingredientList[k].quantity -= ing2;
								}
							}
						}
					}
				}
				cookingTimer = 0;
				isCooking = false;
				GetComponent<SpriteRenderer>().color = Color.white;
				GameObject newbullet = (GameObject)Instantiate(Resources.Load("Dishes/" + addedFood));
				GetComponent<FoodList>().foodList.Add(newbullet);
				newbullet.GetComponent<Bullet>().ResetStats();
				Debug.Log(GetComponent<FoodList>().foodList.Count + "string");
				GetComponent<FoodList>().UpdateList();
			}

			//Cancel Cooking button
			if(Input.GetKeyDown(KeyCode.Space))
			{
				cookingTimer = 0;
				isCooking = false;
			}
		}

		//Close and disable timer for cooking
		if(isCooking == false)
		{
			GetComponent<SpriteRenderer>().color = Color.white;

			if(TimerCanvas.activeInHierarchy)
			{
				TimerCanvas.SetActive(false);
			}
		}

		//Set idle animation
		if(isIdle)
		{
			if(facingLeft == true)
			{
				animator.Play("PlayerIdleLeft");
			}
			else if(facingLeft == false)
			{
				animator.Play("PlayerIdleRight");
			}
		}

		//Check For Border Timer
		if(BorderChecker.withinRange == false)
		{
			deathHud.SetActive(true);
			timeBeforeDeath -= Time.deltaTime;
		}
		else if(BorderChecker.withinRange == true)
		{
			deathHud.SetActive(false);
			timeBeforeDeath = tempTimer;
		}

		if(timeBeforeDeath <= 0 && BorderChecker.withinRange == false)
		{
			Debug.Log ("Player has left the screen and died!");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			timeBeforeDeath = tempTimer;
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
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	//Player animation
	//Set player idle state to true
	//To prevent idle animation to replace action animations
	//Used in player animations event
	//========================================================================================================================================//
	public void SetIdle()
	{
		isIdle = true;
		isHarvesting = false;
		isShooting = false;
		isPopUp = false;
	}

	//While player stay in collide with a collider
	//Note: even if player is in collide with a collider,
	//but if player is standing still, it will not detect any collision
	//========================================================================================================================================//
	void OnTriggerStay2D(Collider2D other)
	{
		//
	}

	//Is triggered when player collide with a collider for the first moment
	//========================================================================================================================================//
	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log("Collision Detected");

		//Colliding with foods on ground
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
		//Colliding with bushes and meat bag
		else if (other.gameObject.tag == "Ingredients")
		{
			onIngredient = true;
			other.GetComponent<DropIngredients> ().RandNumGen ();
			temp = other.gameObject;
			Sprite tempSprite = other.GetComponent<SpriteRenderer>().sprite;
			other.GetComponent<SpriteRenderer>().sprite = other.GetComponent<DropIngredients>().spriteholder;
			other.GetComponent<DropIngredients>().spriteholder = tempSprite;
		}
		//Colliding with merchant
		if(other.gameObject.tag == "Merchant")
		{
			isTrading = true;
		}
	}

	//Is trigger when player left a collider
	//========================================================================================================================================//
	void OnTriggerExit2D(Collider2D other)
	{
		//Debug.Log("Left Collision");

		if(other.gameObject.tag == "Merchant")
		{
			isTrading = false;
		}
		else if (other.gameObject.tag == "Ingredients")
		{
			onIngredient = false;
			temp = null;
			Sprite tempSprite = other.GetComponent<SpriteRenderer>().sprite;
			other.GetComponent<SpriteRenderer>().sprite = other.GetComponent<DropIngredients>().spriteholder;
			other.GetComponent<DropIngredients>().spriteholder = tempSprite;
		}
	}
}
