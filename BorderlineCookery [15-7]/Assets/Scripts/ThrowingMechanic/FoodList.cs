using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FoodList : MonoBehaviour {

	public List<GameObject> foodList;
	public GameObject hudCanvas;
	public int maxLength = 8;

	// Use this for initialization
	void Start () {
		foodList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void BulletCollide(GameObject other)
	{
		other.GetComponent<Rigidbody2D>().isKinematic = false;
		other.GetComponent<BoxCollider2D>().enabled = true;
		other.GetComponent<BoxCollider2D>().isTrigger = false;
		other.GetComponent<SpriteRenderer>().enabled = true;
	}

	public void DeactivateUIImage() 
	{
		if(foodList.Count == 0)
		{
			hudCanvas.transform.Find("Food0").Find("FoodImage").gameObject.SetActive(false);
		}
		else if(foodList.Count == 1)
		{
			hudCanvas.transform.Find("Food1").Find("FoodImage").gameObject.SetActive(false);
		}
		else if(foodList.Count == 2)
		{
			hudCanvas.transform.Find("Food2").Find("FoodImage").gameObject.SetActive(false);
		}
		else if(foodList.Count == 3)
		{
			hudCanvas.transform.Find("Food3").Find("FoodImage").gameObject.SetActive(false);
		}
		else if(foodList.Count == 4)
		{
			hudCanvas.transform.Find("Food4").Find("FoodImage").gameObject.SetActive(false);
		}

		UpdateList();
	}

	public void UpdateList()
	{

		for(int i = 0; i < foodList.Count; i++)
		{
			if(foodList.Count == 1)
			{
				hudCanvas.transform.Find("Food0").Find("FoodImage").gameObject.SetActive(true);
				hudCanvas.transform.Find("Food0").Find("FoodImage").gameObject.GetComponent<Image>().sprite = foodList[i].GetComponent<SpriteRenderer>().sprite;
			}
			else if(foodList.Count == 2)
			{
				if(i == 0)
				{
					hudCanvas.transform.Find("Food0").Find("FoodImage").gameObject.SetActive(true);
					hudCanvas.transform.Find("Food0").Find("FoodImage").gameObject.GetComponent<Image>().sprite = foodList[i].GetComponent<SpriteRenderer>().sprite;
				}
				else if(i == 1)
				{
					hudCanvas.transform.Find("Food1").Find("FoodImage").gameObject.SetActive(true);
					hudCanvas.transform.Find("Food1").Find("FoodImage").gameObject.GetComponent<Image>().sprite = foodList[i].GetComponent<SpriteRenderer>().sprite;
				}
			}
			else if(foodList.Count == 3)
			{
				if(i == 0)
				{
					hudCanvas.transform.Find("Food0").Find("FoodImage").gameObject.SetActive(true);
					hudCanvas.transform.Find("Food0").Find("FoodImage").gameObject.GetComponent<Image>().sprite = foodList[i].GetComponent<SpriteRenderer>().sprite;
				}
				else if(i == 1)
				{
					hudCanvas.transform.Find("Food1").Find("FoodImage").gameObject.SetActive(true);
					hudCanvas.transform.Find("Food1").Find("FoodImage").gameObject.GetComponent<Image>().sprite = foodList[i].GetComponent<SpriteRenderer>().sprite;
				}
				else if(i == 2)
				{
					hudCanvas.transform.Find("Food2").Find("FoodImage").gameObject.SetActive(true);
					hudCanvas.transform.Find("Food2").Find("FoodImage").gameObject.GetComponent<Image>().sprite = foodList[i].GetComponent<SpriteRenderer>().sprite;
				}
			}
			else if(foodList.Count == 4)
			{
				if(i == 0)
				{
					hudCanvas.transform.Find("Food0").Find("FoodImage").gameObject.SetActive(true);
					hudCanvas.transform.Find("Food0").Find("FoodImage").gameObject.GetComponent<Image>().sprite = foodList[i].GetComponent<SpriteRenderer>().sprite;
				}
				else if(i == 1)
				{
					hudCanvas.transform.Find("Food1").Find("FoodImage").gameObject.SetActive(true);
					hudCanvas.transform.Find("Food1").Find("FoodImage").gameObject.GetComponent<Image>().sprite = foodList[i].GetComponent<SpriteRenderer>().sprite;
				}
				else if(i == 2)
				{
					hudCanvas.transform.Find("Food2").Find("FoodImage").gameObject.SetActive(true);
					hudCanvas.transform.Find("Food2").Find("FoodImage").gameObject.GetComponent<Image>().sprite = foodList[i].GetComponent<SpriteRenderer>().sprite;
				}
				else if(i == 3)
				{
					hudCanvas.transform.Find("Food3").Find("FoodImage").gameObject.SetActive(true);
					hudCanvas.transform.Find("Food3").Find("FoodImage").gameObject.GetComponent<Image>().sprite = foodList[i].GetComponent<SpriteRenderer>().sprite;
				}
			}
			else if(foodList.Count == 5)
			{
				if(i == 0)
				{
					hudCanvas.transform.Find("Food0").Find("FoodImage").gameObject.SetActive(true);
					hudCanvas.transform.Find("Food0").Find("FoodImage").gameObject.GetComponent<Image>().sprite = foodList[i].GetComponent<SpriteRenderer>().sprite;
				}
				else if(i == 1)
				{
					hudCanvas.transform.Find("Food1").Find("FoodImage").gameObject.SetActive(true);
					hudCanvas.transform.Find("Food1").Find("FoodImage").gameObject.GetComponent<Image>().sprite = foodList[i].GetComponent<SpriteRenderer>().sprite;
				}
				else if(i == 2)
				{
					hudCanvas.transform.Find("Food2").Find("FoodImage").gameObject.SetActive(true);
					hudCanvas.transform.Find("Food2").Find("FoodImage").gameObject.GetComponent<Image>().sprite = foodList[i].GetComponent<SpriteRenderer>().sprite;
				}
				else if(i == 3)
				{
					hudCanvas.transform.Find("Food3").Find("FoodImage").gameObject.SetActive(true);
					hudCanvas.transform.Find("Food3").Find("FoodImage").gameObject.GetComponent<Image>().sprite = foodList[i].GetComponent<SpriteRenderer>().sprite;
				}
				else if(i == 4)
				{
					hudCanvas.transform.Find("Food4").Find("FoodImage").gameObject.SetActive(true);
					hudCanvas.transform.Find("Food4").Find("FoodImage").gameObject.GetComponent<Image>().sprite = foodList[i].GetComponent<SpriteRenderer>().sprite;
				}
			}
		}
	}
}
