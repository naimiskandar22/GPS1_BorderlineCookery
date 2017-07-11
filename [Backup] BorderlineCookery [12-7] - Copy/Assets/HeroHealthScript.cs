using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroHealthScript : MonoBehaviour {

	public Hero hero;
	public Image healthbar;
	public float health;
	public float maxhealth;

	// Use this for initialization
	void Start () {
		health = hero.health;
		maxhealth = hero.maxhealth;
	}
	
	// Update is called once per frame
	void Update () {
		health = hero.health;
		healthbar.fillAmount = (maxhealth - health) / maxhealth;
	}
}
