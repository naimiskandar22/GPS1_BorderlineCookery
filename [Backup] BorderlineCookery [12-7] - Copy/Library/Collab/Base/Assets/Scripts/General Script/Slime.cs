using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {

	public float health;
	public bool enablePatrol;
	public PatrolArea _patrolArea;
	public int moveSpeed;
	public int amountTime;
	public GameObject healthBar;
	public AIAttackMelee Melee;
	public AIAttackRangeArc RangeArc;
	public AIAttackRangeLine RangeStraight;
	public ParticleSystem heal,buff,debuff;
	public int HealAmount, BuffAmount, DebuffAmount;
	public float BuffTime, DebuffTime;


	private float maxhealth,maxbuff,maxdebuff;
	private Transform myTransform;
	private int point;
	private float particleTime = 1.5f;
	private bool DmgBuff,Dmgdebuff;


	void Awake()
	{
		myTransform = transform;
		maxhealth = health;
		maxbuff = BuffTime;
		maxdebuff = DebuffTime;

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
		DebuffTime-=Time.deltaTime;
		BuffTime-=Time.deltaTime;

		if (DebuffTime <= 0 && Dmgdebuff == true) 
		{
			Dmgdebuff = false;
			BuffTime = maxbuff;
			DebuffTime = maxdebuff;
			Melee.damage += DebuffAmount;
			RangeArc.damage += DebuffAmount;
			RangeStraight.damage += DebuffAmount;

		}
		if (BuffTime <= 0 && DmgBuff == true) 
		{
			DmgBuff = false;
			BuffTime = maxbuff;
			DebuffTime = maxdebuff;
			Melee.damage -= BuffAmount;
			RangeArc.damage -= BuffAmount;
			RangeStraight.damage -= BuffAmount;
		}
	}

	public void particlePlaying()
	{
		if (heal.isPlaying) 
		{
			particleTime = particleTime - Time.deltaTime;
			if (particleTime <= 0) 
			{
				heal.Stop ();
				particleTime = 1.5f;
				Debug.Log ("Stopped healing animation");
			}
		}
		if (buff.isPlaying) 
		{
			particleTime = particleTime - Time.deltaTime;
			if (particleTime <= 0) 
			{
				buff.Stop ();
				particleTime = 1.5f;
				Debug.Log ("Stopped buffing animation");
			}
		}
		if (debuff.isPlaying) 
		{
			particleTime = particleTime - Time.deltaTime;
			if (particleTime <= 0) 
			{
				debuff.Stop ();
				particleTime = 1.5f;
				Debug.Log ("Stopped debuffing animation");
			}
		}

	}

	/*void OnGUI()
	{
		if (gameObject.activeSelf) 
		{
			Vector2 targetPos;
			targetPos = Camera.current.WorldToScreenPoint (myTransform.position);
			GUI.Box (new Rect (targetPos.x, targetPos.y,50,30), health.ToString());
		}

	}*/

	void OnTriggerEnter2D(Collider2D Dish)
	{
		Debug.Log ("Collision Detected");
		if (Dish.tag == "Food") 
		{
			if (Dish.name.Contains ("Heal")) 
			{
				heal.Play ();
				Debug.Log ("Healing animation started");
			} 
			else if (Dish.name.Contains ("Buff")) 
			{
				buff.Play ();
				Debug.Log ("Buffing animation started");
				DmgBuff = true;
				Dmgdebuff = false;
				StatusAil (DmgBuff, Dmgdebuff);
			}
			else if (Dish.name.Contains ("Debuff")) 
			{
				debuff.Play ();
				Debug.Log ("debuffing animation started");
				DmgBuff = false;
				Dmgdebuff = true;
				StatusAil (DmgBuff, Dmgdebuff);
			}
			else if (Dish.name.Contains ("Lure")) 
			{

			}

		} 

	}

	// Update is called once per frame
	void Update () 
	{
		if (enablePatrol == true && gameObject.activeSelf == true) 
		{
			patroling();
		}
		CurrentHealth ();
		if (DmgBuff == true || Dmgdebuff == true) 
		{
			checkStatus ();
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
