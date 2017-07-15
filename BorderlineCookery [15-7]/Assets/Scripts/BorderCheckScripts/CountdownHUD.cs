using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownHUD : MonoBehaviour {

	public Transform CountdownBar;
	public Transform TextBox;
	[SerializeField] float currentAmount;
	[SerializeField] float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		currentAmount = PlayerScript.Instance.deathTimer % 1;
		int temptext = (int)PlayerScript.Instance.deathTimer + 1;
		TextBox.GetComponent<Text>().text = temptext.ToString();
		CountdownBar.GetComponent<Image>().fillAmount = currentAmount;
	}
}
