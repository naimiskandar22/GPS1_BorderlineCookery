using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {

	public float health;
	public int moveSpeed;
	public float distanceToEnemies;
	public GameObject healthBar;
	public AIAttackMelee Melee;
	public AIAttackRangeArc RangeArc;
	public AIAttackRangeLine RangeStraight;
	public ParticleSystem heal,buff,debuff;
	public int HealAmount, BuffAmount, DebuffAmount;
	public float BuffTime, DebuffTime, LureTime;

	public float maxhealth, maxlure;
	public int regMelee, regArc, regStraight;
	private Transform myTransform;
	private Transform target;
	private int point;
	private float particleTime = 1.5f;
	private bool DmgBuff,Dmgdebuff,Lured;

	//Defend Chance Mechanism
	private float defendChanceNum;


	void Awake()
	{
		myTransform = transform;
		maxhealth = health;
		if(Melee != null) regMelee = Melee.damage;
		if(RangeArc != null) regArc = RangeArc.damage;
		if(RangeStraight != null) regStraight = RangeStraight.damage;
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
		if(Lured) LureTime-=Time.deltaTime;

		if (DebuffTime <= 0 && Dmgdebuff == true) 
		{
			Dmgdebuff = false;
			if(Melee != null) Melee.damage = regMelee;
			if(RangeArc != null) RangeArc.damage = regArc;
			if(RangeStraight != null) RangeStraight.damage = regStraight;

		}
		if (BuffTime <= 0 && DmgBuff == true) 
		{
			DmgBuff = false;
			if(Melee != null) Melee.damage = regMelee;
			if(RangeArc != null) RangeArc.damage = regArc;
			if(RangeStraight != null) RangeStraight.damage = regStraight;
		}
		if (LureTime <= 0 && Lured == true) 
		{
			Lured = false;
			//LureTime = maxlure;
			gameObject.tag = "Enemies";
		}
	}

	public void particlePlaying()
	{

		if (heal.isPlaying) 
		{
			particleTime -= Time.deltaTime;
			if (particleTime <= 0) 
			{
				heal.Stop ();
				Debug.Log ("Stopped healing animation");
			}
		}
		if (buff.isPlaying) 
		{
			particleTime -= Time.deltaTime;
			if (particleTime <= 0) 
			{
				buff.Stop ();
				Debug.Log ("Stopped buffing animation");
			}

		}
		if (debuff.isPlaying) 
		{
			particleTime -= Time.deltaTime;
			if (particleTime <= 0) 
			{
				debuff.Stop ();
				Debug.Log ("Stopped debuffing animation");
			}
		}


	}

	public void MoveAI()
	{
		if(target != null) MoveToEnemy();
		else MoveForward ();
	}

	public void MoveToEnemy()
	{
		if (Vector3.Distance (target.transform.position, myTransform.transform.position) > distanceToEnemies) 
		{
			myTransform.position += (target.position - myTransform.position).normalized * moveSpeed * Time.deltaTime;
		}
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
		target = Melee.target;
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
				health += other.GetComponent<Bullet>().bonusStat;

				if(health >= maxhealth)
				{
					health = maxhealth;
				}

				particleTime = other.GetComponent<Bullet>().bonusDuration;
				heal.Play ();
				Debug.Log (name + "Healing animation started");
			} 
			else if (other.GetComponent<Bullet>().foodtype == "Buff") 
			{
				Debug.Log ("Buffing animation started");
				DmgBuff = true;
				Dmgdebuff = false;
				if(Melee != null) Melee.damage += other.GetComponent<Bullet>().bonusStat;
				if(RangeArc != null) RangeArc.damage += other.GetComponent<Bullet>().bonusStat;
				if(RangeStraight != null) RangeStraight.damage += other.GetComponent<Bullet>().bonusStat;
				BuffTime = other.GetComponent<Bullet>().bonusDuration;
				buff.Play ();
				particleTime = other.GetComponent<Bullet>().bonusDuration;
				//StatusAil (DmgBuff, Dmgdebuff);
			}
			else if (other.GetComponent<Bullet>().foodtype == "Debuff") 
			{
				Debug.Log ("debuffing animation started");
				DmgBuff = false;
				Dmgdebuff = true;
				if(Melee != null) Melee.damage -= other.GetComponent<Bullet>().bonusStat;
				if(RangeArc != null) RangeArc.damage -= other.GetComponent<Bullet>().bonusStat;
				if(RangeStraight != null) RangeStraight.damage -= other.GetComponent<Bullet>().bonusStat;
				DebuffTime = other.GetComponent<Bullet>().bonusDuration;
				debuff.Play ();
				particleTime = other.GetComponent<Bullet>().bonusDuration;
				//StatusAil (DmgBuff, Dmgdebuff);
			}
			else if (other.GetComponent<Bullet>().foodtype == "Lure") 
			{
				if(gameObject.tag != "Enemies_Lure") gameObject.tag = "Enemies_Lure";
				Lured = true;
				LureTime = other.GetComponent<Bullet>().bonusDuration;
			}

		} 
	}
}
