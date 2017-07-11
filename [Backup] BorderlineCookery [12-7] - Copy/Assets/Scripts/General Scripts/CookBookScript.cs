using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class CookBookScript : MonoBehaviour {

	public GameObject[] menu;
	public int chosenmenu;

	// Use this for initialization
	void Start () {
		chosenmenu = 0;
		this.transform.Find("CookingMenu" + chosenmenu).Find("Button0").GetComponent<Button>().Select();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.J))
		{
			PlayerScript.animator.SetBool("In_Recipe_Book", false);
			this.gameObject.SetActive(false);
			PlayerScript.Instance.prevPlayerState = PlayerScript.Instance.currPlayerState;
			PlayerScript.Instance.currPlayerState = PlayerScript.PlayerState.IDLE;
		}

		if(Input.GetKeyDown(KeyCode.Tab))
		{
			this.transform.Find("CookingMenu" + chosenmenu).gameObject.SetActive(false);
			chosenmenu++;
			if(chosenmenu >= 4)
			{
				chosenmenu = 0;
			}
			this.transform.Find("CookingMenu" + chosenmenu).gameObject.SetActive(true);
			OnDisable();
			StartCoroutine(HighlightButton());
		}
	}

	void OnEnable() 
	{
		StartCoroutine(HighlightButton());
	}

	void OnDisable()
	{
		EventSystem.current.currentSelectedGameObject.GetComponent<Outline>().enabled = false;
		EventSystem.current.SetSelectedGameObject(null);
	}

	IEnumerator HighlightButton()
	{
		EventSystem.current.SetSelectedGameObject(null);
		yield return new WaitForEndOfFrame();
		this.transform.Find("CookingMenu" + chosenmenu).Find("Button0").GetComponent<Button>().Select();
	}
}
