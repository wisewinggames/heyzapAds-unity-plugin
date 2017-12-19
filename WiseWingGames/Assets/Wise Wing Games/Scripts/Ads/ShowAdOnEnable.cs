using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace WiseWingGames
{
	public class ShowAdOnEnable : MonoBehaviour
	{
		public enum AdType
		{
			Static,
			Video,
			Chartboost

		}

		public AdType _adType;

		public string adTag;

		void OnEnable ()
		{
			switch (_adType) {

			case AdType.Static:
				AdManager.instance.ShowInterstitialAd (adTag);
				break;

			case AdType.Video:
				AdManager.instance.ShowVideoAd (adTag);
				break;

			case AdType.Chartboost:
				AdManager.instance.ShowChartboostInterstitial ();
				break;
			}


		}

	}
}