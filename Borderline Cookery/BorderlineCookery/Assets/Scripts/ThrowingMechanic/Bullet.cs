﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float expTimer = 0f;
	public float expDuration = 5f;
	public float boxTimer = 0f;
	public float boxDuration = 0.5f;
	public float transparentTimer = 0f;
	public float transparentDuration = 0.5f;
	public bool transparent = false;
	public bool startExpire = false;
	public bool startBox = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(startExpire)
		{
			expTimer += Time.deltaTime;
			transparentTimer+= Time.deltaTime;

			if(transparentTimer >= transparentDuration)
			{
				transparent = !transparent;
				transparentTimer = 0f;
			}

			if(transparent)
			{
				GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
			}
			else
			{
				GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			}

			if(expTimer >= expDuration)
			{
				Destroy(gameObject);
			}
		}

		if(startBox)
		{
			boxTimer += Time.deltaTime; 
			if(boxTimer > boxDuration)
			{
				GetComponent<Rigidbody2D>().isKinematic = true;
				GetComponent<BoxCollider2D>().isTrigger = true;
				startExpire = true;
				startBox = false;
			}
		}
	}

	public void ResetStats()
	{
		startBox = false;
		startExpire = false;
		expTimer = 0f;
		boxTimer = 0f;
		transparent = false;
		transparentTimer = 0f;
		GetComponent<BoxCollider2D>().enabled = false;
		GetComponent<BoxCollider2D>().isTrigger = false;
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<Rigidbody2D>().mass = 0.035f;
		GetComponent<Rigidbody2D>().drag = 0.295f;
		GetComponent<Rigidbody2D>().angularDrag = 1.5f;
		GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Ground")
		{
			startBox = true;
		}
	}
}