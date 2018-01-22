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
				AdManager.Instance.ShowInterstitialAd (adTag);
				break;

			case AdType.Video:
				AdManager.Instance.ShowVideoAd (adTag);
				break;

			case AdType.Chartboost:
				AdManager.Instance.ShowChartboostInterstitial ();
				break;
			}


		}

	}
}