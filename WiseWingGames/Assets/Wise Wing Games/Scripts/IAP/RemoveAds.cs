using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace WiseWingGames
{
	public class RemoveAds : MonoBehaviour
	{
		public Text txt;

		void Start ()
		{
			if (AdManager.instance.CheckIfAdsAreRemoved ())
				gameObject.SetActive (false);
		}


		public void Purchased (Product product)
		{
			AdManager.instance.RemoveAds ();	
			gameObject.SetActive (false);
			txt.text = ""+product + " bought successfully";
		}

		public void Failure(Product product, PurchaseFailureReason reason){
			txt.text = ""+ product+" purchase failed: Reason: "+reason;
		}
	}
}