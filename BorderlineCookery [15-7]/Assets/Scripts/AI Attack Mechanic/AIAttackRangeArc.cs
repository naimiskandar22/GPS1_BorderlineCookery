﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AIAttackRangeArc : MonoBehaviour {

	private List<GameObject> EnemiesList = new List<GameObject> ();
	private Transform target;

	[Header("Standard Attack")]
	public float attackRange;
	public int damage;
	public float attackDelay;
	private float lastAttackTime;
	private float distanceToTarget;

	private int randomNumber;
	private int repeatCounter;
	private bool isSorted;
	private bool isWithinRange;

	public GameObject projectile;
	public float bulletForce = 600f; // Suggested : 600
	public float bulletHeight = 310f; // Suggested : 310

	[Header("Special Attack")]
	public bool hasSpecialAttack;
	public int specialDamage;
	public float specialAttackDelay;
	private float specialLastAttackTime;

	public void RandNum()
	{
		if (EnemiesList.Count == 2) 
		{
			randomNumber = Random.Range (0, 2);
		} 
		else 
		{
			randomNumber = Random.Range(0, EnemiesList.Count - 1);
		}
	}

	public void lureChecker()
	{
		// If Enemy, Lure will be added to list
		if (gameObject.tag.Contains("Enemies")) 
		{
			foreach (GameObject Lure in GameObject.FindGameObjectsWithTag("Enemies_Lure")) 
			{
				if (!EnemiesList.Contains (Lure)) 
				{
					EnemiesList.Add (Lure);
				} 
			}
			foreach (GameObject Lure in GameObject.FindGameObjectsWithTag("Hero_Lure")) 
			{
				if (!EnemiesList.Contains (Lure)) 
				{
					EnemiesList.Add (Lure);
				} 
			}
		}
	}

	public void theList()
	{
		// If Hero, Enemies will be added to list
		if (gameObject.tag.Contains("Hero")) 
		{
			foreach (GameObject Enemy in GameObject.FindGameObjectsWithTag("Enemies")) 
			{
				if (!EnemiesList.Contains (Enemy)) 
				{
					EnemiesList.Add (Enemy);
				} 
			}
			foreach (GameObject Enemy in GameObject.FindGameObjectsWithTag("Enemies_Lure")) 
			{
				if (!EnemiesList.Contains (Enemy)) 
				{
					EnemiesList.Add (Enemy);
				} 
			}
		}

		// If Enemy, Heroes will be added to list
		if (gameObject.tag.Contains("Enemies")) 
		{
			foreach (GameObject Hero in GameObject.FindGameObjectsWithTag("Hero")) 
			{
				if (!EnemiesList.Contains (Hero)) 
				{
					EnemiesList.Add (Hero);
				} 
			}

			foreach (GameObject Player in GameObject.FindGameObjectsWithTag("Player")) 
			{
				if (!EnemiesList.Contains (Player)) 
				{
					EnemiesList.Add (Player);
				} 
			}
		}
	}

	public void checkNull()
	{
		for (int i = 0; i < EnemiesList.Count; i++) 
		{
			if (EnemiesList [i] == null) 
			{
				EnemiesList.RemoveAt (i);
			}
		}
	}

	// Prototype Distance Checker & Sorter
	public void distanceCheckNSort()
	{
		if (EnemiesList.Count >= 2) 
		{
			do
			{
				isSorted = true;

				for (int i = 0; i < EnemiesList.Count - 1; i++) 
				{
					if (Vector3.Distance (gameObject.transform.position, EnemiesList [i + 1].transform.position) < Vector3.Distance (gameObject.transform.position, EnemiesList [i].transform.position)) 
					{
						var tempList = EnemiesList [i];
						EnemiesList [i] = EnemiesList [i + 1];
						EnemiesList [i + 1] = tempList;
					}
				}
			} while(!isSorted);
		}
	}

	// Prevents Choosing Targets That Are Out Of Range
	public void distantTargetPreventor()
	{
		isWithinRange = false;

		do 
		{
			if(repeatCounter != EnemiesList.Count)
			{
				RandNum();
				target = EnemiesList [randomNumber].transform;
				distanceToTarget = Vector3.Distance (transform.position, target.position);

				if (distanceToTarget > attackRange) 
				{
					isWithinRange = false;
					repeatCounter += 1;
				} 
				else 
				{
					isWithinRange = true;
				}
			}
			else
			{
				target = null;
				isWithinRange = true;
			}

		} while(!isWithinRange);

		repeatCounter = 0;
	}

	// Update is called once per frame
	void Update()
	{
		lureChecker ();
		checkNull ();

		if (EnemiesList.Count != 0 && EnemiesList.Last().tag.Contains("Lure")) 
		{
			target = EnemiesList.Last().transform;
		} 
		else 
		{
			theList ();
			checkNull ();
			distanceCheckNSort ();
			RandNum ();

			if (EnemiesList.Count != 0) // TEMPORARY 'IF' STATEMENT, TO BE CHANGED  // TO ADD - IF WARRIOR HERO HEALTH < 25, ATTACK FRONTMOST TARGET
			{
				target = EnemiesList [randomNumber].transform;
				distanceToTarget = Vector3.Distance (transform.position, target.position);

				if (target == EnemiesList [0].transform && distanceToTarget > attackRange) 
				{
					target = null;
				} 
				else if (distanceToTarget > attackRange) 
				{
					distantTargetPreventor();
				}
			}

			if (target != null) 
			{
				//Check if Target is within Range
				if (distanceToTarget < attackRange) 
				{
					// For rotating character instead of bullet
					/*
				Vector3 targetDir = target.position - transform.position;
				float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 0f;
				Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 90 * Time.deltaTime);
				*/

					//Check for Special Attack Cooldown
					if(hasSpecialAttack == true && Time.time > specialLastAttackTime + specialAttackDelay)
					{
						//Create bullet
						GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);

						if (gameObject.tag.Contains("Hero")) 
						{
							HeroBulletArc.damage = specialDamage;
						} 

						if (gameObject.tag.Contains("Enemies")) 
						{
							EnemyBulletArc.damage = specialDamage;
						}

						//Rotate bullet
						Vector3 dir = target.transform.position - bullet.transform.position;
						float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
						bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

						//Debug.Log (angle); <- DO NOT DELETE YET

						//Shoot bullet
						if (angle > -90 && angle < 90) // Hero Shoots Right, Enemies Shoot Left
						{
							if (target == EnemiesList [0].transform) 
							{
								bullet.GetComponent <Rigidbody2D> ().AddRelativeForce (new Vector2 (600, 200f)); // Needs more testing. Previous : 600, 210
							} 
							else if (target == EnemiesList [1].transform) 
							{
								bullet.GetComponent <Rigidbody2D> ().AddRelativeForce (new Vector2 (600, 225f)); // Needs more testing. Previous : 600, 250
							}
							else
							{
								bullet.GetComponent <Rigidbody2D> ().AddRelativeForce (new Vector2 (bulletForce, (bulletHeight + 0f))); // Suggested : 600, 310
							}
						}
						else // Hero Shoots Left, Enemies Shoot Right
						{
							if (target == EnemiesList [0].transform) 
							{
								bullet.GetComponent <Rigidbody2D> ().AddRelativeForce (new Vector2 (600, -200f)); // Needs more testing. Previous : 600, -210
							} 
							else if (target == EnemiesList [1].transform) 
							{
								bullet.GetComponent <Rigidbody2D> ().AddRelativeForce (new Vector2 (600, -225f)); // Needs more testing. Previous : 600, -250
							}
							else
							{
								bullet.GetComponent <Rigidbody2D> ().AddRelativeForce (new Vector2 (bulletForce, (-bulletHeight + 0f))); // Suggested : 600, -310
							}
						}

						specialLastAttackTime = Time.time;
					}
					else if (Time.time > lastAttackTime + attackDelay) //Check for Regular Attack Cooldown
					{
						//Create bullet
						GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);

						if (gameObject.tag.Contains("Hero")) 
						{
							HeroBulletArc.damage = damage;
						} 

						if (gameObject.tag.Contains("Enemies")) 
						{
							EnemyBulletArc.damage = damage;
						}

						//Rotate bullet
						Vector3 dir = target.transform.position - bullet.transform.position;
						float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
						bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

						//Debug.Log (angle); <- DO NOT DELETE YET

						//Shoot bullet
						if (angle > -90 && angle < 90) // Hero Shoots Right, Enemies Shoot Left
						{
							if (target == EnemiesList [0].transform) 
							{
								bullet.GetComponent <Rigidbody2D> ().AddRelativeForce (new Vector2 (600, 200f)); // Needs more testing. Previous : 600, 210
							} 
							else if (target == EnemiesList [1].transform) 
							{
								bullet.GetComponent <Rigidbody2D> ().AddRelativeForce (new Vector2 (600, 225f)); // Needs more testing. Previous : 600, 250
							}
							else
							{
								bullet.GetComponent <Rigidbody2D> ().AddRelativeForce (new Vector2 (bulletForce, (bulletHeight + 0f))); // Suggested : 600, 310
							}
						}
						else // Hero Shoots Left, Enemies Shoot Right
						{
							if (target == EnemiesList [0].transform) 
							{
								bullet.GetComponent <Rigidbody2D> ().AddRelativeForce (new Vector2 (600, -200f)); // Needs more testing. Previous : 600, -210
							} 
							else if (target == EnemiesList [1].transform) 
							{
								bullet.GetComponent <Rigidbody2D> ().AddRelativeForce (new Vector2 (600, -225f)); // Needs more testing. Previous : 600, -250
							}
							else
							{
								bullet.GetComponent <Rigidbody2D> ().AddRelativeForce (new Vector2 (bulletForce, (-bulletHeight + 0f))); // Suggested : 600, -310
							}
						}

						lastAttackTime = Time.time;
					}
				}
			}
		}
	}
}
