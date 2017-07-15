using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchArcRenderer : MonoBehaviour {

	public static LaunchArcRenderer Instance;
	public float velocity = 0f;
	public float maxvelocity = 20f;
	public float minAngle = 10f;
	public float maxAngle = 70f;
	public float angle = 0f;
	public int resolution = 10;
	public bool upwards = true;
	public List<GameObject> arcpoint;
	public GameObject arc; 

	float g;
	float radianAngle;

	void Awake() 
	{
		Instance = this;
		g = Mathf.Abs(Physics2D.gravity.y);
	}

	// Use this for initialization
	void Start () {

		arcpoint = new List<GameObject>();

		RenderArc();
	}

	// Update is called once per frame
	void Update () {

		if(angle >= maxAngle)
		{
			upwards = false;
		}
		else if(angle <= minAngle)
		{
			upwards = true;
		}
		velocity = Mathf.Cos(radianAngle) * maxvelocity;

		RenderArc();
	}

	void RenderArc() {

		for(int i = 0; i < arcpoint.Count; i++)
		{
			if(arcpoint[i] != null)
			{
				radianAngle = Mathf.Deg2Rad * angle;
				float maxDistance = (velocity * velocity * Mathf.Sin(2*radianAngle)) / g;
				float x = (float)i / (float)resolution * maxDistance;
				float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 *(velocity * Mathf.Cos(radianAngle) * (velocity * Mathf.Cos(radianAngle)))));

				arcpoint[i].transform.position =  new Vector3(x + this.transform.position.x, y * 0.75f + this.transform.position.y + 1);
			}
		}
	}

}
