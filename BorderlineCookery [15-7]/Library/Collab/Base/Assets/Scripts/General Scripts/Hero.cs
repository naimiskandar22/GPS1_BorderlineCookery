using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour{

	[Header("General")]
	public float health;
	public float moveSpeed;
	public float distanceToEnemies;
	public float distanceToWaypoint;//apply in unity
	public EnemyList _enemyList;//apply the player in unity
	public WaypointList _waypoint;
	public GameObject healthBar;
	[Header("Dish Effect")]
	public ParticleSystem heal;
	public ParticleSystem buff;
	public ParticleSystem debuff;
	[Header("Buff & Debuff")]
	public AIAttackMelee Melee;
	public AIAttackRangeArc RangeArc;
	public AIAttackRangeLine RangeStraight;
	[Header("Healing, Buff, & Debuff Values")]
	public int HealAmount;
	public int BuffAmount;
	public int DebuffAmount;
	public float BuffTime;
	public float DebuffTime;
	[Header ("Tutorial")]
	public Canvas canvas;
	public bool TutorialMode;

	//Defend Chance Mechanism
	private float defendChanceNum;

	public float maxhealth,maxbuff,maxdebuff;
	private ParticleSystem.EmissionModule healParticle;
	private Transform target; //capital T for positions
	private Transform myTransform;
	private float time = 1f;
	private float particleTime = 1.5f;
	private bool DmgBuff,Dmgdebuff;
	private float idleTime = 15f;
	private float waypointIdle = 10f;
	private bool finalWaypoint = false;
	private int current = 0;
	private bool part1,part2,part3,part4;
	private float tutorialIdle = 5f;


	void Awake ()
	{
		myTransform = transform; 
		maxhealth = health;
		maxbuff = BuffTime;
		maxdebuff = DebuffTime;
		canvas.enabled = false;
	}

	//MOVEMENT
	//=============================================================================================================

	public void FindEnemy()
	{
		time = time - Time.deltaTime;

		if (time <= 0) 
		{
			time = 1f;
			target = _enemyList.EnemiesList [0].enemy.transform;
		}
	}

	public void MoveToEnemy()
	{
		if (Vector3.Distance (target.transform.position, myTransform.transform.position) > distanceToEnemies) 
		{
			myTransform.position += (target.position - myTransform.position).normalized * moveSpeed * Time.deltaTime;
		}
	}

	public void MoveForward()
	{
		idleTime -= Time.deltaTime;

		if (idleTime <= 0) 
		{
			myTransform.position += Vector3.right * moveSpeed * Time.deltaTime;
			//causing stop
		}
		
	}

	public void MoveToWaypoint()
	{
		if (_waypoint.waypointList.Count == 1) 
		{
			Transform waypoint = _waypoint.waypointList [0].transform;

			if (Vector3.Distance (waypoint.transform.position, myTransform.transform.position) > 2) 
			{
				myTransform.position += (waypoint.position - myTransform.position).normalized * moveSpeed * Time.deltaTime;
			}
			if (Vector3.Distance (waypoint.transform.position, myTransform.transform.position) < 3)
			{
				Debug.Log ("Reached Final Waypoint");
				finalWaypoint = true;
				/*waypointIdle -= Time.deltaTime;
				if (waypointIdle <= 0) 
				{
					

				}*/
			}
		}
		else if (_waypoint.waypointList.Count > 1)
		{
			if (current < _waypoint.waypointList.Count - 1 ) 
			{
				Transform waypoint1 = _waypoint.waypointList [current].transform;
				Transform waypoint2 = _waypoint.waypointList [current + 1].transform;
				if (Vector3.Distance (waypoint1.transform.position, myTransform.transform.position) < Vector3.Distance (waypoint2.transform.position, myTransform.transform.position)) 
				{
					if (Vector3.Distance (waypoint1.transform.position, myTransform.transform.position) > distanceToWaypoint) 
					{
						myTransform.position += (waypoint1.position - myTransform.position).normalized * moveSpeed * Time.deltaTime;
					}
					if (Vector3.Distance (waypoint1.transform.position, myTransform.transform.position) < distanceToWaypoint + 1) 
					{
						waypointIdle -= Time.deltaTime;
						if (waypointIdle <= 0) 
						{
							waypointIdle = 10f;
							current += 1;
							Debug.Log ("Add current");
						}
					}	
				}
			} 
			else if (current >= _waypoint.waypointList.Count - 1) 
			{
				Transform waypoint1 = _waypoint.waypointList [current].transform;
				if (Vector3.Distance (waypoint1.transform.position, myTransform.transform.position) > distanceToWaypoint) 
				{
					myTransform.position += (waypoint1.position - myTransform.position).normalized * moveSpeed * Time.deltaTime;
				}
				if (Vector3.Distance (waypoint1.transform.position, myTransform.transform.position) < distanceToWaypoint + 1) 
				{
					Debug.Log ("Reached Final Waypoint");
					finalWaypoint = true;
				}
			}
		}
	}
		

	public void MovementAI()
	{
		if (_enemyList.EnemiesList.Count != 0) 
		{
			FindEnemy ();

			if (target != null) 
			{ //checking if not null
				MoveToEnemy ();
			}

			idleTime = 15f;
		} 
		else if (_enemyList.EnemiesList.Count <= 0 && finalWaypoint == false) 
		{
			MoveToWaypoint ();
		}
		else if (_enemyList.EnemiesList.Count <= 0 && finalWaypoint == true) 
		{
			MoveForward ();
		}

	}


	//Defend Chance RandNum
	public void defendChance()
	{
		if (gameObject.name == "Hero0 - Tutorial Hero" || gameObject.name == "Hero0 - Tutorial Hero (1)" || gameObject.name == "Hero0 - Tutorial Hero (2)") 
		{
			defendChanceNum = Random.Range (0, 10);
		}

		if (gameObject.name == "Hero1 - Warrior") 
		{
			defendChanceNum = Random.Range (0, 2);
		}

		if (gameObject.name == "Hero2 - Archer")
		{
			defendChanceNum = Random.Range (0, 4);
		}

		if (gameObject.name == "Hero3 - Mage")
		{
			defendChanceNum = Random.Range (0, 10);
		}
	}

	//TUTORIAL THINGS
	//================================================================================================================

	void tutorialThing()
	{
		if (_enemyList.EnemiesList.Count <= 0) {
			if (Vector3.Distance (_waypoint.waypointList [0].transform.position, myTransform.transform.position) > distanceToWaypoint && part1 == false) 
			{
				Debug.Log ("Move To Start Pos");
				myTransform.position += (_waypoint.waypointList [0].transform.position - myTransform.position).normalized * moveSpeed * Time.deltaTime;
			}
			if (Vector3.Distance (_waypoint.waypointList [0].transform.position, myTransform.transform.position) < distanceToWaypoint + 1 && part1 == false) 
			{
				Debug.Log ("Checking For Input");
				if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.D)) 
				{
					part1 = true;
					part2 = false;
				}
			}


			if (Vector3.Distance (_waypoint.waypointList [1].transform.position, myTransform.transform.position) > distanceToWaypoint && part1 == true && part2 == false) 
			{
				tutorialIdle -= Time.deltaTime;
				if (tutorialIdle <= 0) 
				{
					Debug.Log ("Move To Second Pos");
					myTransform.position += (_waypoint.waypointList [1].transform.position - myTransform.position).normalized * moveSpeed * Time.deltaTime;
				}

			}
			if (Vector3.Distance (_waypoint.waypointList [1].transform.position, myTransform.transform.position) < distanceToWaypoint + 1 && part1 == true && part2 == false) 
			{
				Debug.Log ("Checking For Input");
				if (Input.GetKeyDown (KeyCode.E)) 
				{
					tutorialIdle = 10f;
					part2 = true;
					part3 = false;
				}
			}

			if (Vector3.Distance (_waypoint.waypointList [2].transform.position, myTransform.transform.position) > distanceToWaypoint && part2 == true && part3 == false) {
				tutorialIdle -= Time.deltaTime;
				if (tutorialIdle <= 0) 
				{
					Debug.Log ("Move To Third Pos");
					myTransform.position += (_waypoint.waypointList [2].transform.position - myTransform.position).normalized * moveSpeed * Time.deltaTime;
				}

			}
			if (Vector3.Distance (_waypoint.waypointList [2].transform.position, myTransform.transform.position) < distanceToWaypoint + 1 && part2 == true && part3 == false) {
				Debug.Log ("Checking For Input");
				if (Input.GetKeyDown (KeyCode.Space)) 
				{
					tutorialIdle = 5f;
					part3 = true;
					part4 = false;
				}
			}

			if (Vector3.Distance (_waypoint.waypointList [3].transform.position, myTransform.transform.position) > distanceToWaypoint && part3 == true) {
				tutorialIdle -= Time.deltaTime;
				if (tutorialIdle <= 0) 
				{
					Debug.Log ("Move To Third Pos");
					myTransform.position += (_waypoint.waypointList [3].transform.position - myTransform.position).normalized * moveSpeed * Time.deltaTime;
				}

			}
			if (Vector3.Distance (_waypoint.waypointList [3].transform.position, myTransform.transform.position) < distanceToWaypoint + 1 &&  part3 == true) {
				Debug.Log ("Checking For Input");
				if (Input.GetKeyDown (KeyCode.E)) 
				{
					tutorialIdle = 30f;
					part4 = true;
				}
			}
		} 
		else 
		{
			FindEnemy ();
			MoveToEnemy ();

		}
		




	}

	//HEALTH EFFECTS AND VALUES
	//================================================================================================================

	public void particlePlaying()
	{

		if (heal.isPlaying) 
		{
			particleTime = particleTime - Time.deltaTime;
			if (particleTime <= 0) 
			{
				heal.Stop ();
				particleTime = 1.5f;
				Debug.Log ("Stopped healing animation");
			}
		}
		if (buff.isPlaying) 
		{
			particleTime = particleTime - Time.deltaTime;

			if (particleTime <= 0) 
			{
				buff.Stop ();
				particleTime = 1.5f;
				Debug.Log ("Stopped buffing animation");
			}

		}
		if (debuff.isPlaying) 
		{
			particleTime = particleTime - Time.deltaTime;

			if (particleTime <= 0) 
			{
				debuff.Stop ();
				particleTime = 1.5f;
				Debug.Log ("Stopped debuffing animation");
			}
		}


	}

	public void CurrentHealth()
	{
		float calc_health = (health / maxhealth);
		SetHealthBar (calc_health);

		if (health < 16) 
		{
			canvas.enabled = true;
		} 
		else 
		{
			canvas.enabled = false;
		}
	}

	public void SetHealthBar(float myhealth)
	{
		healthBar.transform.localScale = new Vector3 (myhealth, 1, 1);
	}

	public void StatusAil(bool Buffing,bool Debuffing)
	{
		if (Buffing == true) 
		{
			Melee.damage += BuffAmount;
			RangeArc.damage += BuffAmount;
			RangeStraight.damage += BuffAmount;
		} 
		else if (Debuffing == true) 
		{
			Melee.damage -= DebuffAmount;
			RangeArc.damage -= DebuffAmount;
			RangeStraight.damage -= DebuffAmount;
		}

	}

	public void checkStatus()
	{
		DebuffTime-=Time.deltaTime;
		BuffTime-=Time.deltaTime;

		if (DebuffTime <= 0 && Dmgdebuff == true) 
		{
			Dmgdebuff = false;
			BuffTime = maxbuff;
			DebuffTime = maxdebuff;
			Melee.damage += DebuffAmount;
			RangeArc.damage += DebuffAmount;
			RangeStraight.damage += DebuffAmount;

		}
		if (BuffTime <= 0 && DmgBuff == true) 
		{
			DmgBuff = false;
			BuffTime = maxbuff;
			DebuffTime = maxdebuff;
			Melee.damage -= BuffAmount;
			RangeArc.damage -= BuffAmount;
			RangeStraight.damage -= BuffAmount;
		}
	}

