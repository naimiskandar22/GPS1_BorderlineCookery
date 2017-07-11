using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolArea : MonoBehaviour {

	public Vector2 PatrolSize;
	public List<float> Points = new List<float> ();

	private int point1;
	private int point2;

	void Awake()
	{
		point1 = Mathf.FloorToInt(transform.position.x - (PatrolSize.x / 2.0f));
		point2 = Mathf.FloorToInt(transform.position.x + (PatrolSize.x / 2.0f));

		Points.Add (point1);
		Points.Add (point2);
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube (transform.position,new Vector2 (PatrolSize.x,PatrolSize.y));
	}


}
