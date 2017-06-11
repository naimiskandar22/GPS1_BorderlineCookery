using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour{

	public int health;
	public int moveSpeed;
	public int distanceToEnemies; //apply in unity
	public EnemyList _enemyList; //apply the player in unity
	//public Texture2D emptyTex;
	//public Texture2D fullTex;

	private Transform target; //capital T for positions
	private Transform myTransform;
	private float time = 1f;



	void Awake ()
	{
		myTransform = transform; 
	}

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

	void OnGUI()
	{
		Vector2 targetPos;
		targetPos = Camera.current.WorldToScreenPoint (myTransform.position);
		/*GUI.BeginGroup (new Rect (targetPos.x, targetPos.y, 60, 20));
			GUI.Box (new Rect (0, 0, 60, 20), emptyTex);

			GUI.BeginGroup (new Rect (0, 0, 60 * health, 30));
			GUI.Box (new Rect (targetPos.x, targetPos.y ,50,30), fullTex);
			GUI.EndGroup ();
		GUI.EndGroup ();*/
		GUI.Box (new Rect (targetPos.x, targetPos.y,50,30), health.ToString());

	}

	// Update is called once per frame
	void Update () 
	{
		if (_enemyList.EnemiesList.Count != 0) 
		{
			FindEnemy ();
		}
		if (target != null) //checking if not null
		{
			MoveToEnemy ();
		}

	}

	//Taking Damage
	public void TakeDamage(int damage) 
	{
		health -= damage;
		if (health <= 0) 
		{
			Destroy(gameObject);
			Debug.Log ("Hero is Dead");
		}
	}
}


	


