using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAtArea : MonoBehaviour {

	public GameObject Enemy;
	public int amountOfEnemies;
	public int Distance;
	public List<GameObject> spawnEnemy = new List<GameObject>();

	private int count;
	private Transform myTransform;
	private List<GameObject> heroesPosition = new List<GameObject>();

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
		if (Vector3.Distance (heroesPosition [0].transform.position, myTransform.transform.position) < Distance ) 
		{
			for (int i = 0; i < spawnEnemy.Count; i++) 
			{
				Instantiate (spawnEnemy[i], transform.position + new Vector3(i*3,0,0) , transform.rotation);
				count += 1;
			}
		}
	}
		

	void Update()
	{
		if (spawnEnemy.Count != amountOfEnemies) 
		{
			AddLists ();

		}
		if (count != amountOfEnemies) 
		{
			SpawnEnemy ();
			
		}
	
	}

}
