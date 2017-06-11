using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //For SceneManager
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
	GameObject ingredient;
	public GameObject mainCamera;
	public GameObject merchant;
	public Text timer;

	public int health;

	float speed = 3.0f;
	float cookingTimer = 0.0f;
	float cookingDuration = 3.0f;

	static bool isHarvesting = false;
	static bool isRecipeComplete = false;
	static bool isCooking = false;
	static bool isTrading = false;
	static public bool isPopUp = false; //for detecting pop ups

	//For throwing mechanic
	public float bulletspeed = 20f;
	public GameObject hudCanvas;
	
	// Update is called once per frame
	void Update () 
	{
		//Player's Controls
		//If there are pop ups, if player is cooking, trading, harvesting
		//No inputs will be received
		if(isPopUp == false && isCooking == false && isTrading == false)
		{
			//Basic Up Down Left Right Movements
			if(Input.GetKey(KeyCode.W))
			{
				transform.Translate(Vector3.up * Time.deltaTime * speed);
			}
			if(Input.GetKey(KeyCode.A))
			{
				transform.Translate(Vector3.left * Time.deltaTime * speed);
			}
			if(Input.GetKey(KeyCode.D))
			{
				transform.Translate(Vector3.right * Time.deltaTime * speed);
			}
			if(Input.GetKey(KeyCode.S))
			{
				transform.Translate(Vector3.down * Time.deltaTime * speed);
			}

			//Interactions
			//Key: E
			//Ingredients, Merchant
			if(Input.GetKey(KeyCode.E))
			{
				Debug.Log("Interact button pressed");

				isHarvesting = true;

				//Trading
				if(isTrading)
				{
					SceneManager.LoadScene(3, LoadSceneMode.Additive);
					(mainCamera.GetComponent(typeof(AudioListener)) as AudioListener).enabled = false;
					isPopUp = true;
				}
			}

			//Open up Recipe Menu
			//Key: J
			//Press again to close recipe menu
			if(Input.GetKeyDown(KeyCode.J))
			{
				Debug.Log("Cook button pressed");
				SceneManager.LoadScene("RecipeScene", LoadSceneMode.Additive);
				(mainCamera.GetComponent(typeof(AudioListener)) as AudioListener).enabled = false;
				isPopUp = true;
			}

			//Implementation of food throwing
			//Key: SPACE
			if(Input.GetKey(KeyCode.Space))
			{
				if(this.GetComponent<LaunchArcRenderer>().upwards == true) 
				{
					this.GetComponent<LaunchArcRenderer>().angle += 1f;
				}
				else
				{
					this.GetComponent<LaunchArcRenderer>().angle -= 1f;
				}
			}
			if(Input.GetKeyDown(KeyCode.Space))
			{
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
			}

			//Cycling through cooked dishes
			//Key: Mouse
			/*if(Input.GetMouseButtonDown(0))
			{
				GameObject temp = GetComponent<FoodList>().foodList[0];
				GetComponent<FoodList>().foodList.RemoveAt(0);
				GetComponent<FoodList>().foodList.Add(temp);
				GetComponent<FoodList>().UpdateList();
			}*/
			if(Input.GetMouseButtonDown(1))
			{
				GameObject temp = GetComponent<FoodList>().foodList[GetComponent<FoodList>().foodList.Count - 1];
				GetComponent<FoodList>().foodList.RemoveAt(GetComponent<FoodList>().foodList.Count - 1);
				GetComponent<FoodList>().foodList.Insert(0, temp);
				GetComponent<FoodList>().UpdateList();
			}
		}

		//Detect if player is cooking
		//Cooking Timer
		if(isCooking)
		{
			GetComponent<SpriteRenderer>().color = Color.yellow;

			cookingTimer += Time.deltaTime;
			timer.transform.position = transform.position + new Vector3(0.0f,2.0f,0.0f);

			if(cookingTimer > cookingDuration)
			{
				cookingTimer = 0.0f;
				isCooking = false;
				GetComponent<SpriteRenderer>().color = Color.white;
				isRecipeComplete = false;
				Destroy(ingredient);
				ingredient = null;
			}
		}
	}

	//Player taking damages?
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
	void OnTriggerStay2D(Collider2D other)
	{
		//Colliding with ingredients
		if(other.gameObject.GetComponent<Mushroom>() != null && isHarvesting)
		{
			other.transform.position = transform.position + new Vector3(0.0f,1.0f,0.0f);
			other.transform.parent = transform;
			ingredient = other.gameObject;
			isRecipeComplete = true;
		}
		//Colliding with merchant
		else if(other.gameObject.tag == "Merchant")
		{
			isTrading = true;
		}
	}

	//Is triggered when player collide with a collider for the first moment
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
	void OnTriggerExit2D(Collider2D other)
	{
		//Debug.Log("Left Collision");

		if(other.gameObject.tag == "Merchant")
		{
			isTrading = false;
		}
	}
}
