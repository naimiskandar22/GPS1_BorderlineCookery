using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowingControl : MonoBehaviour 
{
	public float bulletspeed = 20f;
	float speed = 3.0f;
	public GameObject hudCanvas;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey(KeyCode.W))
		{
			transform.Translate(Vector3.up * Time.deltaTime * speed);
		}
		if(Input.GetKey(KeyCode.A))
		{
			transform.Translate(Vector3.left * Time.deltaTime * speed);
		}
		if(Input.GetKey(KeyCode.D))
		{
			transform.Translate(Vector3.right * Time.deltaTime * speed);
		}
		if(Input.GetKey(KeyCode.S))
		{
			transform.Translate(Vector3.down * Time.deltaTime * speed);
		}

		if(Input.GetKey(KeyCode.Q))
		{
			if(LaunchArcRenderer.Instance.upwards == true) 
			{
				LaunchArcRenderer.Instance.angle += 1f;
			}
			else
			{
				LaunchArcRenderer.Instance.angle -= 1f;
			}
		}

		if(Input.GetKeyDown(KeyCode.Q))
		{
			for(int i = 0; i < LaunchArcRenderer.Instance.resolution; i++)
			{
				LaunchArcRenderer.Instance.arcpoint[i]= Instantiate(LaunchArcRenderer.Instance.arc, transform.position, Quaternion.identity);
			}
		}

		if(Input.GetKeyUp(KeyCode.Q))
		{
			if(GetComponent<FoodList>().foodList.Count > 0)
			{
				Vector3 dir = Quaternion.AngleAxis(LaunchArcRenderer.Instance.angle, Vector3.forward) * Vector3.right;
				GameObject newbullet = Instantiate(GetComponent<FoodList>().foodList[0], transform.position, Quaternion.identity);
				GetComponent<FoodList>().BulletCollide(newbullet);
				Destroy(GetComponent<FoodList>().foodList[0]);
				GetComponent<FoodList>().foodList.RemoveAt(0);
				newbullet.GetComponent<Rigidbody2D>().AddForce(dir * bulletspeed);
				GetComponent<FoodList>().DeactivateUIImage();
			}

			for(int i = 0; i < LaunchArcRenderer.Instance.resolution; i++)
			{
				Destroy(LaunchArcRenderer.Instance.arcpoint[i]);
			}

			LaunchArcRenderer.Instance.angle = 0f;
		}

		if(Input.GetKeyDown(KeyCode.E))
		{
			GameObject temp = GetComponent<FoodList>().foodList[0];
			GetComponent<FoodList>().foodList.RemoveAt(0);
			GetComponent<FoodList>().foodList.Add(temp);
			GetComponent<FoodList>().UpdateList();
		}

		if(Input.GetKeyDown(KeyCode.R))
		{
			GameObject temp = GetComponent<FoodList>().foodList[GetComponent<FoodList>().foodList.Count - 1];
			GetComponent<FoodList>().foodList.RemoveAt(GetComponent<FoodList>().foodList.Count - 1);
			GetComponent<FoodList>().foodList.Insert(0, temp);
			GetComponent<FoodList>().UpdateList();
		}

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Food")
		{
			if(GetComponent<FoodList>().foodList.Count < GetComponent<FoodList>().maxLength)
			{
				GetComponent<FoodList>().foodList.Add(other.gameObject);
				other.GetComponent<Bullet>().ResetStats();
				Debug.Log(GetComponent<FoodList>().foodList.Count + "string");
				GetComponent<FoodList>().UpdateList();
			}
		}
	}
}
