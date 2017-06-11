using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Variables
{
	public GameObject enemy;
	public float distance;

	public Variables(GameObject enemy, float distance)
	{
		this.enemy = enemy;
		this.distance = distance;
	}
}

public class EnemyList : MonoBehaviour{

	public List<GameObject> GameObjects = new List<GameObject>();
	public List<Variables> EnemiesList = new List<Variables>();
	public Hero _heroes;

	private bool isSorted;

	public void bubbleSort()
	{
		do 
		{
			for (int i = 0; i < EnemiesList.Count - 1; i++) 
			{
				if (EnemiesList[i].distance > EnemiesList[i + 1].distance)
				{
					var temp = EnemiesList[i];
					EnemiesList[i] = EnemiesList[i + 1];
					EnemiesList[i + 1] = temp;
				}
				isSorted = true;
			}
		} while(!isSorted);

		Debug.Log ("Sorted");
	}

	public void Enemy()
	{
		foreach (GameObject Enemy in GameObject.FindGameObjectsWithTag("Enemies"))
		{
			if (!GameObjects.Contains (Enemy)) 
			{
				GameObjects.Add (Enemy);
				Debug.Log ("Added enemies");
			}

		}
	}

	public void Distance()
	{
		for (int i = 0; i < GameObjects.Count; i++) 
		{
			float distance = Vector3.Distance (_heroes.transform.position, GameObjects[i].transform.position);
			EnemiesList.Add (new Variables (GameObjects [i], distance));
			Debug.Log ("Added distance");
		}
	}

	public void checkNull()
	{
		for (int i = 0; i < GameObjects.Count; i++)
		{
			if (GameObjects[i] == null) 
			{
				GameObjects.RemoveAt (i);
			}
		}
		for (int j = 0; j < EnemiesList.Count; j++)
		{
			if (EnemiesList[j].enemy == null ) 
			{
				EnemiesList.RemoveAt (j);
			}
		}
	}

	public void clearList ()
	{
		EnemiesList.Clear ();
		Debug.Log ("Updating Distance");

	}

	void Update ()
	{
		Enemy ();
		if (EnemiesList.Count != GameObjects.Count) 
		{
			Distance ();
		}
		bubbleSort ();
		checkNull ();
		if (EnemiesList.Count != GameObjects.Count || EnemiesList.Count > GameObjects.Count) 
		{
			clearList ();
		}

	}


}
