using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletLine : MonoBehaviour {

	public static int damage;
	public static Transform target;
	public float selfDestructTimer;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.transform != this.transform && other.tag != "Enemies" && other.tag != "Range Attack" && other.transform == target) // Does not hit itself and other bullets and only the target
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
