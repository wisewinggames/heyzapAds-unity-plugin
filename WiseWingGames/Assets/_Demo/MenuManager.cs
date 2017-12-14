using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

	public GameObject mainmenupanel;
	public GameObject vehicleselectpanel;
	public GameObject levelselectpanel;

	public void Back ()
	{
		DisableAll ();
		mainmenupanel.SetActive (true);
		
	}

	public void VehicleSelect ()
	{
		DisableAll ();
		vehicleselectpanel.SetActive (true);
	}

	public void LevelSelect ()
	{
		DisableAll ();
		levelselectpanel.SetActive (true);
	}

	void  DisableAll ()
	{
		mainmenupanel.SetActive (false);
		vehicleselectpanel.SetActive (false);
		levelselectpanel.SetActive (false);
	}

	public void GiveCurrency(int moneyss){
		Debug.Log ("Currency given "+ moneyss);
	}
}
