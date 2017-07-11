using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletArc : MonoBehaviour {

	public static int damage;
	public float selfDestructTimer;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.transform != this.transform && other.tag != "Enemies" && other.tag != "Range Attack" && other.tag != "Food" && other.tag != "Ingredients") // Does not hit itself and other bullets
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
