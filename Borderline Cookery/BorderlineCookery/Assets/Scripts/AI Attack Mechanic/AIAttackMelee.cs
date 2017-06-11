using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackMelee : MonoBehaviour {

	private List<GameObject> EnemiesList = new List<GameObject> ();
	private Transform target;

	public float attackRange;
	public int damage;
	public float attackDelay;
	private float lastAttackTime;

	public bool hasSpecialAttack;
	public int specialDamage;
	public float specialAttackDelay;
	private float specialLastAttackTime;
	public float knockback;

	private bool isSorted;

	public void theList()
	{
		// If Hero, Enemies will be added to list
		if (gameObject.tag == "Hero") 
		{
			foreach (GameObject Enemy in GameObject.FindGameObjectsWithTag("Enemies")) 
			{
				if (!EnemiesList.Contains (Enemy)) 
				{
					EnemiesList.Add (Enemy);
				} 
			}
		}

		// If Enemy, Heroes will be added to list
		if (gameObject.tag == "Enemies") 
		{
			foreach (GameObject Hero in GameObject.FindGameObjectsWithTag("Hero")) 
			{
				if (!EnemiesList.Contains (Hero)) 
				{
					EnemiesList.Add (Hero);
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
		
	// Update is called once per frame
	void Update() 
	{
		theList ();
		checkNull ();
		distanceCheckNSort();

		if (EnemiesList.Count != 0) // TEMPORARY 'IF' STATEMENT - TO BE CHANGED
		{
			target = EnemiesList [0].transform;
		}
			
		if(target != null) 
		{
			float distanceToTarget = Vector3.Distance(transform.position, target.position);
			Vector3 dir = target.transform.position - transform.position;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

			//Attack Range Check
			if(distanceToTarget < attackRange) 
			{
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
					lastAttackTime = Time.time;
				}
			}
		}
	}
}
