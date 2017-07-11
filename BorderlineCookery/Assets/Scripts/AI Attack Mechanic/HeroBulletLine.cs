using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBulletLine : MonoBehaviour {

	public static int damage;
	public static Transform target;
	public float selfDestructTimer;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.transform != this.transform && other.tag != "Hero" && other.tag != "Range Attack" && other.tag != "Ingredients" && other.tag != "Food" && other.tag != "Player" && other.transform == target) // Does not hit itself and other bullets and only the target
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
