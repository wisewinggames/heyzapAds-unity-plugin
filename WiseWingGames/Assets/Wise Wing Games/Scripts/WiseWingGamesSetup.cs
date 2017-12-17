using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WiseWingGames
{

	public class WiseWingGamesSetup : MonoBehaviour
	{
		public static WiseWingGamesSetup instance = null;


		[SerializeField]GeneralSettings generalSettings;

		public GeneralSettings GetGeneralSettings{ get { return generalSettings; } }

		[SerializeField]AdsSettings adsSettings;

		public AdsSettings GetAdsSettings { get { return adsSettings; } }

		[SerializeField]GameServicesSettings gameServicesSettings;
		public GameServicesSettings GetGameServicesSettings{get{ return gameServicesSettings;}}

		void Awake ()
		{
			if (instance != null) {
				Destroy (gameObject);
			} else {
				instance = this;
				DontDestroyOnLoad (gameObject);
			}

		}







		void OnDestroy ()
		{
			if (instance == this) {
				instance = null;
			}
		}
	}
}
