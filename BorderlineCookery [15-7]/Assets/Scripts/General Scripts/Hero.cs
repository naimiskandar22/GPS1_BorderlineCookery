﻿using System.Collections;
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
	public float RegenAmount;
	public float RegenTime;
	public float BuffTime;
	public float DebuffTime;
	public float LureTime;
	[Header ("Tutorial")]
	public bool TutorialMode;
	public TutorialPrompt _tutorialPrompt;

	[Header ("Header")]
	//Defend Chance Mechanism
	private float defendChanceNum;

	public float maxhealth, maxlure;
	public int regMelee, regArc, regStraight;
	public float regMeleeRate, regArcRate, regStraightRate;
	private ParticleSystem.EmissionModule healParticle;
	private Transform target; //capital T for positions
	private Transform myTransform;
	private float time = 1f;
	public float particleTime = 1.5f;
	private bool HealthRegen,DmgBuff,Dmgdebuff,Lured;
	private float idleTime = 15f;
	private float waypointIdle = 10f;
	private bool finalWaypoint = false;
	private int current = 0;
	private float tutorialIdle = 5f;


	void Awake ()
	{
		myTransform = transform; 
		maxhealth = health;
		if(Melee != null) regMelee = Melee.damage;
		if(RangeArc != null) regArc = RangeArc.damage;
		if(RangeStraight != null) regStraight = RangeStraight.damage;
		if(Melee != null) regMeleeRate = Melee.attackDelay;
		if(RangeArc != null) regArcRate = RangeArc.attackDelay;
		if(RangeStraight != null) regStraightRate = RangeStraight.attackDelay;
		maxlure = LureTime;
	}

	//MOVEMENT
	//=============================================================================================================

	public void FindEnemy()
	{
		time = time - Time.deltaTime;

		if (time <= 0) 
		{
			time = 1f;
			if(target == null) target = _enemyList.EnemiesList [0].enemy.transform;
			Debug.Log ("Target Assigned");
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
		
		if (_tutorialPrompt.startMove == true) 
		{
			if (_enemyList.EnemiesList.Count > 0) 
			{
				
				if (target != null) 
				{
					Debug.Log ("Move to enemy");
					MoveToEnemy ();
				} 
				else 
				{
					Debug.Log ("Found Enemy");
					FindEnemy ();
				}

			}
			else 
			{
				MoveForward ();
			}
			
		}


	}

	//HEALTH EFFECTS AND VALUES
	//================================================================================================================

	public void particlePlaying()
	{

		if (heal.isPlaying) 
		{
			particleTime -= Time.deltaTime;
			if (RegenTime <= 0) 
			{
				heal.Stop ();
				Debug.Log ("Stopped healing animation");
			}
		}
		if (buff.isPlaying) 
		{
			particleTime -= Time.deltaTime;
			if (BuffTime <= 0) 
			{
				buff.Stop ();
				Debug.Log ("Stopped buffing animation");
			}

		}
		if (debuff.isPlaying) 
		{
			particleTime -= Time.deltaTime;
			if (DebuffTime <= 0) 
			{
				debuff.Stop ();
				Debug.Log ("Stopped debuffing animation");
			}
		}


	}

	public void CurrentHealth()
	{
		float calc_health = (health / maxhealth);
		SetHealthBar (calc_health);
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
		if(HealthRegen) RegenTime-=Time.deltaTime;
		if(Dmgdebuff) DebuffTime-=Time.deltaTime;
		if(DmgBuff) BuffTime-=Time.deltaTime;
		if(Lured) LureTime-=Time.deltaTime;

		if(RegenTime <= 0 && HealthRegen)
		{
			HealthRegen = false;
		}
		else if(RegenTime > 0 && HealthRegen)
		{
			if(RegenTime % 1 > 0 && RegenTime % 1 < 0.005) health += RegenAmount;
			if(health >= maxhealth)
			{
				health = maxhealth;
			}
		}
		if (DebuffTime <= 0 && Dmgdebuff == true) 
		{
			Dmgdebuff = false;
			if(Melee != null) Melee.damage = regMelee;
			if(RangeArc != null) RangeArc.damage = regArc;
			if(RangeStraight != null) RangeStraight.damage = regStraight;
			if(Melee != null) Melee.attackDelay = regMeleeRate;
			if(RangeArc != null) RangeArc.attackDelay = regArcRate;
			if(RangeStraight != null) RangeStraight.attackDelay = regStraightRate;

		}
		if (BuffTime <= 0 && DmgBuff == true) 
		{
			DmgBuff = false;
			if(Melee != null) Melee.damage = regMelee;
			if(RangeArc != null) RangeArc.damage = regArc;
			if(RangeStraight != null) RangeStraight.damage = regStraight;
			if(Melee != null) Melee.attackDelay = regMeleeRate;
			if(RangeArc != null) RangeArc.attackDelay = regArcRate;
			if(RangeStraight != null) RangeStraight.attackDelay = regStraightRate;
		}
		if (LureTime <= 0 && Lured == true) 
		{
			Lured = false;
			//LureTime = maxlure;
			gameObject.tag = "Hero";
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
//		if (DmgBuff == true || Dmgdebuff == true || Lured == true || HealthRegen == true) 
//		{
//			checkStatus ();
//		}
		checkStatus();
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
				health += other.GetComponent<Bullet>().bonusStat;
				RegenAmount = other.GetComponent<Bullet>().bonusStat;
				HealthRegen = true;

				if(health >= maxhealth)
				{
					health = maxhealth;
				}

				RegenTime = other.GetComponent<Bullet>().bonusDuration;
				particleTime = other.GetComponent<Bullet>().bonusDuration;
				heal.Play ();
				Debug.Log (name + "Healing animation started");
			} 
			else if (other.GetComponent<Bullet>().foodtype == "BuffAttDmg") 
			{
				Debug.Log ("Buffing animation started");
				DmgBuff = true;
				Dmgdebuff = false;
				if(Melee != null) Melee.damage = regMelee * other.GetComponent<Bullet>().bonusStat;
				if(RangeArc != null) RangeArc.damage = regArc * other.GetComponent<Bullet>().bonusStat;
				if(RangeStraight != null) RangeStraight.damage = regStraight * other.GetComponent<Bullet>().bonusStat;
				BuffTime = other.GetComponent<Bullet>().bonusDuration;
				DebuffTime = 0;
				buff.Play ();
				particleTime = other.GetComponent<Bullet>().bonusDuration;
				//StatusAil (DmgBuff, Dmgdebuff);
			}
			else if (other.GetComponent<Bullet>().foodtype == "BuffAttSpd") 
			{
				Debug.Log ("Buffing animation started");
				DmgBuff = true;
				Dmgdebuff = false;
				if(Melee != null) Melee.attackDelay = regMeleeRate / other.GetComponent<Bullet>().bonusStat;
				if(RangeArc != null) RangeArc.attackDelay = regArcRate / other.GetComponent<Bullet>().bonusStat;
				if(RangeStraight != null) RangeStraight.attackDelay = regStraightRate / other.GetComponent<Bullet>().bonusStat;
				BuffTime = other.GetComponent<Bullet>().bonusDuration;
				DebuffTime = 0;
				buff.Play ();
				particleTime = other.GetComponent<Bullet>().bonusDuration;
				//StatusAil (DmgBuff, Dmgdebuff);
			}
			else if (other.GetComponent<Bullet>().foodtype == "BuffSpecial") 
			{
				Debug.Log ("Buffing animation started");
				DmgBuff = true;
				Dmgdebuff = false;
				if(Melee != null) Melee.damage = regMelee * other.GetComponent<Bullet>().bonusStat;
				if(RangeArc != null) RangeArc.damage = regArc * other.GetComponent<Bullet>().bonusStat;
				if(RangeStraight != null) RangeStraight.damage = regStraight * other.GetComponent<Bullet>().bonusStat;
				if(Melee != null) Melee.attackDelay = regMeleeRate / other.GetComponent<Bullet>().bonusStat;
				if(RangeArc != null) RangeArc.attackDelay = regArcRate / other.GetComponent<Bullet>().bonusStat;
				if(RangeStraight != null) RangeStraight.attackDelay = regStraightRate / other.GetComponent<Bullet>().bonusStat;
				BuffTime = other.GetComponent<Bullet>().bonusDuration;
				DebuffTime = 0;
				buff.Play ();
				particleTime = other.GetComponent<Bullet>().bonusDuration;
				//StatusAil (DmgBuff, Dmgdebuff);
			}
			else if (other.GetComponent<Bullet>().foodtype == "DebuffAttDmg") 
			{
				Debug.Log ("debuffing animation started");
				DmgBuff = false;
				Dmgdebuff = true;
				if(Melee != null) Melee.damage = regMelee / other.GetComponent<Bullet>().bonusStat;
				if(RangeArc != null) RangeArc.damage = regArc / other.GetComponent<Bullet>().bonusStat;
				if(RangeStraight != null) RangeStraight.damage = regStraight / other.GetComponent<Bullet>().bonusStat;
				DebuffTime = other.GetComponent<Bullet>().bonusDuration;
				BuffTime = 0;
				debuff.Play ();
				particleTime = other.GetComponent<Bullet>().bonusDuration;
				//StatusAil (DmgBuff, Dmgdebuff);
			}
			else if (other.GetComponent<Bullet>().foodtype == "DebuffAttSpd") 
			{
				Debug.Log ("debuffing animation started");
				DmgBuff = false;
				Dmgdebuff = true;
				if(Melee != null) Melee.attackDelay = regMeleeRate + other.GetComponent<Bullet>().bonusStat;
				if(RangeArc != null) RangeArc.attackDelay = regArcRate + other.GetComponent<Bullet>().bonusStat;
				if(RangeStraight != null) RangeStraight.attackDelay = regStraightRate + other.GetComponent<Bullet>().bonusStat;
				DebuffTime = other.GetComponent<Bullet>().bonusDuration;
				BuffTime = 0;
				debuff.Play ();
				particleTime = other.GetComponent<Bullet>().bonusDuration;
				//StatusAil (DmgBuff, Dmgdebuff);
			}
			else if (other.GetComponent<Bullet>().foodtype == "Lure") 
			{
				if(gameObject.tag != "Hero_Lure") gameObject.tag = "Hero_Lure";
				Lured = true;
				LureTime = other.GetComponent<Bullet>().bonusDuration;
			}

		} 
	}
}


	


