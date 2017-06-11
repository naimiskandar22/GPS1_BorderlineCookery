using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {

	public int health;
	public bool enablePatrol;
	public PatrolArea _patrolArea;
	public int moveSpeed;
	public int amountTime;


	private Transform myTransform;
	private int point;
	

	void Awake()
	{
		myTransform = transform;


	}
		

	public void patroling()
	{
		
		//myTransform.position = Vector3.Lerp (GameObject.Find ("PatrolPoint1").transform.position, GameObject.Find ("PatrolPoint2").transform.position, Mathf.PingPong (amountTime, moveSpeed));

		if ((_patrolArea.Points [1] - myTransform.position.x) > (_patrolArea.Points[0] - myTransform.position.x)) 
		{
			Debug.Log ("Starting point 1");
			myTransform.Translate ((_patrolArea.Points [1] - myTransform.position.x) * moveSpeed * Time.deltaTime, 0, 0);
		}
		if ((myTransform.position.x - _patrolArea.Points [0] ) > (_patrolArea.Points[1] - myTransform.position.x))
		{
			Debug.Log ("Starting point 2");
			myTransform.Translate ((_patrolArea.Points [0] - myTransform.position.x) * moveSpeed * Time.deltaTime, 0, 0);
		}


			
	}

	void OnGUI()
	{
		Vector2 targetPos;
		targetPos = Camera.current.WorldToScreenPoint (myTransform.position);
		GUI.Box (new Rect (targetPos.x, targetPos.y,50,30), health.ToString());
	}

	// Update is called once per frame
	void Update () 
	{
		if (enablePatrol == true && gameObject.activeSelf == true) 
		{
			patroling();
		}
	}

	//Taking Damage
	public void TakeDamage(int damage)
	{
		health -= damage;
		if(health <= 0) 
		{
			Destroy(gameObject);
			Debug.Log ("Slime is Dead");
		}
	}


}
