using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AIAttackMelee : MonoBehaviour {

	public List<GameObject> EnemiesList = new List<GameObject> ();
	public Transform target;
	public bool Attacking;

	[Header("Standard Attack")]
	public float attackRange;
	public int damage;
	public float attackDelay;
	private float lastAttackTime;
	public float knockback;
	private int knockbackCounter;

	[Header("Special Attack")]
	public bool hasSpecialAttack;
	public int specialDamage;
	public float specialAttackDelay;
	private float specialLastAttackTime;

	private bool isSorted;

	public void lureChecker()
	{
		// If Enemy, Lure will be added to list
		if (gameObject.tag.Contains("Enemies")) 
		{
			foreach (GameObject Lure in GameObject.FindGameObjectsWithTag("Enemies_Lure")) 
			{
				if(Lure != this.gameObject)
				{
					if (!EnemiesList.Contains (Lure)) 
					{
						EnemiesList.Add (Lure);
					} 
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

	void Start()
	{
		knockbackCounter = 0;
	}
		
	// Update is called once per frame
	void Update() 
	{
		lureChecker ();
		theList ();
		checkNull ();
		distanceCheckNSort();

		if (EnemiesList.Count != 0) // TEMPORARY 'IF' STATEMENT - TO BE CHANGED
		{
			target = EnemiesList [0].transform;
		}

		for(int i = 0; i < EnemiesList.Count; i++)
		{
			if(EnemiesList[i].tag == "Hero_Lure") target = EnemiesList[i].transform;
		}
		for(int i = 0; i < EnemiesList.Count; i++)
		{
			if(EnemiesList[i].tag == "Enemies_Lure") target = EnemiesList[i].transform;
		}
		

		if(target != null) 
		{
			float distanceToTarget = Mathf.Abs(Vector3.Distance(transform.position, target.position));
			Vector3 dir = target.transform.position - transform.position;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

			//Attack Range Check
			if(distanceToTarget < attackRange) 
			{
				Attacking = true;

				//Check for Special Attack & Regular Attack Cooldown
				if(hasSpecialAttack == true && Time.time > specialLastAttackTime + specialAttackDelay)
				{
					target.SendMessage("TakeDamage", specialDamage);

					if (angle > -90 && angle < 90) // Hero Knockbacks Right, Enemies Knockbacks Left
					{
						target.GetComponent<Rigidbody2D>().AddForce (new Vector2 (knockback, 0f));
					}
					else // Hero Knockbacks Left, Enemies Knockbacks Right
					{
						target.GetComponent<Rigidbody2D>().AddForce (new Vector2 (-knockback, 0f));
					}

					specialLastAttackTime = Time.time;
				}
				else if(Time.time > lastAttackTime + attackDelay) 
				{
					target.SendMessage("TakeDamage", damage);
					knockbackCounter++;

					if(knockbackCounter >= 2 && target != null)
					{
						if (angle > -90 && angle < 90) // Hero Knockbacks Right, Enemies Knockbacks Left
						{
							target.GetComponent<Rigidbody2D>().AddForce (new Vector2 (knockback, 0f));
						}
						else // Hero Knockbacks Left, Enemies Knockbacks Right
						{
							target.GetComponent<Rigidbody2D>().AddForce (new Vector2 (-knockback, 0f));
						}

						knockbackCounter = 0;
					}
					else if(target == null)
					{
						knockbackCounter = 0;
					}
						
					lastAttackTime = Time.time;
				}
			}
			else
			{
				Attacking = false;
			}
		}
	}
}
