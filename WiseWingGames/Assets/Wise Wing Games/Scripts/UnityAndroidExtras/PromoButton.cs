using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace WiseWingGames
{
	[RequireComponent (typeof(Button))]
	public class PromoButton : MonoBehaviour
	{
		public enum ButtonType
		{
			RateMyGame, MoreGames, FacebookPage, ShareScreenshot, OpenShare
		}
		public ButtonType _buttonType;
	
		Button _button;


		void Start ()
		{
			_button = GetComponent<Button> ();

			_button.onClick.AddListener (delegate {
				OnClickCallback();
			});
		}


		void OnClickCallback(){
		
			switch (_buttonType) {
		
			case ButtonType.RateMyGame:
				UnityAndroidExtras.instance.rateMyGame ();
				break;

			case ButtonType.MoreGames:
				UnityAndroidExtras.instance.OpenDeveloperPage ();
				break;

			case ButtonType.FacebookPage:
				UnityAndroidExtras.instance.OpenFacebookPage ();
				break;


			case ButtonType.ShareScreenshot:
				UnityAndroidExtras.instance.ShareScreenshot ();
				break;

			case ButtonType.OpenShare:
				UnityAndroidExtras.instance.OpenShareGameLink ();
				break;
			}
		}
		
	

	}
}