//	void OnTriggerEnter2D(Collider2D Dish)
//	{
//		Debug.Log ("Collision Detected");
//		if (Dish.tag == "Food") 
//		{
//			if (Dish.GetComponent<Bullet>().foodtype == "Heal") 
//			{
//				if ((health + HealAmount) < maxhealth) 
//				{
//					health += HealAmount;
//				} 
//				else 
//				{
//					health = maxhealth;
//				}
//
//				heal.Play ();
//				Debug.Log (name + "Healing animation started");
//			} 
//			else if (Dish.GetComponent<Bullet>().foodtype == "Buff") 
//			{
//				buff.Play ();
//				Debug.Log ("Buffing animation started");
//				DmgBuff = true;
//				Dmgdebuff = false;
//				StatusAil (DmgBuff, Dmgdebuff);
//
//
//			}
//			else if (Dish.GetComponent<Bullet>().foodtype == "Debuff") 
//			{
//				debuff.Play ();
//				Debug.Log ("debuffing animation started");
//				DmgBuff = false;
//				Dmgdebuff = true;
//				StatusAil (DmgBuff, Dmgdebuff);
//			}
//			else if (Dish.GetComponent<Bullet>().foodtype == "Lure") 
//			{
//
//			}
//
//		} 
//
//	}

	// Update 
	//=========================================================================================================
	void Update () 
	{
		if (!TutorialMode) 
		{
			MovementAI ();
		} 
		else if (TutorialMode)
		{
			tutorialThing ();
		}
		particlePlaying ();
		if (DmgBuff == true || Dmgdebuff == true) 
		{
			checkStatus ();
		}
		CurrentHealth ();

	}

	//Taking Damage
	//==========================================================================================================
	public void TakeDamage(int damage) 
	{
		defendChance ();

		if (gameObject.name == "Hero0 - Tutorial Hero" || gameObject.name == "Hero0 - Tutorial Hero (1)" || gameObject.name == "Hero0 - Tutorial Hero (2)") 
		{
			if (defendChanceNum != 0) 
			{
				health -= damage;
			}
		}
		else if (gameObject.name == "Hero2 - Archer") 
		{
			if (defendChanceNum != 0) 
			{
				health -= damage;
			}
		} 
		else if (gameObject.name == "Hero3 - Mage") 
		{
			if (defendChanceNum != 0) 
			{
				health -= damage;
			}
		} 
		else 
		{
			if (defendChanceNum == 0) 
			{
				health -= damage;
			}
		}
			
		if (health <= 0) 
		{
			Destroy(gameObject);
			Debug.Log ("Hero is Dead");
		}
	}

	public void DishEffect(GameObject other)
	{
		Debug.Log ("Collision Detected");
		if (other.tag == "Food") 
		{
			if (other.GetComponent<Bullet>().foodtype == "Heal") 
			{
				if ((health + HealAmount) < maxhealth) 
				{
					health += HealAmount;
				} 
				else 
				{
					health = maxhealth;
				}

				heal.Play ();
				Debug.Log (name + "Healing animation started");
			} 
			else if (other.GetComponent<Bullet>().foodtype == "Buff") 
			{
				Debug.Log ("Buffing animation started");
				DmgBuff = true;
				Dmgdebuff = false;
				if(Melee != null) Melee.damage += BuffAmount;
				if(RangeArc != null) RangeArc.damage += BuffAmount;
				if(RangeStraight != null) RangeStraight.damage += BuffAmount;
				buff.Play ();
				//StatusAil (DmgBuff, Dmgdebuff);
			}
			else if (other.GetComponent<Bullet>().foodtype == "Debuff") 
			{
				Debug.Log ("debuffing animation started");
				DmgBuff = false;
				Dmgdebuff = true;
				if(Melee != null) Melee.damage -= DebuffAmount;
				if(RangeArc != null) RangeArc.damage -= DebuffAmount;
				if(RangeStraight != null) RangeStraight.damage -= DebuffAmount;
				debuff.Play ();
				//StatusAil (DmgBuff, Dmgdebuff);
			}
			else if (other.GetComponent<Bullet>().foodtype == "Lure") 
			{

			}

		} 
	}
}


	


