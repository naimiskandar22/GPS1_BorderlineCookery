using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBulletArc : MonoBehaviour {

	public static int damage;
	public float selfDestructTimer;

	void OnTriggerEnter2D(Collider2D other)
	{
		/*
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1);

		for(int i = 0; i < hitColliders.Length; i++)
		{
			//Debug.Log(hitColliders.Length);
			if(hitColliders[i].isTrigger)
			{
				if(hitColliders[i].transform != this.transform && hitColliders[i].tag != "Hero" && hitColliders[i].tag != "Range Attack" && hitColliders[i].tag != "Food" && hitColliders[i].tag != "Ingredients" && hitColliders[i].tag != "Player") // Does not hit itself and other bullets
				{
					hitColliders[i].SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
				}
			}
		}

		Destroy(gameObject);
		*/

		if(other.transform != this.transform && other.tag != "Hero" && other.tag != "Range Attack" && other.tag != "Ingredients" && other.tag != "Food" && other.tag != "Player") // Does not hit itself and other bullets
		{
			other.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}
	}

	//Bullet Destroys Self After Timer
	void Update()
	{
		Destroy(gameObject, selfDestructTimer);
	}
}
