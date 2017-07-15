using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAtArea : MonoBehaviour {

	public GameObject Enemy;
	public int amountOfEnemies;
	public int Distance;
	public float timeDelay;
	public int spawnlimit;
	public int currentenemy;
	public float floorY;
	public List<GameObject> spawnEnemy = new List<GameObject>();
	public List<GameObject> spawnedEnemies = new List<GameObject>();

	public float timeN;
	private int count;
	private Transform myTransform;
	public List<GameObject> heroesPosition = new List<GameObject>();

	void Awake ()
	{
		myTransform = transform; 
	}

	public void AddLists()
	{
		if (spawnEnemy.Count != amountOfEnemies) 
		{
			for (int i = 0; i < amountOfEnemies; i++) 
			{
				spawnEnemy.Add (Enemy);
			}
		}

		foreach (GameObject Hero in GameObject.FindGameObjectsWithTag("Hero")) 
		{
			if (!heroesPosition.Contains (Hero)) 
			{
				heroesPosition.Add (Hero);
			} 
		}
	}

	public void SpawnEnemy()
	{
		GameObject newenemy =  Instantiate (spawnEnemy[count], new Vector3(this.transform.position.x,floorY,0) , this.transform.rotation);
		spawnedEnemies.Add(newenemy);
		count += 1;
	}
		

	void Update()
	{
		for(int i = 0; i < spawnedEnemies.Count; i++)
		{
			if(spawnedEnemies[i] == null)
			{
				spawnedEnemies.RemoveAt(i);
			}
		}
		if (spawnEnemy.Count != amountOfEnemies) 
		{
			AddLists ();

		}
		for(int i = 0; i < heroesPosition.Count; i++) 
		{
			if(count != amountOfEnemies && spawnedEnemies.Count < spawnlimit)
			{
				if(heroesPosition[i] != null)
				{
					if (Vector3.Distance (heroesPosition [i].transform.position, myTransform.transform.position) < Distance) 
					{
						timeN += Time.deltaTime;
						if (timeN >= timeDelay)
						{
							timeN = 0.0f;
							SpawnEnemy ();
						}
					}
				}
			}

		}
	
	}

}
