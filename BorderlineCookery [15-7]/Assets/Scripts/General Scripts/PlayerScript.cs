using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour 
{
	public enum PlayerState
	{
		PAUSED,
		IDLE,
		WALKING,
		HARVESTING,
		IN_RECIPE_BOOK,
		COOKING,
		SERVING,
	}

	public static PlayerScript Instance;

	//public variables first
	[Header("General")]
	public int health = 1;
	public float speed = 3.0f;
	public static Animator animator;
	public PlayerState currPlayerState;
	public PlayerState prevPlayerState;
	public GameObject inventoryManager;
	public bool playingPostAnimation;
	private InventoryManagerScript inventory;

	[Header("Harvesting")]
	public int dropQuantity;
	public string dropName;
	public static bool hasDropped;
	public static Sprite dropImage;
	private bool onIngredient = false;
	private GameObject tempIngredient;

	[Header("Cooking")]
	public GameObject cookingMenu;
	public GameObject timerCanvas;
	public float cookingTimer = 0.0f ;
	public float cookingDuration = 2.0f;
	public string addedFood;
	public static bool cookingEnd = false;

	[Header("Serving")]
	public float dishSpeed = 20.0f;
	private FoodList foodListScript;

	[Header("Border Timer")]
	public GameObject deathHud;
	public float deathDuration = 5.0f;
	public float deathTimer;

	//SFX
	public AudioClip bushHarvest1;
	public AudioClip bushHarvest2;
	//	public AudioClip cooking;
	public AudioClip cooking1;
	public AudioClip cooking2;
	public AudioClip cooking3;
	public AudioClip cookComplete;
	//	public AudioClip lootHarvest;
	public AudioClip throwing;
	public AudioClip footSteps;

	private int soundCounter;

	void Awake()
	{
		Instance = this;
		currPlayerState = PlayerState.IDLE;
		prevPlayerState = PlayerState.PAUSED;

		foodListScript = GetComponent<FoodList>();
	}

// Use this for initialization
	void Start () 
	{
		animator = this.GetComponent<Animator>();

		inventoryManager = GameObject.Find("InventoryManager");
		inventory= inventoryManager.GetComponent<InventoryManagerScript>();

		deathTimer = deathDuration;
	}

	// Update is called once per frame
	void Update () 
	{
		if(currPlayerState == PlayerState.PAUSED)
		{
			//Restrict any inputs
		}
		else if(currPlayerState == PlayerState.IDLE || currPlayerState == PlayerState.WALKING)
		{
			//Player movements
			//Key: A/Left, D/Right
			//==================================================================================
			if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
			{
				prevPlayerState = currPlayerState;
				currPlayerState = PlayerState.WALKING;
			}
			//Harvesting on bush/ loot bag
			//Key: E
			//==================================================================================
			if(Input.GetKeyDown(KeyCode.E))
			{
				if(onIngredient && tempIngredient.GetComponent<DropIngredients>().canHarvest)
				{
					prevPlayerState = currPlayerState;
					currPlayerState = PlayerState.HARVESTING;
				}
			}
			//Player cook
			//Key: J, J/ Esc
			//==================================================================================
			if(Input.GetKeyDown(KeyCode.F))
			{
				prevPlayerState = currPlayerState;
				currPlayerState = PlayerState.IN_RECIPE_BOOK;
			}
			//Cycling through dishes
			//Key: Left mouse click, right mouse click
			//==================================================================================
			if(Input.GetKeyDown(KeyCode.W))
			{
				if(foodListScript.foodList.Count > 1)
				{
					GameObject temp = foodListScript.foodList[0];
					foodListScript.foodList.RemoveAt(0);
					foodListScript.foodList.Add(temp);
					foodListScript.UpdateList();
				}
			}
			if(Input.GetKeyDown(KeyCode.D))
			{
				if(foodListScript.foodList.Count > 1)
				{
					GameObject temp = foodListScript.foodList[foodListScript.foodList.Count - 1];
					foodListScript.foodList.RemoveAt(foodListScript.foodList.Count - 1);
					foodListScript.foodList.Insert(0, temp);
					foodListScript.UpdateList();
				}
			}
			//Player serves dishes
			//Key: SPACE
			//==================================================================================
			if(Input.GetKeyDown(KeyCode.Q))
			{
				prevPlayerState = currPlayerState;
				currPlayerState = PlayerState.SERVING;
			}
		}
		//!Player's states:
		//PAUSED - Restrict any inputs to player
		//WALKING - Player in walking state
		//HARVESTING - Player in harvesting state
		//IN_RECIPE_BOOK - Player in recipe menu
		//COOKING - Player in cooking state
		//SERVING - Player in serving/throwing state
		if(currPlayerState == PlayerState.IN_RECIPE_BOOK)
		{
			//Restrict any inputs
		}
		if (currPlayerState == PlayerState.IDLE)
		{
			animator.SetBool("Idle", true);
			playingPostAnimation = false;
		}
		else if(currPlayerState == PlayerState.WALKING)
		{
			//When player is walking
			animator.SetBool("Idle", false);
			animator.SetBool("Walking", true);

			if(Input.GetKey(KeyCode.LeftArrow))
			{
				transform.Translate(Vector3.left * Time.deltaTime * speed);
				GetComponent<SpriteRenderer>().flipX = true;

				soundCounter++;

				if(soundCounter == 40)
				{
					soundCounter = 1;
				}

				if(soundCounter == 1)
				{
					SoundManager.instance.playSingle (footSteps);
				}
				else if(soundCounter == 20)
				{
					SoundManager.instance.playSingle (footSteps);
				}
			}
			else if(Input.GetKey(KeyCode.RightArrow))
			{
				transform.Translate(Vector3.right * Time.deltaTime * speed);
				GetComponent<SpriteRenderer>().flipX = false;

				soundCounter++;

				if(soundCounter == 40)
				{
					soundCounter = 1;
				}

				if(soundCounter == 1)
				{
					SoundManager.instance.playSingle (footSteps);
				}
				else if(soundCounter == 20)
				{
					SoundManager.instance.playSingle (footSteps);
				}
			}
			else if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftArrow) &&
				!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.RightArrow))
			{
				animator.SetBool("Walking", false);
				prevPlayerState = currPlayerState;
				currPlayerState = PlayerState.IDLE;
			}
		}
		else if(currPlayerState == PlayerState.HARVESTING)
		{
			if(Input.GetKeyDown(KeyCode.E))
			{
				animator.SetBool("Idle", false);
				animator.SetBool("Walking", false);
				animator.SetTrigger("Harvesting");
				SoundManager.instance.randomizeSfx (bushHarvest1, bushHarvest2);

				tempIngredient.GetComponent<DropIngredients>().RandNumGen();
				tempIngredient.GetComponent<DropIngredients>().dropCount -= 1;
				dropName = tempIngredient.GetComponent<DropIngredients>().myName;
				dropQuantity = tempIngredient.GetComponent<DropIngredients>().dropq;

				for(int i = 0; i < inventory.ingredientList.Length; i++)
				{
					if(inventory.ingredientList[i].ingredientName == dropName)
					{
						dropImage = inventory.ingredientList[i].image;
						Debug.Log(dropName + " " + dropQuantity);
						inventory.ingredientList[i].quantity += dropQuantity;
						hasDropped = true;
					}
				}
				prevPlayerState = currPlayerState;
				currPlayerState = PlayerState.IDLE;
			}
		}
		else if(currPlayerState == PlayerState.IN_RECIPE_BOOK)
		{
			animator.SetBool("Idle", false);
			animator.SetBool("Walking", false);
			animator.SetBool("In_Recipe_Book", true);

			if(!cookingMenu.activeInHierarchy)
			{
				cookingMenu.SetActive(true);
			}
		}
		else if(currPlayerState == PlayerState.COOKING)
		{
			animator.SetBool("Idle", false);
			animator.SetBool("In_Recipe_Book", false);
			animator.SetBool("Cooking", true);

			soundCounter++;

			if(soundCounter == 40)
			{
				soundCounter = 1;
			}

			if(soundCounter == 1)
			{
				SoundManager.instance.randomizeSfx (cooking1, cooking2, cooking3);
			}
			else if(soundCounter == 20)
			{
				SoundManager.instance.randomizeSfx (cooking1, cooking2, cooking3);
			}

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

					SoundManager.instance.randomizeSfx (cookComplete);
				}
				cookingEnd = true;
				GameObject newBullet = (GameObject)Instantiate(Resources.Load("Dishes/" + addedFood));
				foodListScript.foodList.Add(newBullet);
				newBullet.GetComponent<Bullet>().ResetStats();
				Debug.Log(foodListScript.foodList.Count + "string");
				foodListScript.UpdateList();
			}
			else if(Input.GetKeyDown(KeyCode.R))
			{
				cookingEnd = true;
			}
			else 
			{
				cookingEnd = false;
				cookingTimer += Time.deltaTime;

				if(cookingMenu.activeInHierarchy)
				{
					cookingMenu.SetActive(false);
				}

				if(!timerCanvas.activeInHierarchy)
				{
					timerCanvas.SetActive(true);
				}
			}

			if(cookingEnd)
			{
				animator.SetBool("Cooking", false);
				cookingTimer = 0;
				if(timerCanvas.activeInHierarchy)
				{
					timerCanvas.SetActive(false);
				}
				prevPlayerState = currPlayerState;
				currPlayerState = PlayerState.IDLE;
			}
		}
		else if(currPlayerState == PlayerState.SERVING)
		{
			animator.SetBool("Idle", false);
			animator.SetBool("Walking", false);

			if(Input.GetKeyDown(KeyCode.Q))
			{
				Time.timeScale = 0.5f;
				GetComponent<SpriteRenderer>().flipX = false;
				for(int i = 0; i < LaunchArcRenderer.Instance.resolution; i++)
				{
					GameObject newarc = Instantiate(LaunchArcRenderer.Instance.arc, transform.position, Quaternion.identity);
					LaunchArcRenderer.Instance.arcpoint.Add(newarc); 
				}
			}
			if(Input.GetKey(KeyCode.Q))
			{
				animator.SetBool("Throwing", true);

				if(LaunchArcRenderer.Instance.upwards) 
				{
					LaunchArcRenderer.Instance.angle += Time.deltaTime * 40;
				}
				else
				{
					LaunchArcRenderer.Instance.angle -= Time.deltaTime * 40;
				}
			}
			if(Input.GetKeyUp(KeyCode.Q) || !Input.GetKey(KeyCode.Q))
			{
				animator.SetBool("Throwing", false);
				SoundManager.instance.randomizeSfx (throwing);

				if(foodListScript.foodList.Count > 0)
				{
					Vector3 dir = Quaternion.AngleAxis(LaunchArcRenderer.Instance.angle, Vector3.forward) * Vector3.right;
					GameObject newbullet = Instantiate(foodListScript.foodList[0], transform.position + new Vector3(1.5f,2), Quaternion.identity);
					foodListScript.BulletCollide(newbullet);
					Destroy(foodListScript.foodList[0]);
					foodListScript.foodList.RemoveAt(0);
					newbullet.GetComponent<Rigidbody2D>().AddForce(dir * dishSpeed);
					foodListScript.DeactivateUIImage();
				}

				for(int i = LaunchArcRenderer.Instance.arcpoint.Count - 1; i >= 0; i--)
				{
					Destroy(LaunchArcRenderer.Instance.arcpoint[i]);
					LaunchArcRenderer.Instance.arcpoint.RemoveAt(i);
				}
				LaunchArcRenderer.Instance.angle = 0f;
				prevPlayerState = currPlayerState;
				currPlayerState = PlayerState.IDLE;
				Time.timeScale = 1f;
			}
		}

		//Checking boaderline for player
		//==================================================================================
		if(!BorderChecker.withinRange)
		{
			deathHud.SetActive(true);
			deathTimer -= Time.deltaTime;
		}
		else if(BorderChecker.withinRange)
		{
			deathHud.SetActive(false);
			deathTimer = deathDuration;
		}

		if(deathTimer <= 0 && !BorderChecker.withinRange)
		{
			Debug.Log ("Player has left the screen and died!");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			deathTimer = -1;
		}
	}

	//Receive damage
	public void TakeDamage(int damage)
	{
		health -= damage;
		if (health <= 0) 
		{
			Debug.Log ("Player is Dead");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	//Player animation event
	//Set currPLayerState to idle
	//After playing the entire animation
	public void SetIdleState()
	{
		if(currPlayerState != PlayerState.IDLE && prevPlayerState != PlayerState.WALKING)
		{
			prevPlayerState = currPlayerState;
			currPlayerState = PlayerState.IDLE;
		}
		playingPostAnimation = false;
	}

	//Is triggered when player collide with a collider at the first moment
	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log("Entered a collider");

		if(other.gameObject.tag == "Food")
		{
			if(foodListScript.foodList.Count < foodListScript.maxLength)
			{
				foodListScript.foodList.Add(other.gameObject);
				other.GetComponent<Bullet>().ResetStats();
				Debug.Log(foodListScript.foodList.Count + "string");
				foodListScript.UpdateList();
			}
		}
		else if(other.gameObject.tag == "Ingredients")
		{
			onIngredient = true;
			tempIngredient = other.gameObject;
			Sprite tempSprite = other.GetComponent<SpriteRenderer>().sprite;
			other.GetComponent<SpriteRenderer>().sprite = other.GetComponent<DropIngredients>().spriteholder;
			other.GetComponent<DropIngredients>().spriteholder = tempSprite;
		}
	}

	//Is triggered when player left a collider
	void OnTriggerExit2D(Collider2D other)
	{
		//Debug.Log("Left a collider");

		if(other.gameObject.tag == "Ingredients")
		{
			onIngredient = false;
			tempIngredient = null;
			Sprite tempSprite = other.GetComponent<SpriteRenderer>().sprite;
			other.GetComponent<SpriteRenderer>().sprite = other.GetComponent<DropIngredients>().spriteholder;
			other.GetComponent<DropIngredients>().spriteholder = tempSprite;
		}
	}
}
