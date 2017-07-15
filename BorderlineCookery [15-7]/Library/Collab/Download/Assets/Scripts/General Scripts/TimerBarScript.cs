using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBarScript : MonoBehaviour {

	public Transform TimerBar;
	[SerializeField] private float totalCookingTime;

	// Update is called once per frame
	void Update () {

		totalCookingTime = PlayerScript.Instance.cookingTimer;

		/*if(totalCookingTime > 0)
		{
			totalCookingTime -= Time.deltaTime;
		}*/

		TimerBar.GetComponent<Image>().fillAmount = totalCookingTime / 2;
	}
}
