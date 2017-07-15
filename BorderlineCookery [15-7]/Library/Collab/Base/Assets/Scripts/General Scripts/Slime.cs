using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {

	public float health;
	public int moveSpeed;
	public GameObject healthBar;
	public AIAttackMelee Melee;
	public AIAttackRangeArc RangeArc;
	public AIAttackRangeLine RangeStraight;
	public ParticleSystem heal,buff,debuff;
	public int HealAmount, BuffAmount, DebuffAmount;
	public float BuffTime, DebuffTime, LureTime;


	private float maxhealth,maxbuff,maxdebuff,maxlure;
	private Transform myTransform;
	private int point;
	private float particleTime = 1.5f;
	private bool DmgBuff,Dmgdebuff,Lured;

	//Defend Chance Mechanism
	private float defendChanceNum;


	void Awake()
	{
		myTransform = transform;
		maxhealth = health;
		maxbuff = BuffTime;
		maxdebuff = DebuffTime;
		maxlure = LureTime;
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
		if(Dmgdebuff) DebuffTime-=Time.deltaTime;
		if(DmgBuff) BuffTime-=Time.deltaTime;
		if (Lured) LureTime -= Time.deltaTime;

		if (DebuffTime <= 0 && Dmgdebuff == true) 
		{
			Dmgdebuff = false;
			DebuffTime = maxdebuff;
			if(Melee != null) Melee.damage += BuffAmount;
			if(RangeArc != null) RangeArc.damage += BuffAmount;
			if(RangeStraight != null) RangeStraight.damage += BuffAmount;

		}
		if (BuffTime <= 0 && DmgBuff == true) 
		{
			DmgBuff = false;
			BuffTime = maxbuff;
			if(Melee != null) Melee.damage -= DebuffAmount;
			if(RangeArc != null) RangeArc.damage -= DebuffAmount;
			if(RangeStraight != null) RangeStraight.damage -= DebuffAmount;
		}
		if (LureTime <= 0 && Lured == true) 
		{
			Lured = false;
			LureTime = maxlure;
			gameObject.tag = "Enemies";
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

	public void MoveAI()
	{
		MoveForward ();
	}

	void MoveForward()
	{
		myTransform.position += Vector3.left * Time.deltaTime * moveSpeed;
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

//	void OnTriggerEnter2D(Collider2D Dish)
//	{
//		Debug.Log ("Collision Detected");
//		if (Dish.tag == "Food") 
//		{
//			if (Dish.name.Contains ("Heal")) 
//			{
//				heal.Play ();
//				Debug.Log ("Healing animation started");
//			} 
//			else if (Dish.name.Contains ("Buff")) 
//			{
//				buff.Play ();
//				Debug.Log ("Buffing animation started");
//				DmgBuff = true;
//				Dmgdebuff = false;
//				StatusAil (DmgBuff, Dmgdebuff);
//			}
//			else if (Dish.name.Contains ("Debuff")) 
//			{
//				debuff.Play ();
//				Debug.Log ("debuffing animation started");
//				DmgBuff = false;
//				Dmgdebuff = true;
//				StatusAil (DmgBuff, Dmgdebuff);
//			}
//			else if (Dish.name.Contains ("Lure")) 
//			{
//
//			}
//
//		} 
//
//	}


	//Defend Chance RandNum For Slimes
	public void defendChance()
	{
		defendChanceNum = Random.Range (0, 10);
	}

	// Update is called once per frame
	void Update () 
	{
		CurrentHealth ();
		particlePlaying ();
		if (DmgBuff == true || Dmgdebuff == true || Lured == true) 
		{
			checkStatus ();
		}
		if(Melee.Attacking == false) MoveAI ();
	}

	//Taking Damage
	public void TakeDamage(int damage)
	{
		defendChance ();

		if (defendChanceNum != 0) 
		{
			health -= damage;
		}

		if(health <= 0) 
		{
			Destroy(gameObject);
			Debug.Log ("Slime is Dead");
		}
	}

	public void DishEffect(GameObject other)
	{
		Debug.Log ("Collision Detected");
		if (other.tag == "Food") 
		{
			if (other.GetComponent<Bullet>().foodtype == "Heal") 
			{
				if ((health + HealAmount) < maxhealth) 
				{
					health += HealAmount;
				} 
				else 
				{
					health = maxhealth;
				}

				heal.Play ();
				Debug.Log (name + "Healing animation started");
			} 
			else if (other.GetComponent<Bullet>().foodtype == "Buff") 
			{
				Debug.Log ("Buffing animation started");
				DmgBuff = true;
				Dmgdebuff = false;
				if(Melee != null) Melee.damage += BuffAmount;
				if(RangeArc != null) RangeArc.damage += BuffAmount;
				if(RangeStraight != null) RangeStraight.damage += BuffAmount;
				buff.Play ();
				//StatusAil (DmgBuff, Dmgdebuff);
			}
			else if (other.GetComponent<Bullet>().foodtype == "Debuff") 
			{
				Debug.Log ("debuffing animation started");
				DmgBuff = false;
				Dmgdebuff = true;
				if(Melee != null) Melee.damage -= DebuffAmount;
				if(RangeArc != null) RangeArc.damage -= DebuffAmount;
				if(RangeStraight != null) RangeStraight.damage -= DebuffAmount;
				debuff.Play ();
				//StatusAil (DmgBuff, Dmgdebuff);
			}
			else if (other.GetComponent<Bullet>().foodtype == "Lure") 
			{
				if(gameObject.tag != "Lure") gameObject.tag = "Lure";
				Lured = true;
				LureTime = maxlure;
			}

		} 
	}
}
