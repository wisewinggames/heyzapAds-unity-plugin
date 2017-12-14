using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Money : MonoBehaviour {

	Text txt;
	int money = 0;

	void Start(){
		txt = GetComponent<Text> ();
		txt.text = money.ToString ();
	}
	
	// Update is called once per frame
	public void AddMoney () {
		money++;
		txt.text = money.ToString ();
	}
}
